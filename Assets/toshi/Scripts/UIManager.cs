using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        SPotionBoxUI.SetActive(false);
        SPotionUI.SetActive(false);
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
            SPotionBoxUI.SetActive(true);
            SPotionUI.SetActive(true);
        }
        else if (ToshiPlayer.isPowerUpTimer == false)
        {
            SPotionBoxUI.SetActive(false);
            SPotionUI.SetActive(false);
        }
    }
}
