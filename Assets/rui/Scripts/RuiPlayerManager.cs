using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;

public class RuiPlayerManager : MonoBehaviour
{

    MapCreate mapCreate;
    Note noteScript;

    void Start()
    {
        noteScript = GetComponent<Note>();
    }

    void Update()
    {
        if (noteScript.isTouchingHeart)
        {
        }
    }
}
