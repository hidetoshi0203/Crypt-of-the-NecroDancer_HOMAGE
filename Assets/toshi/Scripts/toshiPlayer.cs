using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;
using Unity.VisualScripting;
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

    float sconds;
    [SerializeField] SpriteRenderer firstSlashObj;
    [SerializeField] Sprite[] slashSprites;
    //[SerializeField] SpriteRenderer secondSlashObj;

    PlayerAttackSound playerAttackSound;
    private void Start()
    {
        mapGenerator = transform.parent.GetComponent<MapGenerator>();
        direction = DIRECTION.DOWN;
        notesObjets = GameObject.FindGameObjectWithTag("Notes");
        cam = Camera.main;
        cam.transform.position = transform.position + new Vector3(0,0,-1);

        //playerAttackSound.audioSource = gameObject.AddComponent<AudioSource>();
    }


    private void FixedUpdate()
    {
        cam.transform.position = transform.position + new Vector3(0, 0, -1);
    }

    //　入力時に_move関数を呼ぶようにする。
    private void Update()
    {

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

        if (notesManager != null && notesManager.CanInputKey())
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                HandlePlayerMove(DIRECTION.TOP);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                HandlePlayerMove(DIRECTION.RIGHT);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                HandlePlayerMove(DIRECTION.DOWN);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                HandlePlayerMove(DIRECTION.LEFT);
            }
        }
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
            Debug.Log( "PlayerPos"+playerNextPos);
            switch (mapGenerator.GetEntityMapType(playerNextPos))
            {
                //case MapGenerator.MAP_TYPE.PLAYER:
                //    // バグ
                //    break;
                case MapGenerator.MAP_TYPE.ENEMY:
                    // 攻撃
                    Debug.Log("攻撃");
                    enemySystem.Hit(playerNextPos);
                    StartCoroutine("Slash");
                    playerAttackSound.AttackSound();
                    break;
                case MapGenerator.MAP_TYPE.ENEMY2:
                    // 攻撃
                    Debug.Log("攻撃");
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
                }
            }
            

            
            /*
            //if (mapGenerator.GetPlayerNextMapType(playerNextPos) == MapGenerator.MAP_TYPE.WALL && mapGenerator.GetPlayerNextMapType(playerNextPos) == MapGenerator.MAP_TYPE.WALL2) // 入力先(プレイヤーのnextPos)が壁だった場合
            //{
            //    
            //}
            
            if (mapGenerator.GetEntityMapType(playerNextPos) == MapGenerator.MAP_TYPE.ENEMY) // 敵だった場合
            {
                // 上下左右の入力判定をとりboolをtrueにする
                if (Input.GetKeyDown(KeyCode.W))
                {
                    // 敵を倒す（MapGenarator.csのMAP_TYPEをENEMYからGROUND書き換える）
                    Attack();
                    enemyManager.Hit();
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    Attack();
                    enemyManager.Hit();
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    Attack();
                    enemyManager.Hit();
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    Attack();
                    enemyManager.Hit();
                }
            }

            if (mapGenerator.GetStageMapType(playerCurrentPos) == MapGenerator.MAP_TYPE.STAIRS)
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

                mapGenerator._loadMapData();
                mapGenerator._createMap();
            }
            */
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

            mapGenerator._loadMapData();
            mapGenerator._createMap();
        }
    }
    public IEnumerator Slash()
    {
        Debug.Log("斬撃");
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
