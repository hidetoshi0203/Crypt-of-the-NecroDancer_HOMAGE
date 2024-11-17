using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    private GameObject player;
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        
    }
    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, 6.0f * Time.deltaTime);
    }
}
