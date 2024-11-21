using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toshiEnemy : MonoBehaviour
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
    MapGenerator mapGenerator;
    NotesManager notesManager = null;
    EnemyManager enemyManager;
    GameObject leftNotes;
    GameObject rightNotes;
    GameObject function;
    int count = 0;
    public bool isEnemyAttack = false;

    void Start()
    {
        mapGenerator = transform.parent.GetComponent<MapGenerator>();
        notesManager = GetComponent<NotesManager>();
        enemyManager = GetComponent<EnemyManager>();
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
            if (notesManager.enemyCanMove)
            {
                switch (direction)
                {
                    case DIRECTION.TOP:
                        eMoveType();
                        direction = DIRECTION.DOWN;
                        break;
                    case DIRECTION.DOWN:
                        eMoveType();
                        direction = DIRECTION.TOP;
                        break;
                }
            }
        }
        
    }
    void eMoveType()
    {
        if (notesManager != null && notesManager.CanInputKey())
        {
            enemyManager.enemyNextPos = enemyManager.enemyCurrentPos + new Vector2Int(move[(int)direction, 0],
                move[(int)direction, 1]);

            if (mapGenerator.GetEnemyNextMapType(enemyManager.enemyNextPos) == MapGenerator.MAP_TYPE.WALL)
            {
                // �������Ȃ�
            }
            else if (mapGenerator.GetEnemyNextMapType(enemyManager.enemyNextPos) == MapGenerator.MAP_TYPE.PLAYER)
            {
                // �v���C���[�ɍU������
                isEnemyAttack = true;
            }
            else if (mapGenerator.GetEnemyNextMapType(enemyManager.enemyNextPos) != MapGenerator.MAP_TYPE.WALL)
            {
                //�ړ�
                mapGenerator.UpdateTilie(enemyManager.enemyCurrentPos, MapGenerator.MAP_TYPE.GROUND);
                transform.localPosition = mapGenerator.ScreenPos(enemyManager.enemyNextPos);
                enemyManager.enemyCurrentPos = enemyManager.enemyNextPos;
                mapGenerator.UpdateTilie(enemyManager.enemyCurrentPos, MapGenerator.MAP_TYPE.ENEMY);
            }
        }
    }
    
}
