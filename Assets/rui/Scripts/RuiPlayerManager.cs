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

    bool canPlaySpaceSound = false;

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
            canPlaySpaceSound = true;

            //Debug.Log("aa");
            if (Input.GetKeyDown(KeyCode.W) && canPlaySpaceSound)
            {
                objectMove.direction = ObjectMove.DIRECTION.TOP;
                objectMove.MoveMent();

                notesManager.StopTouchSound();
                notesManager.PlaySpaceSound(); //�X�y�[�X�L�[���������Ƃ��̉���炷
                canPlaySpaceSound = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
            }

            if (Input.GetKeyDown(KeyCode.D) && canPlaySpaceSound)
            {
                objectMove.direction = ObjectMove.DIRECTION.RIGHT;
                objectMove.MoveMent();

                notesManager.StopTouchSound();
                notesManager.PlaySpaceSound(); //�X�y�[�X�L�[���������Ƃ��̉���炷
                canPlaySpaceSound = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
            }

            if (Input.GetKeyDown(KeyCode.S) && canPlaySpaceSound)
            {
                objectMove.direction = ObjectMove.DIRECTION.DOWN;
                objectMove.MoveMent();

                notesManager.StopTouchSound();
                notesManager.PlaySpaceSound(); //�X�y�[�X�L�[���������Ƃ��̉���炷
                canPlaySpaceSound = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
            }

            if (Input.GetKeyDown(KeyCode.A) && canPlaySpaceSound)
            {
                objectMove.direction = ObjectMove.DIRECTION.LEFT;
                objectMove.MoveMent();

                notesManager.StopTouchSound();
                notesManager.PlaySpaceSound(); //�X�y�[�X�L�[���������Ƃ��̉���炷
                canPlaySpaceSound = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
            }
        }
    }
}
