using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;

public class RuiPlayerManager : MonoBehaviour
{
    ObjectMove objectMove;
    GameObject function;
    NotesManager notesManager = null;

    //bool playerCanMove = false;//�ꎞ�I�̃R�����g�����Ƃ���

    void Start()
    {
        objectMove = GetComponent<ObjectMove>();
        function = GameObject.Find("Function");
        //        notesController = FindObjectOfType<NotesController>();
    }

    void Update()
    {
        if (notesManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("NotesManager");
            notesManager = inst.GetComponent<NotesManager>();
        }
        if (notesManager != null && notesManager.CanInputKey())
        {
            notesManager.playerCanMove = true;

            //Debug.Log("aa");
            if (Input.GetKeyDown(KeyCode.W) && notesManager.playerCanMove) // W�L�[����������ړ�����
            {
                objectMove.direction = ObjectMove.DIRECTION.TOP;
                objectMove.MoveMent();

                notesManager.StopTouchSound();
                notesManager.PlaySpaceSound(); //�X�y�[�X�L�[���������Ƃ��̉���炷
                notesManager.playerCanMove = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
            }

            if (Input.GetKeyDown(KeyCode.D) && notesManager.playerCanMove) // D�L�[����������ړ�����
            {
                objectMove.direction = ObjectMove.DIRECTION.RIGHT;
                objectMove.MoveMent();

                notesManager.StopTouchSound();
                notesManager.PlaySpaceSound(); //�X�y�[�X�L�[���������Ƃ��̉���炷
                notesManager.playerCanMove = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
            }

            if (Input.GetKeyDown(KeyCode.S) && notesManager.playerCanMove) // S�L�[���������牺�Ɉړ�����
            {
                objectMove.direction = ObjectMove.DIRECTION.DOWN;
                objectMove.MoveMent();

                notesManager.StopTouchSound();
                notesManager.PlaySpaceSound(); //�X�y�[�X�L�[���������Ƃ��̉���炷
                notesManager.playerCanMove = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
            }

            if (Input.GetKeyDown(KeyCode.A) && notesManager.playerCanMove)
            {
                objectMove.direction = ObjectMove.DIRECTION.LEFT;
                objectMove.MoveMent();

                notesManager.StopTouchSound();
                notesManager.PlaySpaceSound(); //�X�y�[�X�L�[���������Ƃ��̉���炷
                notesManager.playerCanMove = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
            }
        }
    }
}
