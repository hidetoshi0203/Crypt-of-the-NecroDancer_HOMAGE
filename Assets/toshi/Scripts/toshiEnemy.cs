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
      { 0, -1 },�@//TOP�̏ꍇ
      { 1, 0 },   //RIGHT�̏ꍇ
      { 0, 1 },   //DOWN�̏ꍇ
      { -1, 0 }   //LEFT�̏ꍇ
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
        //�m�[�c���󂯎��Atrue�̎��Ɉ�񂾂�������
        //�㉺�ړ��̓G��1��ځi��ɂP�}�X�j�A�Q��ځi���ɂP�}�X�j��int�^��count�ϐ��ŊǗ�����
        //�P��ڂ̍s�����ł���Ƃ���count���O�̎��A�Q��ڂ�count���P�̂Ƃ��ɂ���
        //�P��ڂ̍s�����I����count��1++�A�Q��ڂ��I����count������������
    }
}
