using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toshiPlayer : MonoBehaviour
{
    public enum DIRECTION
    {
        TOP,
        RIGHT,
        DOWN,
        LEFT
    }
    //追記
    int[,] move = {
      { 0, -1 },　//TOPの場合
      { 1, 0 },   //RIGHTの場合
      { 0, 1 },   //DOWNの場合
      { -1, 0 }   //LEFTの場合
    };

    public DIRECTION direction;
    public Vector2Int currentPos, nextPos;

    public bool isEnemy; 

    MapGenerator mapGenerator;

    private void Start()
    {
        mapGenerator = transform.parent.GetComponent<MapGenerator>();

        direction = DIRECTION.DOWN;
    }
    //追記　nextPosを定義
    



    //修正　入力時に_move関数を呼ぶようにする。
    private void Update()
    {
        if (!isEnemy)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                direction = DIRECTION.TOP;
                _move();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                direction = DIRECTION.RIGHT;
                _move();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                direction = DIRECTION.DOWN;
                _move();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                direction = DIRECTION.LEFT;
                _move();
            }
        }
        if (isEnemy)
        {
            //敵に向かって移動キー入力されたら移動はせず、攻撃をする処理を書く

        }
    }

    //追記　移動用の関数
    void _move()
    {
        nextPos = currentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);

        //修正　MAP_TYPEがWALL以外なら移動の処理をする
        if (mapGenerator.GetNextMapType(nextPos) != MapGenerator.MAP_TYPE.WALL)
        {
            transform.localPosition = mapGenerator.ScreenPos(nextPos);
            currentPos = nextPos;
        }
    }
    void _attack()
    {
        if (mapGenerator.GetNextMapType(nextPos) != MapGenerator.MAP_TYPE.ENEMY)
        {
            isEnemy = true;
            currentPos = nextPos;
        } 
    }
}
