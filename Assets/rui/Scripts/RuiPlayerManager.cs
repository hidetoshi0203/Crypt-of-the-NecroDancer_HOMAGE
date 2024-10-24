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
                notesManager.PlaySpaceSound(); //スペースキーを押したときの音を鳴らす
                canPlaySpaceSound = false; //フラグをオフにして音を鳴らせないようにする
            }

            if (Input.GetKeyDown(KeyCode.D) && canPlaySpaceSound)
            {
                objectMove.direction = ObjectMove.DIRECTION.RIGHT;
                objectMove.MoveMent();

                notesManager.StopTouchSound();
                notesManager.PlaySpaceSound(); //スペースキーを押したときの音を鳴らす
                canPlaySpaceSound = false; //フラグをオフにして音を鳴らせないようにする
            }

            if (Input.GetKeyDown(KeyCode.S) && canPlaySpaceSound)
            {
                objectMove.direction = ObjectMove.DIRECTION.DOWN;
                objectMove.MoveMent();

                notesManager.StopTouchSound();
                notesManager.PlaySpaceSound(); //スペースキーを押したときの音を鳴らす
                canPlaySpaceSound = false; //フラグをオフにして音を鳴らせないようにする
            }

            if (Input.GetKeyDown(KeyCode.A) && canPlaySpaceSound)
            {
                objectMove.direction = ObjectMove.DIRECTION.LEFT;
                objectMove.MoveMent();

                notesManager.StopTouchSound();
                notesManager.PlaySpaceSound(); //スペースキーを押したときの音を鳴らす
                canPlaySpaceSound = false; //フラグをオフにして音を鳴らせないようにする
            }
        }
    }
}
