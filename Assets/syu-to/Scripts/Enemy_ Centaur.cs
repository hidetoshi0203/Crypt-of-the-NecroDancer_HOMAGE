using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;

public class Enemy_Centaur : MonoBehaviour
{
    public enum DIRECTION
    {
        TOP,
        RIGHT,
        DOWN,
        LEFT,
        STOP
    }

    int[,] move = {
      { 0, -1 },�@//TOP�̏ꍇ
      { 1, 0 },   //RIGHT�̏ꍇ
      { 0, 1 },   //DOWN�̏ꍇ
      { -1, 0 },   //LEFT�̏ꍇ
      { 0 , 0 }   //STOP
    };

    public DIRECTION direction;

    MapGenerator mapGenerator;
    NotesManager notesManager = null;
    EnemyManager enemyManager;
    PlayerManager playerManager;
    toshiPlayer toshiPlayer;
    GameObject leftNotes;
    GameObject rightNotes;
    GameObject function;

    int moveCount = 0;//���������񓮂�����
    public bool isEnemyAttack = false;

    public bool check = false;

    void Start()
    {
        mapGenerator = transform.parent.GetComponent<MapGenerator>();
        notesManager = GetComponent<NotesManager>();
        enemyManager = GetComponent<EnemyManager>();
        direction = DIRECTION.STOP;
        check = true;
    }

