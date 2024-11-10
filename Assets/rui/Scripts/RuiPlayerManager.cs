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

    //bool playerCanMove = false;//一時的のコメントかしといた

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
            if (Input.GetKeyDown(KeyCode.W) && notesManager.playerCanMove) // Wキーを押したら移動する
            {
                objectMove.direction = ObjectMove.DIRECTION.TOP;
                objectMove.MoveMent();

                notesManager.StopTouchSound();
                notesManager.PlaySpaceSound(); //スペースキーを押したときの音を鳴らす
                notesManager.playerCanMove = false; //フラグをオフにして音を鳴らせないようにする
            }

            if (Input.GetKeyDown(KeyCode.D) && notesManager.playerCanMove) // Dキーを押したら移動する
            {
                objectMove.direction = ObjectMove.DIRECTION.RIGHT;
                objectMove.MoveMent();

                notesManager.StopTouchSound();
                notesManager.PlaySpaceSound(); //スペースキーを押したときの音を鳴らす
                notesManager.playerCanMove = false; //フラグをオフにして音を鳴らせないようにする
            }

            if (Input.GetKeyDown(KeyCode.S) && notesManager.playerCanMove) // Sキーを押したら下に移動する
            {
                objectMove.direction = ObjectMove.DIRECTION.DOWN;
                objectMove.MoveMent();

                notesManager.StopTouchSound();
                notesManager.PlaySpaceSound(); //スペースキーを押したときの音を鳴らす
                notesManager.playerCanMove = false; //フラグをオフにして音を鳴らせないようにする
            }

            if (Input.GetKeyDown(KeyCode.A) && notesManager.playerCanMove)
            {
                objectMove.direction = ObjectMove.DIRECTION.LEFT;
                objectMove.MoveMent();

                notesManager.StopTouchSound();
                notesManager.PlaySpaceSound(); //スペースキーを押したときの音を鳴らす
                notesManager.playerCanMove = false; //フラグをオフにして音を鳴らせないようにする
            }
        }
    }
}
