using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;

public class RuiPlayerManager : MonoBehaviour
{
    ObjectMove objectMove;
    GameObject function;
    Function functionScript;

    void Start()
    {
        objectMove = GetComponent<ObjectMove>();
        function = GameObject.Find("Function");
        functionScript = FindObjectOfType<Function>();
    }

    void Update()
    {
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
