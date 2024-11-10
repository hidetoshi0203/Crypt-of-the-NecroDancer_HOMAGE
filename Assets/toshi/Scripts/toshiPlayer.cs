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
    public Vector2Int playerCurrentPos, playerNextPos;
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
            if (notesManager.playerCanMove)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    direction = DIRECTION.TOP;
                    moveType();
                    notesManager.StopTouchSound();
                    notesManager.playerCanMove = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���

                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    direction = DIRECTION.RIGHT;
                    moveType();
                    notesManager.StopTouchSound();
                    notesManager.playerCanMove = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    direction = DIRECTION.DOWN;
                    moveType();
                    notesManager.StopTouchSound();
                    notesManager.playerCanMove = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    direction = DIRECTION.LEFT;
                    moveType();
                    notesManager.StopTouchSound();
                    notesManager.playerCanMove = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
                }
            }
        }
    }

        //�ړ��p�̊֐�
        void moveType()
    {
        if (notesManager != null && notesManager.CanInputKey())
        {
            playerNextPos = playerCurrentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);

            if (mapGenerator.GetPlayerNextMapType(playerNextPos) == MapGenerator.MAP_TYPE.WALL) // ���͐�(�v���C���[��nextPos)���ǂ������ꍇ
            {
                // �������Ȃ��i��X���̏�ŃW�����v����悤�ȃA�j���[�V����������j
            }
            else if (mapGenerator.GetPlayerNextMapType(playerNextPos) == MapGenerator.MAP_TYPE.ENEMY) // �G�������ꍇ
            {
                // �㉺���E�̓��͔�����Ƃ�bool��true�ɂ���
                if (Input.GetKeyDown(KeyCode.W))
                {
                    isAttack = true; // EnemyManager.cs��true���󂯎��A�G��|���iMapGenarator.cs��MAP_TYPE��ENEMY����GROUND����������j
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
            else if (mapGenerator.GetPlayerNextMapType(playerNextPos) != MapGenerator.MAP_TYPE.WALL) // �ǈȊO�������ꍇ
            {
                // �ړ�����
                mapGenerator.UpdateTilie(playerCurrentPos, MapGenerator.MAP_TYPE.GROUND); // �����̍��W��MAP_TYPE��GROUND�ɂ���
                transform.localPosition = mapGenerator.ScreenPos(playerNextPos);          // �ړ�
                playerCurrentPos = playerNextPos;
                mapGenerator.UpdateTilie(playerCurrentPos, MapGenerator.MAP_TYPE.PLAYER); // �����̍��W��MAP_TYPE��PLAYER�ɂ���
            }
        }
    }
}
