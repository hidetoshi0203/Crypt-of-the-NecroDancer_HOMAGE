using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;
public class toshiPlayer : MonoBehaviour
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
    public bool isAttack;

    MapGenerator mapGenerator;
    NotesManager notesManager = null;
    GameObject leftNotes;
    GameObject rightNotes;
    GameObject function;

    private void Start()
    {
        mapGenerator = transform.parent.GetComponent<MapGenerator>();
        notesManager = GetComponent<NotesManager>();
        leftNotes = GameObject.Find("notesManager.leftNoteObject");
        rightNotes = GameObject.Find("notesManager.rightNoteObject");
        function = GameObject.Find("Function");
        direction = DIRECTION.DOWN;
    }
    



    //�@���͎���_move�֐����ĂԂ悤�ɂ���B
    private void Update()
    {
        if (notesManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("NotesManager");
            notesManager = inst.GetComponent<NotesManager>();
        }
        if (notesManager != null && notesManager.CanInputKey())
        {
            if (notesManager.canMove)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    direction = DIRECTION.TOP;
                    moveType();
                    notesManager.StopTouchSound();
                    notesManager.PlaySpaceSound(); //�X�y�[�X�L�[���������Ƃ��̉���炷
                    notesManager.canMove = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���

                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    direction = DIRECTION.RIGHT;
                    moveType();
                    notesManager.StopTouchSound();
                    notesManager.PlaySpaceSound(); //�X�y�[�X�L�[���������Ƃ��̉���炷
                    notesManager.canMove = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    direction = DIRECTION.DOWN;
                    moveType();
                    notesManager.StopTouchSound();
                    notesManager.PlaySpaceSound(); //�X�y�[�X�L�[���������Ƃ��̉���炷
                    notesManager.canMove = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    direction = DIRECTION.LEFT;
                    moveType();
                    notesManager.StopTouchSound();
                    notesManager.PlaySpaceSound(); //�X�y�[�X�L�[���������Ƃ��̉���炷
                    notesManager.canMove = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
                }
            }
        }
    }

        //�ړ��p�̊֐�
        void moveType()
    {
        if (notesManager != null && notesManager.CanInputKey())
        {
            nextPos = currentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);

            if (mapGenerator.GetNextMapType(nextPos) == MapGenerator.MAP_TYPE.WALL)
            {
                // �������Ȃ�

            }
            else if (mapGenerator.GetNextMapType(nextPos) == MapGenerator.MAP_TYPE.ENEMY)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    isAttack = true;
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    isAttack = true;
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    isAttack = true;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    isAttack = true;
                }
            }
            else if (mapGenerator.GetNextMapType(nextPos) != MapGenerator.MAP_TYPE.WALL)
            {
                // �ړ�
                mapGenerator.UpdateTilie(currentPos, MapGenerator.MAP_TYPE.GROUND);
                transform.localPosition = mapGenerator.ScreenPos(nextPos);
                currentPos = nextPos;
                mapGenerator.UpdateTilie(currentPos, MapGenerator.MAP_TYPE.PLAYER);
                Debug.Log("�ړ�");
            }
        }
    }
}
