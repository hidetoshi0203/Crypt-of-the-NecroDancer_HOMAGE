using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class toshiPlayer : MonoBehaviour
{
    public enum DIRECTION
    {
        TOP,
        RIGHT,
        DOWN,
        LEFT
    }

    Vector2Int[] move = {
      new Vector2Int(  0, -1 ),　//TOPの場合
      new Vector2Int(  1,  0 ),   //RIGHTの場合
      new Vector2Int(  0,  1 ),   //DOWNの場合
      new Vector2Int( -1,  0 )   //LEFTの場合
    };

    private DIRECTION direction;
    public Vector2Int playerCurrentPos, playerNextPos;
    Camera cam;
    MapGenerator mapGenerator;
    NotesManager notesManager = null;
    ComboManager comboManager = null;
    //EnemyManager enemyManager = null;
    EnemySystem enemySystem = null;
    GameObject leftNotes;
    GameObject rightNotes;
    GameObject function;
    GameObject notesObjets;
    PlayerManager playerManager;
    Item item;
    CheckAliveScripts checkAliveScripts;
    private GameObject checkAliveObjs;

    float sconds;
    [SerializeField] SpriteRenderer firstSlashObj;
    [SerializeField] Sprite[] slashSprites;
    //[SerializeField] SpriteRenderer secondSlashObj;

    PlayerAttackSound playerAttackSound;

    [SerializeField] ParticleSystem sPotionEffect;
    public int playerAttackPower = 1; // プレイヤーの攻撃力
    [SerializeField] int playerMaxHP;
    private bool isPowerUp = false; // プレイヤーの攻撃力のフラグ(プレイヤーが攻撃力UPポーションを取ったか)

    public float powerUpTimer; // プレイヤーの攻撃力UPの効果時間
    public float powerUpTimerEnd = 20.0f; // プレイヤー攻撃力UPの効果が切れる時間
    public bool isPowerUpTimer = false; // プレイヤー攻撃力UPの効果時間のフラグ
    AudioSource audioSource;
    public AudioClip getSPotionSound;

    private void Start()
    {
        mapGenerator = transform.parent.GetComponent<MapGenerator>();
        direction = DIRECTION.DOWN;
        notesObjets = GameObject.FindGameObjectWithTag("Notes");
        cam = Camera.main;
        cam.transform.position = transform.position + new Vector3(0,0,-1);

        //playerAttackSound.audioSource = gameObject.AddComponent<AudioSource>();
        GameObject playerManagerObj = GameObject.Find("PlayerManager");
        playerManager = playerManagerObj.GetComponent<PlayerManager>();
        audioSource = GetComponent<AudioSource>();
        sPotionEffect.Stop();

        checkAliveObjs = GameObject.Find("CheckAliveObjects");
        checkAliveScripts = checkAliveObjs.GetComponent<CheckAliveScripts>();
    }


    private void FixedUpdate()
    {
        cam.transform.position = transform.position + new Vector3(0, 0, -1);
    }

    //　入力時に_move関数を呼ぶようにする。
    private void Update()
    {
        Debug.Log(playerAttackPower);

        if (notesManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("NotesManager");
            notesManager = inst.GetComponent<NotesManager>();
        }
        if (comboManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("ComboManager");
            comboManager = inst.GetComponent <ComboManager>();
        }
        if (enemySystem == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("EnemySystem");
            enemySystem = inst.GetComponent<EnemySystem>();
        }

        if (playerAttackSound == null)
        {
            GameObject inst = GameObject.Find("PlayerAttackSound");
            playerAttackSound = inst.GetComponent<PlayerAttackSound>();
        }

        if (item == null && checkAliveScripts.isAliveItemScr)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("StrengthPotion");
            item = inst.GetComponent<Item>();
        }

        if (notesManager != null && notesManager.CanInputKey())
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                HandlePlayerMove(DIRECTION.TOP);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                HandlePlayerMove(DIRECTION.RIGHT);                
                firstSlashObj.transform.rotation = Quaternion.Euler(0,0,0);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                HandlePlayerMove(DIRECTION.DOWN);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                HandlePlayerMove(DIRECTION.LEFT);     
                firstSlashObj.transform.rotation = Quaternion.Euler(0,180,0);
            }
        }
        playerAttackPowerUpTimer();
    }

    private void HandlePlayerMove(DIRECTION directionInput)
    {
        direction = directionInput;
        moveType();
        notesManager.PlayerInputKey();
        notesManager.StopTouchSound();
    }

    //移動用の関数
    void moveType()
    {
        
        if (notesManager != null && notesManager.CanInputKey())
        {
            playerNextPos = playerCurrentPos + move[(int)direction];
            //firstSlashObj.transform.localPosition = new Vector3(move[(int)direction].x, -move[(int)direction].y);
            firstSlashObj.transform.position = new Vector3(playerNextPos.x, -playerNextPos.y);
            //playerNextPos = playerCurrentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);
            switch (mapGenerator.GetEntityMapType(playerNextPos))
            {
                //case MapGenerator.MAP_TYPE.PLAYER:
                //    // バグ
                //    break;
                case MapGenerator.MAP_TYPE.ENEMY:
                    // 攻撃
                    enemySystem.Hit(playerNextPos);
                    StartCoroutine("Slash");
                    playerAttackSound.AttackSound();
                    break;
                case MapGenerator.MAP_TYPE.ENEMY2:
                    // 攻撃
                    enemySystem.Hit(playerNextPos);
                    StartCoroutine("Slash");
                    playerAttackSound.AttackSound();
                    break;
                default:
                    TryMovement();
                    break;
            }
            void TryMovement()
            {
                switch (mapGenerator.GetStageMapType(playerNextPos))
                {
                    case MapGenerator.MAP_TYPE.GROUND:
                        Move();
                        break;
                    case MapGenerator.MAP_TYPE.WALL:
                        // 何もしない（後々その場でジャンプするようなアニメーションを入れる）
                        break;
                    case MapGenerator.MAP_TYPE.STAIRS:
                        // 次のステージに進む
                        Stairs();
                        break;
                    case MapGenerator.MAP_TYPE.WALL2:
                        // 何もしない（後々その場でジャンプするようなアニメーションを入れる）
                        break;
                    case MapGenerator.MAP_TYPE.HEALINGPOTION: // プレイヤーが回復ポーションを取ったら
                        Heal();
                        mapGenerator.GetItem(playerNextPos);
                        Move();
                        break;
                    case MapGenerator.MAP_TYPE.STRENGTHPOTION:
                        playerAttackPowerUp();
                        mapGenerator.GetItem(playerNextPos);
                        Move();
                        break;
                }
            }
        }
        
        void Move()
        {
            //Debug.Log(playerCurrentPos);
            //Debug.Log("床だよ");
            // 移動する
            mapGenerator.UpdateTile(playerCurrentPos, MapGenerator.MAP_TYPE.GROUND); // 自分の座標のMAP_TYPEをGROUNDにする
            transform.localPosition = mapGenerator.ScreenPos(playerNextPos);          // 移動
            playerCurrentPos = playerNextPos;
            mapGenerator.UpdateTile(playerCurrentPos, MapGenerator.MAP_TYPE.PLAYER); // 自分の座標のMAP_TYPEをPLAYERにする
        }
        void Stairs()
        {
            Debug.Log("階段の上だよ");
            GameObject parentObject = GameObject.Find("MapChip");

            int childCount = parentObject.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform childTransform = parentObject.transform.GetChild(i);
                GameObject childObject = childTransform.gameObject;
                Destroy(childObject);
            }

            mapGenerator.floor++;
            if (mapGenerator.floor == 2)
            {
                SceneManager.LoadScene("GameClear");
            }
            mapGenerator._loadMapData();
            mapGenerator._createMap();
        }
    }

    public void Heal() // プレイヤーが回復する関数
    {
        if (playerManager.playerHP < playerMaxHP) // プレイヤーの体力が減ってたら(3HP未満だったら)
        {
            playerManager.playerHP++; // プレイヤー体力(HP)を回復する
            playerManager.lifeArray[playerManager.playerHP -1].GetComponent<Image>().enabled = true;
        }
    }

    void playerAttackPowerUp() // プレイヤーの攻撃力が上がる関数
    {
        Debug.Log("fjs");
        isPowerUpTimer = true; // trueにしてプレイヤーの攻撃力UPの効果時間を数え始める
        isPowerUp = true;
        audioSource.PlayOneShot(getSPotionSound);
        sPotionEffect.Play(); // 攻撃力UPポーションのエフェクトを始める

        if (isPowerUp) // 攻撃力UPポーションを取ったら、
        {
            playerAttackPower++; // プレイヤーの攻撃力を上げる
            isPowerUp = false; // isPowerUpをfalseにして、プレイヤーの攻撃力を過度に上げないようにしている
        }
    }

    void playerAttackPowerUpTimer() // プレイヤーの攻撃力UPの効果時間の関数
    {
        if (isPowerUpTimer)
        {
            powerUpTimer += Time.deltaTime; // プレイヤーの攻撃力UPの効果時間を数える
            if (powerUpTimer >= powerUpTimerEnd) // 効果時間がPOWERUPTIMERENDまでいったら、
            {
                Debug.Log("PowerDown");
                isPowerUpTimer = false; // falseにして効果時間を数えるのを終わる
                powerUpTimer = 0.0f; // 効果時間を初期化する
                playerAttackPower--; // 攻撃力を元に戻す
                sPotionEffect.Stop();
            }
        }
    }

    public IEnumerator Slash()
    {
        int index = 0;
        firstSlashObj.sprite = slashSprites[index];
        firstSlashObj.enabled = true;
        yield return new WaitForSeconds(0.1f);
        index++;
        firstSlashObj.sprite = slashSprites[index];
        yield return new WaitForSeconds(0.1f);
        firstSlashObj.enabled = false;
    }
}
