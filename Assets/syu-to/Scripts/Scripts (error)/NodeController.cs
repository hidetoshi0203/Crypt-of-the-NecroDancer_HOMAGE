using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    //[SerializeField] private bool isUp = false;
    [SerializeField] private float endX = 0.5f;

    private Vector3 startPos;
    private Vector3 endPos;
    private float currentTime = 0f;

    private float tempo = 2.0f;

    private void Awake()
    {
        startPos = transform.position;
        endPos = transform.position;
        endPos.x = endX;
    }
    private void Update()
    {
        currentTime += Time.deltaTime;
        transform.position = Vector3.Lerp(startPos, endPos, currentTime / tempo);

        if(currentTime > tempo)
        {
            Destroy(this.gameObject);
        }
    }

}
