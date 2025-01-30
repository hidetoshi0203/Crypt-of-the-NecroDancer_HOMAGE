using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Heartresize : MonoBehaviour
{
    [SerializeField] GameObject defaultHeart;
    [SerializeField] Vector3 reSizeHeart;
    [SerializeField] Vector3 maxsizeHeart;

    [SerializeField] float Interval = 1f;
    private float CurrentTime = 0f;

    bool ReSize = true;

    private void Start()
    {
        reSizeHeart = defaultHeart.transform.localScale;
        ReSize = true;
    }

    private void Update()
    {
        CurrentTime += Time.deltaTime;

        if (ReSize && (CurrentTime > Interval))
        {
            ReSizeHeart();
            Debug.Log("a");
            CurrentTime = 0f;
            ReSize = false;
        }
        
        if (!ReSize && (CurrentTime > Interval))
        {
            DefaultSizeHeart();
            Debug.Log("b");
            CurrentTime = 0f;
            ReSize = true;
        }

    }
    
    private void ReSizeHeart()
    {
        defaultHeart.transform.localScale = maxsizeHeart;
    }
    
    private void DefaultSizeHeart()
    {
        defaultHeart.transform.localScale = reSizeHeart;
    }
}

