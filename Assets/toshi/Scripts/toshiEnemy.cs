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
    public Vector2Int eCurrentPos, eNextPos;
    MapGenerator mapGenerator;
    NotesManager notesManager = null;
    GameObject leftNotes;
    GameObject rightNotes;
    GameObject function;
    int count = 0;  

    void Start()
    {
        mapGenerator = transform.parent.GetComponent<MapGenerator>();
        notesManager = GetComponent<NotesManager>();
        direction = DIRECTION.DOWN;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(eNextPos);
        if (notesManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("NotesManager");
            notesManager = inst.GetComponent<NotesManager>();
        }
        if (notesManager != null && notesManager.CanInputKey())
        {
            if (notesManager.enemyCanMove)
            {
                if (count == 0)
                {
                    direction = DIRECTION.DOWN;
                    eMoveType();
                    notesManager.enemyCanMove = false;
                    count++;
                }
                if (count != 0)
                {
                    direction = DIRECTION.TOP;
                    //eMoveType();
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
            eNextPos = eCurrentPos + new Vector2Int(move[(int)direction, 0],
                move[(int)direction, 1]);

            if (mapGenerator.GetEnemyNextMapType(eNextPos) == MapGenerator.MAP_TYPE.WALL)
            {
                // �������Ȃ�
            }
            else if (mapGenerator.GetEnemyNextMapType(eNextPos) == MapGenerator.MAP_TYPE.PLAYER)
            {
                // �v���C���[�ɍU������
            }
            else if (mapGenerator.GetEnemyNextMapType(eNextPos) != MapGenerator.MAP_TYPE.WALL)
            {
                //�ړ�
                mapGenerator.UpdateTilie(eCurrentPos, MapGenerator.MAP_TYPE.GROUND);
                transform.localPosition = mapGenerator.ScreenPos(eNextPos);
                eCurrentPos = eNextPos;
                mapGenerator.UpdateTilie(eCurrentPos, MapGenerator.MAP_TYPE.ENEMY);
                Debug.Log("�G���ړ�");
            }
        }
    }
    
}
