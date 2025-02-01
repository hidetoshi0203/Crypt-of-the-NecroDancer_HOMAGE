using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine.UI;
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
      new Vector2Int(  0, -1 ),�@//TOP�̏ꍇ
      new Vector2Int(  1,  0 ),   //RIGHT�̏ꍇ
      new Vector2Int(  0,  1 ),   //DOWN�̏ꍇ
      new Vector2Int( -1,  0 )   //LEFT�̏ꍇ
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
    public float playerAttackPower = 1; // �v���C���[�̍U����
    private bool isPowerUp = false; // �v���C���[�̍U���͂̃t���O(�v���C���[���U����UP�|�[�V�������������)

    public float powerUpTimer; // �v���C���[�̍U����UP�̌��ʎ���
    public float powerUpTimerEnd = 20.0f; // �v���C���[�U����UP�̌��ʂ��؂�鎞��
    public bool isPowerUpTimer = false; // �v���C���[�U����UP�̌��ʎ��Ԃ̃t���O
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

    //�@���͎���_move�֐����ĂԂ悤�ɂ���B
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
        playerAttackPowerUpTimer();
    }

    private void HandlePlayerMove(DIRECTION directionInput)
    {
        direction = directionInput;
        moveType();
        notesManager.PlayerInputKey();
        notesManager.StopTouchSound();
    }

    //�ړ��p�̊֐�
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
                //    // �o�O
                //    break;
                case MapGenerator.MAP_TYPE.ENEMY:
                    // �U��
                    Debug.Log("�U��");
                    enemySystem.Hit(playerNextPos);
                    StartCoroutine("Slash");
                    playerAttackSound.AttackSound();
                    break;
                case MapGenerator.MAP_TYPE.ENEMY2:
                    // �U��
                    Debug.Log("�U��");
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
                        // �������Ȃ��i��X���̏�ŃW�����v����悤�ȃA�j���[�V����������j
                        break;
                    case MapGenerator.MAP_TYPE.STAIRS:
                        // ���̃X�e�[�W�ɐi��
                        Stairs();
                        break;
                    case MapGenerator.MAP_TYPE.WALL2:
                        // �������Ȃ��i��X���̏�ŃW�����v����悤�ȃA�j���[�V����������j
                        break;
                    case MapGenerator.MAP_TYPE.HEALINGPOTION: // �v���C���[���񕜃|�[�V�������������
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
            

            
            /*
            //if (mapGenerator.GetPlayerNextMapType(playerNextPos) == MapGenerator.MAP_TYPE.WALL && mapGenerator.GetPlayerNextMapType(playerNextPos) == MapGenerator.MAP_TYPE.WALL2) // ���͐�(�v���C���[��nextPos)���ǂ������ꍇ
            //{
            //    
            //}
            
            if (mapGenerator.GetEntityMapType(playerNextPos) == MapGenerator.MAP_TYPE.ENEMY) // �G�������ꍇ
            {
                // �㉺���E�̓��͔�����Ƃ�bool��true�ɂ���
                if (Input.GetKeyDown(KeyCode.W))
                {
                    // �G��|���iMapGenarator.cs��MAP_TYPE��ENEMY����GROUND����������j
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
                Debug.Log("�K�i�̏ゾ��");
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
            //Debug.Log("������");
            // �ړ�����
            mapGenerator.UpdateTile(playerCurrentPos, MapGenerator.MAP_TYPE.GROUND); // �����̍��W��MAP_TYPE��GROUND�ɂ���
            transform.localPosition = mapGenerator.ScreenPos(playerNextPos);          // �ړ�
            playerCurrentPos = playerNextPos;
            mapGenerator.UpdateTile(playerCurrentPos, MapGenerator.MAP_TYPE.PLAYER); // �����̍��W��MAP_TYPE��PLAYER�ɂ���
        }
        void Stairs()
        {
            Debug.Log("�K�i�̏ゾ��");
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

    void Heal() // �v���C���[���񕜂���֐�
    {
        if (playerManager.playerHP < 3) // �v���C���[�̗̑͂������Ă���(3HP������������)
        {
            playerManager.playerHP++; // �v���C���[�̗�(HP)���񕜂���
            playerManager.lifeArray[playerManager.playerHP - 1].GetComponent<Image>().enabled = true;
        }
    }

    void playerAttackPowerUp() // �v���C���[�̍U���͂��オ��֐�
    {
        isPowerUpTimer = true; // true�ɂ��ăv���C���[�̍U����UP�̌��ʎ��Ԃ𐔂��n�߂�
        isPowerUp = true;
        audioSource.PlayOneShot(getSPotionSound);
        sPotionEffect.Play(); // �U����UP�|�[�V�����̃G�t�F�N�g���n�߂�

        if (isPowerUp) // �U����UP�|�[�V�������������A
        {
            playerAttackPower++; // �v���C���[�̍U���͂��グ��
            isPowerUp = false; // isPowerUp��false�ɂ��āA�v���C���[�̍U���͂��ߓx�ɏグ�Ȃ��悤�ɂ��Ă���
        }
    }

    void playerAttackPowerUpTimer() // �v���C���[�̍U����UP�̌��ʎ��Ԃ̊֐�
    {
        if (isPowerUpTimer)
        {
            powerUpTimer += Time.deltaTime; // �v���C���[�̍U����UP�̌��ʎ��Ԃ𐔂���
        }

        if (powerUpTimer >= powerUpTimerEnd) // ���ʎ��Ԃ�POWERUPTIMEREND�܂ł�������A
        {
            isPowerUpTimer = false; // false�ɂ��Č��ʎ��Ԃ𐔂���̂��I���
            powerUpTimer = 0.0f; // ���ʎ��Ԃ�����������
            playerAttackPower--; // �U���͂����ɖ߂�
            sPotionEffect.Stop();
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
