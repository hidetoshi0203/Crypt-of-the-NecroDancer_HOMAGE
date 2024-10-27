using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toshiPlayer : MonoBehaviour
{
    EnemyJudgement enemyJudgementW = null;
    EnemyJudgement enemyJudgementA = null;
    EnemyJudgement enemyJudgementS = null;
    EnemyJudgement enemyJudgementD = null;

    public bool isAttack;
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

    private void Start()
    {
        mapGenerator = transform.parent.GetComponent<MapGenerator>();

        direction = DIRECTION.DOWN;

    }
    



    //　入力時に_move関数を呼ぶようにする。
    private void Update()
    {
        if (enemyJudgementW == null)
        {
            GameObject instW = GameObject.FindGameObjectWithTag("WTag");
            enemyJudgementW = instW.GetComponent<EnemyJudgement>();
        }

        if (enemyJudgementA == null)
        {
            GameObject instA = GameObject.FindGameObjectWithTag("ATag");
            enemyJudgementA = instA.GetComponent<EnemyJudgement>();
        }

        if (enemyJudgementS == null)
        {
            GameObject instS = GameObject.FindGameObjectWithTag("STag");
            enemyJudgementS = instS.GetComponent<EnemyJudgement>();
        }

        if (enemyJudgementD == null)
        {
            GameObject instD = GameObject.FindGameObjectWithTag("DTag");
            enemyJudgementD = instD.GetComponent<EnemyJudgement>();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = DIRECTION.TOP;
            _moveType();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            direction = DIRECTION.RIGHT;
            _moveType();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            direction = DIRECTION.DOWN;
            _moveType();
            Debug.Log("aaa");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = DIRECTION.LEFT;
            _moveType();
        }
    }

    //移動用の関数
    void _moveType()
    {
        nextPos = currentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);

        if (mapGenerator.GetNextMapType(nextPos) == MapGenerator.MAP_TYPE.WALL)
        {
            // 何もしない
            
        }
        else if(mapGenerator.GetNextMapType(nextPos) == MapGenerator.MAP_TYPE.ENEMY)
        {
            // 攻撃
            //if (enemyJudgementW.isEnemyJudge)
            {
                if (Input.GetKeyDown(KeyCode.W)) 
                {
                    isAttack = true;
                }
            }
            //if (enemyJudgementA.isEnemyJudge)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    isAttack = true;
                }
            }
            //if (enemyJudgementS.isEnemyJudge)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    isAttack = true;
                }
            }
            //if (enemyJudgementD.isEnemyJudge)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    isAttack = true;
                }
            }
        }
        else if(mapGenerator.GetNextMapType(nextPos) == MapGenerator.MAP_TYPE.GROUND || mapGenerator.GetNextMapType(nextPos) == MapGenerator.MAP_TYPE.PLAYER)
        {
            // 移動
            transform.localPosition = mapGenerator.ScreenPos(nextPos);
            currentPos = nextPos;
            Debug.Log("移動");
        }
    }
}
