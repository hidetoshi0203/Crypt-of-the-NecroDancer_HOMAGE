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
    //�ǋL
    int[,] move = {
      { 0, -1 },�@//TOP�̏ꍇ
      { 1, 0 },   //RIGHT�̏ꍇ
      { 0, 1 },   //DOWN�̏ꍇ
      { -1, 0 }   //LEFT�̏ꍇ
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
    //�ǋL�@nextPos���`
    



    //�C���@���͎���_move�֐����ĂԂ悤�ɂ���B
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
            //�G�Ɍ������Ĉړ��L�[���͂��ꂽ��ړ��͂����A�U�������鏈��������

        }
    }

    //�ǋL�@�ړ��p�̊֐�
    void _move()
    {
        nextPos = currentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);

        //�C���@MAP_TYPE��WALL�ȊO�Ȃ�ړ��̏���������
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
