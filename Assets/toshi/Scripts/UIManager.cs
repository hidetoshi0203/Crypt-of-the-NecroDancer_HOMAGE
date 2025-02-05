using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject SPotionBoxUI;
    [SerializeField] private GameObject SPotionUI;

    toshiPlayer ToshiPlayer = null;
    CheckAliveScripts checkAliveScripts;
    [SerializeField] private GameObject checkAliveObjs;

    void Start()
    {
        checkAliveObjs = GameObject.Find("CheckAliveObjects");
        checkAliveScripts = checkAliveObjs.GetComponent<CheckAliveScripts>();

        SPotionBoxUI.GetComponent<Image>().enabled = false;
        SPotionUI.GetComponent<Image>().enabled = false;
    }

    void Update()
    {
        if (ToshiPlayer == null && checkAliveScripts.isAliveToshiPlayerScr)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Player");
            ToshiPlayer = inst.GetComponent<toshiPlayer>();
        }

        if (ToshiPlayer.isPowerUpTimer)
        {
            SPotionBoxUI.GetComponent<Image>().enabled = true;
            SPotionUI.GetComponent<Image>().enabled = true;
        }
        else if (ToshiPlayer.isPowerUpTimer == false)
        {
            SPotionBoxUI.GetComponent<Image>().enabled = false;
            SPotionUI.GetComponent<Image>().enabled = false;
        }
    }
}
