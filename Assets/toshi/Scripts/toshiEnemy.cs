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
      { 0, -1 },　//TOPの場合
      { 1, 0 },   //RIGHTの場合
      { 0, 1 },   //DOWNの場合
      { -1, 0 }   //LEFTの場合
    };
    public DIRECTION direction;
    MapGenerator mapGenerator;
    NotesManager notesManager = null;
    EnemyManager enemyManager;
    GameObject leftNotes;
    GameObject rightNotes;
    GameObject function;
    int count = 0;  

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
        if (notesManager != null && notesManager.CanInputKey())
        {
            count = 1;
            if (notesManager.enemyCanMove)
            {
                if (count == 0)
                {
                    direction = DIRECTION.DOWN;
                    eMoveType();
                    notesManager.enemyCanMove = false;
                    
                }
                if (count == 1)
                {
                    direction = DIRECTION.TOP;
                    eMoveType();
                    notesManager.enemyCanMove = false;
                    count = 0;
                }
            }
        }
        //ノーツを受け取り、trueの時に一回だけ動かす
        //上下移動の敵は1回目（上に１マス）、２回目（下に１マス）をint型のcount変数で管理する
        //１回目の行動ができるときはcountが０の時、２回目はcountが１のときにする
        //１回目の行動が終わるとcountに1++、２回目が終わるとcountを初期化する
    }
    void eMoveType()
    {
        //下の処理だと移動できない
        if (notesManager != null && notesManager.CanInputKey())
        {
            enemyManager.enemyNextPos = enemyManager.enemyCurrentPos + new Vector2Int(move[(int)direction, 0],
                move[(int)direction, 1]);

            if (mapGenerator.GetEnemyNextMapType(enemyManager.enemyNextPos) == MapGenerator.MAP_TYPE.WALL)
            {
                // 何もしない
            }
            else if (mapGenerator.GetEnemyNextMapType(enemyManager.enemyNextPos) == MapGenerator.MAP_TYPE.PLAYER)
            {
                // プレイヤーに攻撃する
            }
            else if (mapGenerator.GetEnemyNextMapType(enemyManager.enemyNextPos) != MapGenerator.MAP_TYPE.WALL)
            {
                //移動
                mapGenerator.UpdateTilie(enemyManager.enemyCurrentPos, MapGenerator.MAP_TYPE.GROUND);
                transform.localPosition = mapGenerator.ScreenPos(enemyManager.enemyNextPos);
                enemyManager.enemyCurrentPos = enemyManager.enemyNextPos;
                mapGenerator.UpdateTilie(enemyManager.enemyCurrentPos, MapGenerator.MAP_TYPE.ENEMY);
            }
        }
    }
    
}
