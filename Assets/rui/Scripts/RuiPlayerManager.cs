using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;

public class RuiPlayerManager : MonoBehaviour
{
    ObjectMove objectMove;
    Note noteScript;

    void Start()
    {
        objectMove = GetComponent<ObjectMove>();
        noteScript = GetComponent<Note>();
    }

    void Update()
    {
        //if (noteScript.isTouchingHeart)
        //{
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
        //}
    }
}
