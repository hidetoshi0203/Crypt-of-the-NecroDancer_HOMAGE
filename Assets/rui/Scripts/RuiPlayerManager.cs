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
            //Debug.Log("aa");
            if (Input.GetKeyDown(KeyCode.W))
            {
                objectMove.direction = ObjectMove.DIRECTION.TOP;
                objectMove.MoveMent();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                objectMove.direction = ObjectMove.DIRECTION.RIGHT;
                objectMove.MoveMent();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                objectMove.direction = ObjectMove.DIRECTION.DOWN;
                objectMove.MoveMent();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                objectMove.direction = ObjectMove.DIRECTION.LEFT;
                objectMove.MoveMent();
            }
        }
    }
}
