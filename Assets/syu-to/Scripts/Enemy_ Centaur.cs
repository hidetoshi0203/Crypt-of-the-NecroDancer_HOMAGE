using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
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
      { 0, -1 },　//TOPの場合
      { 1, 0 },   //RIGHTの場合
      { 0, 1 },   //DOWNの場合
      { -1, 0 },   //LEFTの場合
      { 0 , 0 }   //STOP
    };

    public DIRECTION direction;

    MapGenerator mapGenerator;
    NotesManager notesManager = null;
    EnemyManager enemyManager;
    PlayerManager playerManager;
    GameObject leftNotes;
    GameObject rightNotes;
    GameObject function;

    int moveCount = 0;//自分が何回動いたか
    public bool isEnemyAttack = false;

    void Start()
    {
        mapGenerator = transform.parent.GetComponent<MapGenerator>();
        notesManager = GetComponent<NotesManager>();
        enemyManager = GetComponent<EnemyManager>();
        direction = DIRECTION.STOP;
    }

    void Update()
    {
        CheckPlayer();

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
            if (moveCount != 1 && notesManager.enemyCanMove)
            {
                switch (direction)
                {
                    case DIRECTION.RIGHT:
                        enemyManager.enemyNextPos = enemyManager.enemyCurrentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);
                        if (mapGenerator.GetMapType(enemyManager.enemyNextPos) == MapGenerator.MAP_TYPE.WALL)
                        {
                            direction = DIRECTION.LEFT;
                        }
                        else
                        {
                            eMoveType();
                        }
                        
                        moveCount++;
                        break;
                    case DIRECTION.LEFT:
                        enemyManager.enemyNextPos = enemyManager.enemyCurrentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);
                        if (mapGenerator.GetMapType(enemyManager.enemyNextPos) == MapGenerator.MAP_TYPE.WALL)
                        {
                            direction = DIRECTION.RIGHT;
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
            if (mapGenerator.GetEnemyNextMapType(enemyManager.enemyNextPos) == MapGenerator.MAP_TYPE.PLAYER)
            {
                Debug.Log("攻撃エネミー側");
                // プレイヤーに攻撃する
                isEnemyAttack = true;

                playerManager.Hit();
            }
            else if (mapGenerator.GetEnemyNextMapType(enemyManager.enemyNextPos) != MapGenerator.MAP_TYPE.WALL)
            {
                //移動
                mapGenerator.UpdateTile(enemyManager.enemyCurrentPos, MapGenerator.MAP_TYPE.GROUND);
                transform.localPosition = mapGenerator.ScreenPos(enemyManager.enemyNextPos);
                enemyManager.enemyCurrentPos = enemyManager.enemyNextPos;
                mapGenerator.UpdateTile(enemyManager.enemyCurrentPos, MapGenerator.MAP_TYPE.ENEMY2);
            }
        }
    }

    private void CheckPlayer()
    {
        Vector2 origin = transform.position; // Ray開始位置

        // Rayの方向ベクトル
        Vector2[] directions = {
        Vector2.up,    // 上方向
        Vector2.right, // 右方向
        Vector2.down,  // 下方向
        Vector2.left   // 左方向
    };

        string[] directionNames = { "Up", "Right", "Down", "Left" };

        for (int i = 0; i < directions.Length; i++)
        {
            // Raycastを実行
            RaycastHit2D hit = Physics2D.Raycast(origin, directions[i], Mathf.Infinity);

            // DebugでRayを可視化（常に緑色）
            Debug.DrawRay(origin, directions[i] * 10, Color.green);

            // プレイヤーに当たった場合
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                Debug.Log("あたった"); // 当たった方向をログに表示
            }
        }
    }
}
