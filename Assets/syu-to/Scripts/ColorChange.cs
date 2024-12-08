using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    [SerializeField] Sprite[] mat = new Sprite[2];
    private int count = 0;


    public void Changecolor()
    {
        this.GetComponent<SpriteRenderer>().sprite = mat[count];

        count++;
        if (count == mat.Length)
        {
            count = 0;
        }

        Debug.Log("Change");
    }
}
