using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rtoshiEnemy : MonoBehaviour
{
    public enum DIRECTION
    {
        TOP,
        DOWN,
    }

    int[,] move = {
      { 0, -1 },　//TOPの場合
      { 0, 1 },   //DOWNの場合
    };
    public DIRECTION direction;
    public Vector2Int eCurrentPos, eNextPos;
    rMapGenerator mapGenerator;
    NotesManager notesManager = null;
    EnemyManager enemyManager;
    GameObject leftNotes;
    GameObject rightNotes;
    GameObject function;
    float count = 0;  

    void Start()
    {
        mapGenerator = transform.parent.GetComponent<rMapGenerator>();
        notesManager = GetComponent<NotesManager>();
        direction = DIRECTION.DOWN;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(eNextPos);
        //Debug.Log(count);
        if (notesManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("NotesManager");
            notesManager = inst.GetComponent<NotesManager>();
        }
        if (notesManager != null && notesManager.CanInputKey())
        {
            count++;
            if (notesManager.enemyCanMove)
            {
                if (0 <= count && count <= 60)
                {
                    direction = DIRECTION.DOWN;
                    eMoveType();
                    notesManager.StopTouchSound();
                    notesManager.enemyCanMove = false;
                }
                if (60 <= count && count <= 120)
                {
                    direction = DIRECTION.TOP;
                    eMoveType();
                    notesManager.StopTouchSound();
                    notesManager.enemyCanMove = false;
                }
                if (120 <= count)
                {
                    count = 0;
                    notesManager.enemyCanMove = false;

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
            eNextPos = eCurrentPos + new Vector2Int(move[(int)direction, 0],
                move[(int)direction, 1]);

            if (mapGenerator.GetEnemyNextMapType(eNextPos) == rMapGenerator.MAP_TYPE.WALL)
            {
                // 何もしない
            }
            else if (mapGenerator.GetEnemyNextMapType(eNextPos) == rMapGenerator.MAP_TYPE.PLAYER)
            {
                // プレイヤーに攻撃する
            }
            else if (mapGenerator.GetEnemyNextMapType(eNextPos) != rMapGenerator.MAP_TYPE.WALL)
            {
                //移動
                mapGenerator.UpdateTilie(eCurrentPos, rMapGenerator.MAP_TYPE.GROUND);
                transform.localPosition = mapGenerator.ScreenPos(eNextPos);
                eCurrentPos = eNextPos;
                mapGenerator.UpdateTilie(eCurrentPos, rMapGenerator.MAP_TYPE.ENEMY);
                //Debug.Log("敵が移動");
            }
        }
    }
    
    //プレイヤーを追いかける敵の動きで、プレイヤーが壁の向こうにいても追うようにする。↓資料
    //経路探索、ダイクストラ法、Aスター法、←優先、Navigation
}
