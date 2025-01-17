using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;
public class toshiPlayer : MonoBehaviour
{
    public enum DIRECTION
    {
        TOP,
        RIGHT,
        DOWN,
        LEFT
    }

    int[,] move = {
      { 0, -1 },�@//TOP�̏ꍇ
      { 1, 0 },   //RIGHT�̏ꍇ
      { 0, 1 },   //DOWN�̏ꍇ
      { -1, 0 }   //LEFT�̏ꍇ
    };

    public DIRECTION direction;
    public Vector2Int playerCurrentPos, playerNextPos;
    public bool isAttack;
    public bool isPlayerMove;
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
    private void Start()
    {
        mapGenerator = transform.parent.GetComponent<MapGenerator>();
        direction = DIRECTION.DOWN;
        notesObjets = GameObject.FindGameObjectWithTag("Notes");
        cam = Camera.main;
        cam.transform.position = transform.position + new Vector3(0,0,-1);
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

    //�ړ��p�̊֐�
    void moveType()
    {
        
        if (notesManager != null && notesManager.CanInputKey())
        {
            playerNextPos = playerCurrentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);
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
                    break;
                case MapGenerator.MAP_TYPE.ENEMY2:
                    // �U��
                    Debug.Log("�U��");
                    enemySystem.Hit(playerNextPos);
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



}
