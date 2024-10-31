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
    public Vector2Int currentPos, nextPos;
    MapGenerator mapGenerator;
    void Start()
    {
        mapGenerator = transform.parent.GetComponent<MapGenerator>();

        direction = DIRECTION.DOWN;
    }

    // Update is called once per frame
    void Update()
    {
        //ノーツを受け取り、trueの時に一回だけ動かす
        //上下移動の敵は1回目（上に１マス）、２回目（下に１マス）をint型のcount変数で管理する
        //１回目の行動ができるときはcountが０の時、２回目はcountが１のときにする
        //１回目の行動が終わるとcountに1++、２回目が終わるとcountを初期化する
    }
}
