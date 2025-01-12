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
      { 0, -1 },　//TOPの場合
      { 1, 0 },   //RIGHTの場合
      { 0, 1 },   //DOWNの場合
      { -1, 0 }   //LEFTの場合
    };
    public DIRECTION direction;
    RuiMapGenerator ruiMapGenerator = null;
    NotesManager notesManager = null;
    RuiEnemyManager ruiEnemyManager;
    RuiEnemyManager enemyManager;
    RuitoshiPlayer ruiToshiPlayer = null;
    RuiPlayerManager playerManager;
    GameObject leftNotes;
    GameObject rightNotes;
    GameObject function;
    int moveCount = 0;//自分が何回動いたか
    public bool isEnemyAttack = false;

    void Start()
    {
        //ruiMapGenerator = transform.parent.GetComponent<RuiMapGenerator>();
        notesManager = GetComponent<NotesManager>();
        enemyManager = GetComponent<RuiEnemyManager>();
        ruiEnemyManager = GetComponent<RuiEnemyManager>();
        direction = DIRECTION.DOWN;
    }

    float eMoveTime = 0;
    bool isEnemyMove = false;
    float distance;

    // Update is called once per frame
    void Update()
    {
        if (ruiMapGenerator == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("MapChip");
            ruiMapGenerator = inst.GetComponent<RuiMapGenerator>();
        }
        

        if (notesManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("NotesManager");
            notesManager = inst.GetComponent<NotesManager>();
        }
        if (ruiToshiPlayer == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Player");
            ruiToshiPlayer = inst.GetComponent<RuitoshiPlayer>();
        }
        if (playerManager == null)
        {
            GameObject inst = GameObject.Find("PlayerManager");
            playerManager = inst.GetComponent<RuiPlayerManager>();
        }

        //if (notesManager != null && notesManager.CanInputKey())
        //{
        //    count = 1;
        //    if (notesManager.enemyCanMove) // 動けるとき
        //    {
        //        if (count == 0)
        //        {
        //            // cout0の時1マス下
        //            direction = DIRECTION.DOWN;
        //            eMoveType();
        //            notesManager.enemyCanMove = false;

        //        }
        //        if (count == 1)
        //        {
        //            // cout1の時1マス上
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
                        //eMoveType();
                        direction = DIRECTION.DOWN;
                        moveCount++;
                        break;
                    case DIRECTION.DOWN:
                        //eMoveType();
                        direction = DIRECTION.TOP;
                        moveCount++;
                        break;
                }             
            }
            else {notesManager.enemyCanMove = false; moveCount = 0; }
        }

        /*if (Input.GetKeyDown(KeyCode.L))
        {
            ruiEnemyManager.enemyCurrentPos = ruiMapGenerator.SearchRoute(ruiEnemyManager.enemyCurrentPos, ruiToshiPlayer.playerCurrentPos);
            //Debug.Log(ruiEnemyManager.enemyCurrentPos);
            //Debug.Log(ruiMapGenerator.aStarMap[ruiMapGenerator.nextX, ruiMapGenerator.nextY].score <= );
            eMoveType();
        }*/

        //修正予定
        //変数で時間を計って移動させているため、ノーツがハートに触れたら移動するようにする
        distance = Vector2Int.Distance(enemyManager.enemyCurrentPos, ruiToshiPlayer.playerCurrentPos);
        if (distance < 5)
        {
            eMoveTime++;
            if (eMoveTime >= 60)
            {
                isEnemyMove = true;
            }

            if (isEnemyMove)
            {
                ruiEnemyManager.enemyNextPos = ruiMapGenerator.SearchRoute(ruiEnemyManager.enemyCurrentPos, ruiToshiPlayer.playerCurrentPos);
                eMoveType();

                isEnemyMove = false;
                eMoveTime = 0;
            }
        }
    }
    void eMoveType()
    {
        if (notesManager != null && notesManager.CanInputKey())
        {
            //enemyManager.enemyNextPos = enemyManager.enemyCurrentPos + new Vector2Int(move[(int)direction, 0],move[(int)direction, 1]);
            if (ruiMapGenerator.GetEntityMapType(enemyManager.enemyNextPos) == RuiMapGenerator.MAP_TYPE.PLAYER)
            {
                Debug.Log("攻撃エネミー側");
                // プレイヤーに攻撃する
                isEnemyAttack = true;
                //ruiMapGenerator.UpdateTile(enemyManager.enemyCurrentPos, MapGenerator.MAP_TYPE.PLAYER);
                //transform.localPosition = ruiMapGenerator.ScreenPos(enemyManager.enemyNextPos);
                //enemyManager.enemyCurrentPos = enemyManager.enemyNextPos;
                //ruiMapGenerator.UpdateTile(enemyManager.enemyCurrentPos, MapGenerator.MAP_TYPE.ENEMY);
                playerManager.Hit();
            }
            else if (ruiMapGenerator.GetStageMapType(enemyManager.enemyNextPos) != RuiMapGenerator.MAP_TYPE.WALL)
            {
                //移動
                ruiMapGenerator.UpdateTile(enemyManager.enemyCurrentPos, RuiMapGenerator.MAP_TYPE.GROUND);
                transform.localPosition = ruiMapGenerator.ScreenPos(enemyManager.enemyNextPos);
                enemyManager.enemyCurrentPos = enemyManager.enemyNextPos;
                ruiMapGenerator.UpdateTile(enemyManager.enemyCurrentPos, RuiMapGenerator.MAP_TYPE.ENEMY);
            }
        }
    }
    
}
