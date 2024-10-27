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
      { 0, -1 },�@//TOP�̏ꍇ
      { 1, 0 },   //RIGHT�̏ꍇ
      { 0, 1 },   //DOWN�̏ꍇ
      { -1, 0 }   //LEFT�̏ꍇ
    };

    public DIRECTION direction;
    public Vector2Int currentPos, nextPos;

    MapGenerator mapGenerator;

    private void Start()
    {
        mapGenerator = transform.parent.GetComponent<MapGenerator>();

        direction = DIRECTION.DOWN;

    }
    



    //�@���͎���_move�֐����ĂԂ悤�ɂ���B
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

    //�ړ��p�̊֐�
    void _moveType()
    {
        nextPos = currentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);

        if (mapGenerator.GetNextMapType(nextPos) == MapGenerator.MAP_TYPE.WALL)
        {
            // �������Ȃ�
            
        }
        else if(mapGenerator.GetNextMapType(nextPos) == MapGenerator.MAP_TYPE.ENEMY)
        {
            // �U��
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
            // �ړ�
            transform.localPosition = mapGenerator.ScreenPos(nextPos);
            currentPos = nextPos;
            Debug.Log("�ړ�");
        }
    }
}
