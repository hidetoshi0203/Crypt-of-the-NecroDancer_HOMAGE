using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuiTD_Zombie : MonoBehaviour
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
    RuiMapGenerator mapGenerator;
    NotesManager notesManager = null;
    RuiEnemyManager enemyManager;
    RuiPlayerManager playerManager;
    GameObject leftNotes;
    GameObject rightNotes;
    GameObject function;
    int moveCount = 0;//自分が何回動いたか
    public bool isEnemyAttack = false;

    void Start()
    {
        mapGenerator = transform.parent.GetComponent<RuiMapGenerator>();
        notesManager = GetComponent<NotesManager>();
        enemyManager = GetComponent<RuiEnemyManager>();
        direction = DIRECTION.TOP;
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
        if (notesManager != null && notesManager.CanInputKey())
        {
            if (moveCount != 1 && notesManager.enemyCanMove)
            {
                switch (direction)
                {
                    case DIRECTION.TOP:
                        enemyManager.enemyNextPos = enemyManager.enemyCurrentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);
                        if (mapGenerator.GetMapType(enemyManager.enemyNextPos) == RuiMapGenerator.MAP_TYPE.WALL)
                        {
                            direction = DIRECTION.DOWN;
                        }
                        else
                        {
                            eMoveType();
                        }

                        moveCount++;
                        break;
                    case DIRECTION.DOWN:
                        enemyManager.enemyNextPos = enemyManager.enemyCurrentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);
                        if (mapGenerator.GetMapType(enemyManager.enemyNextPos) == RuiMapGenerator.MAP_TYPE.WALL)
                        {
                            direction = DIRECTION.TOP;
                        }
                        else
                        {
                            eMoveType();
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
                Debug.Log("攻撃エネミー側");
                // プレイヤーに攻撃する
                isEnemyAttack = true;
                //mapGenerator.UpdateTile(enemyManager.enemyCurrentPos, MapGenerator.MAP_TYPE.PLAYER);
                //transform.localPosition = mapGenerator.ScreenPos(enemyManager.enemyNextPos);
                //enemyManager.enemyCurrentPos = enemyManager.enemyNextPos;
                //mapGenerator.UpdateTile(enemyManager.enemyCurrentPos, MapGenerator.MAP_TYPE.ENEMY2);
                playerManager.Hit();
            }
            else if (mapGenerator.GetEnemyNextMapType(enemyManager.enemyNextPos) != RuiMapGenerator.MAP_TYPE.WALL)
            {
                //移動
                mapGenerator.UpdateTile(enemyManager.enemyCurrentPos, RuiMapGenerator.MAP_TYPE.GROUND);
                transform.localPosition = mapGenerator.ScreenPos(enemyManager.enemyNextPos);
                enemyManager.enemyCurrentPos = enemyManager.enemyNextPos;
                mapGenerator.UpdateTile(enemyManager.enemyCurrentPos, RuiMapGenerator.MAP_TYPE.ENEMY2);
            }
        }
    }
}
