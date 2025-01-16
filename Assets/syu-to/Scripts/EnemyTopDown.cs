using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTopDown : MonoBehaviour
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
    PlayerManager playerManager;
    GameObject leftNotes;
    GameObject rightNotes;
    GameObject function;
    int moveCount = 0;//���������񓮂�����
    public bool isEnemyAttack = false;
    private bool isMove = false;

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
        if (playerManager == null)
        {
            GameObject inst = GameObject.Find("PlayerManager");
            playerManager = inst.GetComponent<PlayerManager>();
        }

        if (notesManager != null && notesManager.CanInputKey())
        {
            
            if (notesManager.enemyCanMove)
            {
                if(isMove) return;
                else
                {
                    isMove = true;
                    moveCount =(moveCount + 1) % 2;

                    if (moveCount == 0) return;
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
            else
            {
                isMove = false;
            }
        }
        
    }
    void eMoveType()
    {
        if (notesManager != null && notesManager.CanInputKey())
        {
            enemyManager.enemyNextPos = enemyManager.enemyCurrentPos + new Vector2Int(move[(int)direction, 0],move[(int)direction, 1]);
            if (mapGenerator.GetEntityMapType(enemyManager.enemyNextPos) == MapGenerator.MAP_TYPE.PLAYER)
            {
                Debug.Log("�U���G�l�~�[��");
                // �v���C���[�ɍU������
                isEnemyAttack = true;
                playerManager.Hit();
            }
            else if (mapGenerator.GetStageMapType(enemyManager.enemyNextPos) != MapGenerator.MAP_TYPE.WALL ||
                            mapGenerator.GetStageMapType(enemyManager.enemyNextPos) != MapGenerator.MAP_TYPE.WALL2)
            {
                //�ړ�
                mapGenerator.UpdateTile(enemyManager.enemyCurrentPos, MapGenerator.MAP_TYPE.GROUND);
                transform.localPosition = mapGenerator.ScreenPos(enemyManager.enemyNextPos);
                enemyManager.enemyCurrentPos = enemyManager.enemyNextPos;
                mapGenerator.UpdateTile(enemyManager.enemyCurrentPos, MapGenerator.MAP_TYPE.ENEMY);
            }
        }
    }
    
}
