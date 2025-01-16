using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuiCentaur : MonoBehaviour
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

    RuiMapGenerator mapGenerator;
    NotesManager notesManager = null;
    RuiEnemyManager enemyManager;
    RuiPlayerManager playerManager;
    GameObject leftNotes;
    GameObject rightNotes;
    GameObject function;

    int moveCount = 0;//���������񓮂�����
    public bool isEnemyAttack = false;

    public bool check = false;

    void Start()
    {
        mapGenerator = transform.parent.GetComponent<RuiMapGenerator>();
        notesManager = GetComponent<NotesManager>();
        enemyManager = GetComponent<RuiEnemyManager>();
        direction = DIRECTION.STOP;
        check = true;
    }

    void Update()
    {
        if (check)
        {
            CheckPlayer();
        }


        if (notesManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("NotesManager");
            notesManager = inst.GetComponent<NotesManager>();
        }
        if (playerManager == null)
        {
            GameObject inst = GameObject.Find("PlayerManager");
            playerManager = inst.GetComponent<RuiPlayerManager>();
        }

        if (notesManager != null && notesManager.CanInputKey())
        {
            if (moveCount != 1 && notesManager.enemyCanMove)
            {
                switch (direction)
                {
                    case DIRECTION.RIGHT:
                        enemyManager.enemyNextPos = enemyManager.enemyCurrentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);
                        if (mapGenerator.GetMapType(enemyManager.enemyNextPos) == RuiMapGenerator.MAP_TYPE.WALL)
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
                        if (mapGenerator.GetMapType(enemyManager.enemyNextPos) == RuiMapGenerator.MAP_TYPE.WALL)
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
                        if (mapGenerator.GetMapType(enemyManager.enemyNextPos) == RuiMapGenerator.MAP_TYPE.WALL)
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
                        if (mapGenerator.GetMapType(enemyManager.enemyNextPos) == RuiMapGenerator.MAP_TYPE.WALL)
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
            if (mapGenerator.GetEnemyNextMapType(enemyManager.enemyNextPos) == RuiMapGenerator.MAP_TYPE.PLAYER)
            {
                Debug.Log("�U���G�l�~�[��");
                // �v���C���[�ɍU������
                isEnemyAttack = true;

                playerManager.Hit();
            }
            else if (mapGenerator.GetEnemyNextMapType(enemyManager.enemyNextPos) != RuiMapGenerator.MAP_TYPE.WALL)
            {
                //�ړ�
                mapGenerator.UpdateTile(enemyManager.enemyCurrentPos, RuiMapGenerator.MAP_TYPE.GROUND);
                transform.localPosition = mapGenerator.ScreenPos(enemyManager.enemyNextPos);
                enemyManager.enemyCurrentPos = enemyManager.enemyNextPos;
                mapGenerator.UpdateTile(enemyManager.enemyCurrentPos, RuiMapGenerator.MAP_TYPE.ENEMY2);
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

        for (int i = 0; i < directions.Length; i++)
        {
            //Ray���΂�
            Ray2D ray = new Ray2D(origin, directions[i]);
            // Raycast�����s
            RaycastHit2D hit = Physics2D.Raycast(origin, directions[i], detectionRange);

            // Debug��Ray�������i��ɗΐF�j
            Debug.DrawRay(origin, directions[i] * detectionRange, Color.green);


            // �v���C���[�ɓ��������ꍇ
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                Debug.Log($"{direction}����");
                direction = (DIRECTION)i;
            }

        }

    }
}

