using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ruiCameraController : MonoBehaviour
{
    private GameObject instPlayer;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - instPlayer.transform.position;
    }

    void Update()
    {
        if (instPlayer != null)
        {
            instPlayer = GameObject.FindGameObjectWithTag("Player");
        }

        transform.position = instPlayer.transform.position + offset;
    }
}