    void Update()
    {
        if (notesManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("NotesManager");
            notesManager = inst.GetComponent<NotesManager>();
        }
        if (playerManager == null)
        {
            GameObject inst = GameObject.Find("PlayerManager");
            playerManager = inst.GetComponent<PlayerManager>();
        }
        if (toshiPlayer == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Player");
            toshiPlayer = inst.GetComponent<toshiPlayer>();
        }
        if (check)
        {
            CheckPlayer();
        }
        if (notesManager != null && notesManager.CanInputKey())
        {
            if (moveCount != 1 && notesManager.enemyCanMove)
            {
                switch (direction)
                {
                    case DIRECTION.RIGHT:
                        enemyManager.enemyNextPos = enemyManager.enemyCurrentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);
                        if (mapGenerator.GetStageMapType(enemyManager.enemyNextPos) == MapGenerator.MAP_TYPE.WALL ||
                            mapGenerator.GetStageMapType(enemyManager.enemyNextPos) == MapGenerator.MAP_TYPE.WALL2)
                        {
                            direction = DIRECTION.STOP;
                            check = true;
                        }
                        else
                        {
                            eMoveType();
                            check = false;
                        }
                        
                        moveCount++;
                        break;
                    case DIRECTION.LEFT:
                        enemyManager.enemyNextPos = enemyManager.enemyCurrentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);
                        if (mapGenerator.GetStageMapType(enemyManager.enemyNextPos) == MapGenerator.MAP_TYPE.WALL || 
                            mapGenerator.GetStageMapType(enemyManager.enemyNextPos) == MapGenerator.MAP_TYPE.WALL2)
                        {
                            direction = DIRECTION.STOP;
                            check = true;
                        }
                        else
                        {
                            eMoveType();
                            check = false;
                        }
                        moveCount++;
                        break;
                    case DIRECTION.TOP:
                        enemyManager.enemyNextPos = enemyManager.enemyCurrentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);
                        if (mapGenerator.GetStageMapType(enemyManager.enemyNextPos) == MapGenerator.MAP_TYPE.WALL || 
                            mapGenerator.GetStageMapType(enemyManager.enemyNextPos) == MapGenerator.MAP_TYPE.WALL2)
                        {
                            direction = DIRECTION.STOP;
                            check = true;
                        }
                        else
                        {
                            eMoveType();
                            check = false;
                        }
                        
                        moveCount++;
                        break;
                    case DIRECTION.DOWN:
                        enemyManager.enemyNextPos = enemyManager.enemyCurrentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);
                        if (mapGenerator.GetStageMapType(enemyManager.enemyNextPos) == MapGenerator.MAP_TYPE.WALL || 
                            mapGenerator.GetStageMapType(enemyManager.enemyNextPos) == MapGenerator.MAP_TYPE.WALL2)
                        {
                            direction = DIRECTION.STOP;
                            check = true;
                        }
                        else
                        {
                            eMoveType();
                            check = false;
                        }
                        moveCount++;
                        break;
                }
            }
            else { notesManager.enemyCanMove = false; moveCount = 0; }
        }
        
    }

    void eMoveType()
    {
        if (notesManager != null && notesManager.CanInputKey())
        {
            enemyManager.enemyNextPos = enemyManager.enemyCurrentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);
            if (mapGenerator.GetEntityMapType(enemyManager.enemyNextPos) == MapGenerator.MAP_TYPE.PLAYER)
            {
                Debug.Log("�U���G�l�~�[��");
                // �v���C���[�ɍU������
                isEnemyAttack = true;

                StartCoroutine(playerManager.Damage());
            }
            else if (mapGenerator.GetStageMapType(enemyManager.enemyNextPos) != MapGenerator.MAP_TYPE.WALL ||
                            mapGenerator.GetStageMapType(enemyManager.enemyNextPos) != MapGenerator.MAP_TYPE.WALL2)
            {
                //�ړ�
                mapGenerator.UpdateTile(enemyManager.enemyCurrentPos, MapGenerator.MAP_TYPE.GROUND);
                transform.localPosition = mapGenerator.ScreenPos(enemyManager.enemyNextPos);
                enemyManager.enemyCurrentPos = enemyManager.enemyNextPos;
                mapGenerator.UpdateTile(enemyManager.enemyCurrentPos, MapGenerator.MAP_TYPE.ENEMY2);
            }
        }
    }

    [SerializeField] float detectionRange = 5.0f;  // ���G�͈͂� 5 ���j�b�g�ɐ���
    private void CheckPlayer()
    {
        Vector2 origin = transform.position; // Ray�J�n�ʒu

        // Ray�̕����x�N�g��
        Vector2[] directions = {
        Vector2.up,    // �����
        Vector2.right, // �E����
        Vector2.down,  // ������
        Vector2.left   // ������
        };

        Vector2Int enemy = enemyManager.enemyCurrentPos;
        Vector2Int player = toshiPlayer.playerCurrentPos;
        Debug.Log(enemy + " : " + player);
        if (enemy.x == player.x)
        {
            float pPos = enemy.y - player.y;
            if (Mathf.Abs(pPos) < 3)
            {
                if (pPos > 0)
                {
                    direction = DIRECTION.TOP;
                }
                else
                {
                    direction = DIRECTION.DOWN;
                }

            }
        }
        else if (enemy.y == player.y)
        {
            float pPos = enemy.x - player.x;
            if (Mathf.Abs(pPos) < 3)
            {
                if (pPos > 0)
                {
                    direction = DIRECTION.LEFT;
                }
                else
                {
                    direction = DIRECTION.RIGHT;
                }

            }
        }
        //for (int i = 0; i < directions.Length; i++)
        //{
        //    //Ray���΂�
        //    Ray2D ray = new Ray2D(origin, directions[i]);
        //    // Raycast�����s
        //    RaycastHit2D hit = Physics2D.Raycast(origin, directions[i], detectionRange);

        //    // Debug��Ray�������i��ɗΐF�j
        //    Debug.DrawRay(origin, directions[i] * detectionRange, Color.green);

        //    //Debug.Log(hit.collider.name);

        //    // �v���C���[�ɓ��������ꍇ
        //    if (hit.collider != null && hit.collider.CompareTag("Player"))
        //    {
        //        Debug.Log($"{direction}����");
        //        direction = (DIRECTION)i;
        //    }

        //}

    }
}
