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
      { 0, -1 },�@//TOP�̏ꍇ
      { 0, 1 },   //DOWN�̏ꍇ
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

            if (mapGenerator.GetEnemyNextMapType(eNextPos) == rMapGenerator.MAP_TYPE.WALL)
            {
                // �������Ȃ�
            }
            else if (mapGenerator.GetEnemyNextMapType(eNextPos) == rMapGenerator.MAP_TYPE.PLAYER)
            {
                // �v���C���[�ɍU������
            }
            else if (mapGenerator.GetEnemyNextMapType(eNextPos) != rMapGenerator.MAP_TYPE.WALL)
            {
                //�ړ�
                mapGenerator.UpdateTilie(eCurrentPos, rMapGenerator.MAP_TYPE.GROUND);
                transform.localPosition = mapGenerator.ScreenPos(eNextPos);
                eCurrentPos = eNextPos;
                mapGenerator.UpdateTilie(eCurrentPos, rMapGenerator.MAP_TYPE.ENEMY);
                //Debug.Log("�G���ړ�");
            }
        }
    }
    
    //�v���C���[��ǂ�������G�̓����ŁA�v���C���[���ǂ̌������ɂ��Ă��ǂ��悤�ɂ���B������
    //�o�H�T���A�_�C�N�X�g���@�AA�X�^�[�@�A���D��ANavigation
}
