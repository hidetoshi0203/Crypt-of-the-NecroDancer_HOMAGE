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
    toshiPlayer toshiPlayer;
    GameObject leftNotes;
    GameObject rightNotes;
    GameObject function;

    PlayerDamageSound playerDamageSound;

    int moveCount = 0;//自分が何回動いたか
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

        if (playerDamageSound == null)
        {
            GameObject inst = GameObject.Find("PlayerDamageSound");
            playerDamageSound = inst.GetComponent<PlayerDamageSound>();
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
                Debug.Log("攻撃エネミー側");
                playerDamageSound.DamageSound();
                // プレイヤーに攻撃する
                isEnemyAttack = true;

                StartCoroutine(playerManager.Damage());
            }
            else if (mapGenerator.GetStageMapType(enemyManager.enemyNextPos) != MapGenerator.MAP_TYPE.WALL ||
                            mapGenerator.GetStageMapType(enemyManager.enemyNextPos) != MapGenerator.MAP_TYPE.WALL2)
            {
                //移動
                mapGenerator.UpdateTile(enemyManager.enemyCurrentPos, MapGenerator.MAP_TYPE.GROUND);
                transform.localPosition = mapGenerator.ScreenPos(enemyManager.enemyNextPos);
                enemyManager.enemyCurrentPos = enemyManager.enemyNextPos;
                mapGenerator.UpdateTile(enemyManager.enemyCurrentPos, MapGenerator.MAP_TYPE.ENEMY2);
            }
        }
    }

    [SerializeField] float detectionRange = 5.0f;  // 索敵範囲を 5 ユニットに制限
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
        //    //Rayを飛ばす
        //    Ray2D ray = new Ray2D(origin, directions[i]);
        //    // Raycastを実行
        //    RaycastHit2D hit = Physics2D.Raycast(origin, directions[i], detectionRange);

        //    // DebugでRayを可視化（常に緑色）
        //    Debug.DrawRay(origin, directions[i] * detectionRange, Color.green);

        //    //Debug.Log(hit.collider.name);

        //    // プレイヤーに当たった場合
        //    if (hit.collider != null && hit.collider.CompareTag("Player"))
        //    {
        //        Debug.Log($"{direction}方向");
        //        direction = (DIRECTION)i;
        //    }

        //}

    }
}
