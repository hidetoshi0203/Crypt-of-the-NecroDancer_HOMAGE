using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;
using static UnityEditor.Progress;
public class RuitoshiPlayer : MonoBehaviour
{
    public enum DIRECTION
    {
        TOP,
        RIGHT,
        DOWN,
        LEFT,
    }

    int[,] move = {
      { 0, -1 },�@//TOP�̏ꍇ
      { 1, 0 },   //RIGHT�̏ꍇ
      { 0, 1 },   //DOWN�̏ꍇ
      { -1, 0 },  //LEFT�̏ꍇ
    };

    public DIRECTION direction;
    public Vector2Int playerCurrentPos, playerNextPos;
    public bool isAttack;
    public bool isPlayerMove;
    Camera cam;
    RuiMapGenerator mapGenerator;
    NotesManager notesManager = null;
    ComboManager comboManager = null;
    GameObject leftNotes;
    GameObject rightNotes;
    GameObject function;
    GameObject notesObjets;

    RuiPlayerManager ruiPlayerManager = null;
    Item itemHPotion = null;
    Item itemSPotion = null;

    public float playerAttackPower = 1;
    private void Start()
    {
        mapGenerator = transform.parent.GetComponent<RuiMapGenerator>();
        notesManager = GetComponent<NotesManager>();
        comboManager = GetComponent<ComboManager>();
        direction = DIRECTION.DOWN;
        notesObjets = GameObject.FindGameObjectWithTag("Notes");
        cam = Camera.main;
        cam.transform.position = transform.position + new Vector3(0,0,-1);

        ruiPlayerManager = GetComponent<RuiPlayerManager>();
    }


    private void FixedUpdate()
    {
        cam.transform.position = transform.position + new Vector3(0, 0, -1);
    }

    //�@���͎���_move�֐����ĂԂ悤�ɂ���B
    private void Update()
    {
        if (ruiPlayerManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("PlayerManager");
            ruiPlayerManager = inst.GetComponent<RuiPlayerManager>();
        }

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
        //if (itemHPotion == null)
        //{
        //    GameObject inst = GameObject.FindGameObjectWithTag("HealingPotion");
        //    itemHPotion = inst.GetComponent<Item>();
        //}
        if (itemSPotion == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("StrengthPotion");
            itemSPotion = inst.GetComponent<Item>();
        }
        if (notesManager != null && notesManager.CanInputKey())
        {
            if (notesManager.playerCanMove)
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
    }

    private void HandlePlayerMove(DIRECTION directionInput)
    {
        direction = directionInput;
        moveType();

        notesManager.StopTouchSound();
    }

    //�ړ��p�̊֐�
    void moveType()
    {
        
        if (notesManager != null && notesManager.CanInputKey())
        {
            playerNextPos = playerCurrentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);

            switch (mapGenerator.GetStageMapType(playerNextPos))
            {
                case RuiMapGenerator.MAP_TYPE.GROUND:
                    Move();
                    break;
                case RuiMapGenerator.MAP_TYPE.HEALINGPOTION:
                    Heal();
                    mapGenerator.GetItem(playerNextPos);
                    Move();
                    break;
                case RuiMapGenerator.MAP_TYPE.STRENGTHPOTION:
                    // �v���C���[�̍U���͂��グ��֐�
                    mapGenerator.GetItem(playerNextPos);
                    Move();
                    break;
                case RuiMapGenerator.MAP_TYPE.WALL:
                    // �������Ȃ��i��X���̏�ŃW�����v����悤�ȃA�j���[�V����������j
                    break;
                case RuiMapGenerator.MAP_TYPE.STAIRS:
                    // ���̃X�e�[�W�ɐi��
                    break;
                case RuiMapGenerator.MAP_TYPE.WALL2:
                    // �������Ȃ��i��X���̏�ŃW�����v����悤�ȃA�j���[�V����������j
                    break;
            }

            switch (mapGenerator.GetEntityMapType(playerNextPos))
            {
                case RuiMapGenerator.MAP_TYPE.PLAYER:
                    // �o�O
                    break;
                case RuiMapGenerator.MAP_TYPE.ENEMY:
                    // �U��
                    Attack();
                    break;
                case RuiMapGenerator.MAP_TYPE.ENEMY2:
                    // �U��

                    break;
            }

            //switch (mapGenerator.GetItemMapType(playerNextPos))
            //{
            //    case RuiMapGenerator.MAP_TYPE.HEALINGPOTION:
            //        Move();
            //        itemHPotion.HealingHP();
            //        break;
            //}

            //if (mapGenerator.GetPlayerNextMapType(playerNextPos) == RuiMapGenerator.MAP_TYPE.WALL && mapGenerator.GetPlayerNextMapType(playerNextPos) == RuiMapGenerator.MAP_TYPE.WALL2) // ���͐�(�v���C���[��nextPos)���ǂ������ꍇ
            //{
            //    
            //}
            if (mapGenerator.GetEntityMapType(playerNextPos) == RuiMapGenerator.MAP_TYPE.ENEMY) // �G�������ꍇ
            {
                // �㉺���E�̓��͔�����Ƃ�bool��true�ɂ���
                if (Input.GetKeyDown(KeyCode.W))
                {
                    isAttack = true; // EnemyManager.cs��true���󂯎��A�G��|���iMapGenarator.cs��MAP_TYPE��ENEMY����GROUND����������j
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    isAttack = true;
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    isAttack = true;
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    isAttack = true;
                }
            }

            if (mapGenerator.GetStageMapType(playerCurrentPos) == RuiMapGenerator.MAP_TYPE.STAIRS)
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
        void Attack()
        {

        }

        void Heal() // �v���C���[���񕜂���֐�
        {
            if (ruiPlayerManager.playerHP < 3) // �v���C���[�̗̑͂������Ă���(3HP������������)
            {
                ruiPlayerManager.playerHP++; // �v���C���[�̗�(HP)���񕜂���
            }
        }

        void playerPowerAttackUp()
        {
            playerAttackPower++;
        }

        void Move()
        {
            //Debug.Log(playerCurrentPos);
            //Debug.Log("������");
            // �ړ�����
            mapGenerator.UpdateTile(playerCurrentPos, RuiMapGenerator.MAP_TYPE.GROUND); // �����̍��W��MAP_TYPE��GROUND�ɂ���
            transform.localPosition = mapGenerator.ScreenPos(playerNextPos);          // �ړ�
            playerCurrentPos = playerNextPos;
            mapGenerator.UpdateTile(playerCurrentPos, RuiMapGenerator.MAP_TYPE.PLAYER); // �����̍��W��MAP_TYPE��PLAYER�ɂ���
        }
    }
}
