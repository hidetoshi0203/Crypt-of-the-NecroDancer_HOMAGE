using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject SPotionBoxObj;
    [SerializeField] private GameObject SPotionObj;

    toshiPlayer ToshiPlayer = null;
    CheckAliveScripts checkAliveScripts;
    [SerializeField] private GameObject checkAliveObjs;

    void Start()
    {
        checkAliveObjs = GameObject.Find("CheckAliveObjects");
        checkAliveScripts = checkAliveObjs.GetComponent<CheckAliveScripts>();

        SPotionBoxObj = GameObject.Find("SPotionBoxUI");
        SPotionBoxObj = GameObject.Find("SPotionUI");
        SPotionBoxObj.GetComponent<Image>().enabled = false;
        SPotionObj.GetComponent<Image>().enabled = false;
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
            SPotionBoxObj.GetComponent<Image>().enabled = true;
            SPotionObj.GetComponent<Image>().enabled = true;
        }
        else if (ToshiPlayer.isPowerUpTimer == false)
        {
            SPotionBoxObj.GetComponent<Image>().enabled = false;
            SPotionObj.GetComponent<Image>().enabled = false;
            SPotionObj.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }
}
