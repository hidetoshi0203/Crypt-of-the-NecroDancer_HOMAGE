using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MapCreate : MonoBehaviour
{
    [SerializeField] TextAsset mapText;
    [SerializeField] GameObject[] objPrefabs;
    private string mapLines;

    private void Start()
    {
        loadMapData();
    }

    void loadMapData()
    {
        //mapLines = mapText.text.Split    
    }
}
