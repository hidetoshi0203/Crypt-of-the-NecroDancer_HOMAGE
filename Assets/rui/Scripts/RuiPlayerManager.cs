using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;

public class RuiPlayerManager : MonoBehaviour
{
    float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, 1, 0);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, -1, 0);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
        }
    }
}
