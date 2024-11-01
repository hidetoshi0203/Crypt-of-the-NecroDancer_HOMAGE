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

    //bool canMove = false;//�ꎞ�I�̃R�����g�����Ƃ���

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
            notesManager.canMove = true;

            //Debug.Log("aa");
            if (Input.GetKeyDown(KeyCode.W) && notesManager.canMove) // W�L�[����������ړ�����
            {
                objectMove.direction = ObjectMove.DIRECTION.TOP;
                objectMove.MoveMent();

                notesManager.StopTouchSound();
                notesManager.PlaySpaceSound(); //�X�y�[�X�L�[���������Ƃ��̉���炷
                notesManager.canMove = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
            }

            if (Input.GetKeyDown(KeyCode.D) && notesManager.canMove) // D�L�[����������ړ�����
            {
                objectMove.direction = ObjectMove.DIRECTION.RIGHT;
                objectMove.MoveMent();

                notesManager.StopTouchSound();
                notesManager.PlaySpaceSound(); //�X�y�[�X�L�[���������Ƃ��̉���炷
                notesManager.canMove = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
            }

            if (Input.GetKeyDown(KeyCode.S) && notesManager.canMove) // S�L�[���������牺�Ɉړ�����
            {
                objectMove.direction = ObjectMove.DIRECTION.DOWN;
                objectMove.MoveMent();

                notesManager.StopTouchSound();
                notesManager.PlaySpaceSound(); //�X�y�[�X�L�[���������Ƃ��̉���炷
                notesManager.canMove = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
            }

            if (Input.GetKeyDown(KeyCode.A) && notesManager.canMove)
            {
                objectMove.direction = ObjectMove.DIRECTION.LEFT;
                objectMove.MoveMent();

                notesManager.StopTouchSound();
                notesManager.PlaySpaceSound(); //�X�y�[�X�L�[���������Ƃ��̉���炷
                notesManager.canMove = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
            }
        }
    }
}
