using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuitoshiEnemy : MonoBehaviour
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
    RuiMapGenerator mapGenerator;
    NotesManager notesManager = null;
    RuiEnemyManager enemyManager;
    RuiPlayerManager playerManager;
    GameObject leftNotes;
    GameObject rightNotes;
    GameObject function;
    int moveCount = 0;//���������񓮂�����
    public bool isEnemyAttack = false;

    void Start()
    {
        mapGenerator = transform.parent.GetComponent<RuiMapGenerator>();
        notesManager = GetComponent<NotesManager>();
        enemyManager = GetComponent<RuiEnemyManager>();
        direction = DIRECTION.DOWN;
    }
    // Update is called once per frame
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
            playerManager = inst.GetComponent<RuiPlayerManager>();
        }

        //if (notesManager != null && notesManager.CanInputKey())
        //{
        //    count = 1;
        //    if (notesManager.enemyCanMove) // ������Ƃ�
        //    {
        //        if (count == 0)
        //        {
        //            // cout0�̎�1�}�X��
        //            direction = DIRECTION.DOWN;
        //            eMoveType();
        //            notesManager.enemyCanMove = false;

        //        }
        //        if (count == 1)
        //        {
        //            // cout1�̎�1�}�X��
        //            direction = DIRECTION.TOP;
        //            eMoveType();
        //            notesManager.enemyCanMove = false;
        //            count = 0;
        //        }
        //    }
        //}
        if (notesManager != null && notesManager.CanInputKey())
        {
            if (moveCount != 1 && notesManager.enemyCanMove)
            {
                switch (direction)
                {
                    case DIRECTION.TOP:
                        eMoveType();
                        direction = DIRECTION.DOWN;
                        moveCount++;
                        break;
                    case DIRECTION.DOWN:
                        eMoveType();
                        direction = DIRECTION.TOP;
                        moveCount++;
                        break;
                }             
            }
            else {notesManager.enemyCanMove = false; moveCount = 0; }
        }
        
    }
    void eMoveType()
    {
        if (notesManager != null && notesManager.CanInputKey())
        {
            enemyManager.enemyNextPos = enemyManager.enemyCurrentPos + new Vector2Int(move[(int)direction, 0],move[(int)direction, 1]);
            if (mapGenerator.GetEntityMapType(enemyManager.enemyNextPos) == RuiMapGenerator.MAP_TYPE.PLAYER)
            {
                Debug.Log("�U���G�l�~�[��");
                // �v���C���[�ɍU������
                isEnemyAttack = true;
                //mapGenerator.UpdateTile(enemyManager.enemyCurrentPos, MapGenerator.MAP_TYPE.PLAYER);
                //transform.localPosition = mapGenerator.ScreenPos(enemyManager.enemyNextPos);
                //enemyManager.enemyCurrentPos = enemyManager.enemyNextPos;
                //mapGenerator.UpdateTile(enemyManager.enemyCurrentPos, MapGenerator.MAP_TYPE.ENEMY);
                playerManager.Hit();
            }
            else if (mapGenerator.GetStageMapType(enemyManager.enemyNextPos) != RuiMapGenerator.MAP_TYPE.WALL)
            {
                //�ړ�
                mapGenerator.UpdateTile(enemyManager.enemyCurrentPos, RuiMapGenerator.MAP_TYPE.GROUND);
                transform.localPosition = mapGenerator.ScreenPos(enemyManager.enemyNextPos);
                enemyManager.enemyCurrentPos = enemyManager.enemyNextPos;
                mapGenerator.UpdateTile(enemyManager.enemyCurrentPos, RuiMapGenerator.MAP_TYPE.ENEMY);
            }
        }
    }
    
}
