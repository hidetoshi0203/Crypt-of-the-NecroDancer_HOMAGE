using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuiEnemyManager : MonoBehaviour
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

    MapGenerator mapGenerator;
    NotesManager notesManager = null;
    RuiAttackedEnemy ruiAttackedEnemy;

    int count = 0;
    public bool isEnemyAttack;

    void Start()
    {
        mapGenerator = transform.parent.GetComponent<MapGenerator>();
        notesManager = GetComponent<NotesManager>();
        ruiAttackedEnemy = GetComponent<RuiAttackedEnemy>();
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
        //�m�[�c���󂯎��Atrue�̎��Ɉ�񂾂�������
        //�㉺�ړ��̓G��1��ځi��ɂP�}�X�j�A�Q��ځi���ɂP�}�X�j��int�^��count�ϐ��ŊǗ�����
        //�P��ڂ̍s�����ł���Ƃ���count���O�̎��A�Q��ڂ�count���P�̂Ƃ��ɂ���
        //�P��ڂ̍s�����I����count��1++�A�Q��ڂ��I����count������������
    }
    void eMoveType()
    {
        //���̏������ƈړ��ł��Ȃ�
        if (notesManager != null && notesManager.CanInputKey())
        {
            ruiAttackedEnemy.enemyNextPos = ruiAttackedEnemy.enemyCurrentPos + new Vector2Int(move[(int)direction, 0],
                move[(int)direction, 1]);

            if (mapGenerator.GetEnemyNextMapType(ruiAttackedEnemy.enemyNextPos) == MapGenerator.MAP_TYPE.WALL)
            {
                // �������Ȃ�
            }
            else if (mapGenerator.GetEnemyNextMapType(ruiAttackedEnemy.enemyNextPos) == MapGenerator.MAP_TYPE.PLAYER)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    isEnemyAttack = true; // EnemyManager.cs��true���󂯎��A�G��|���iMapGenarator.cs��MAP_TYPE��ENEMY����GROUND����������j
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    isEnemyAttack = true;
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    isEnemyAttack = true;
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    isEnemyAttack = true;
                }
            }
            else if (mapGenerator.GetEnemyNextMapType(ruiAttackedEnemy.enemyNextPos) != MapGenerator.MAP_TYPE.WALL)
            {
                //�ړ�
                //mapGenerator.UpdateTilie(ruiAttackedEnemy.enemyCurrentPos, MapGenerator.MAP_TYPE.GROUND);
                //transform.localPosition = mapGenerator.ScreenPos(ruiAttackedEnemy.enemyNextPos);
                //ruiAttackedEnemy.enemyCurrentPos = ruiAttackedEnemy.enemyNextPos;
                //mapGenerator.UpdateTilie(ruiAttackedEnemy.enemyCurrentPos, MapGenerator.MAP_TYPE.ENEMY);
            }
        }
    }

}
