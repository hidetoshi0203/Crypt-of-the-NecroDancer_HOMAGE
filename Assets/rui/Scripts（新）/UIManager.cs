using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject SPotionBoxUI;
    [SerializeField] private GameObject SPotionUI;

    RuitoshiPlayer ruiToshiPlayer = null;
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
        if (ruiToshiPlayer == null && checkAliveScripts.isAliveToshiPlayerScr)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Player");
            ruiToshiPlayer = inst.GetComponent<RuitoshiPlayer>();
        }

        if (ruiToshiPlayer.isPowerUpTimer)
        {
            SPotionBoxUI.SetActive(true);
            SPotionUI.SetActive(true);
        }
        else if (ruiToshiPlayer.isPowerUpTimer == false)
        {
            SPotionBoxUI.SetActive(false);
            SPotionUI.SetActive(false);
        }
    }
}
