using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject SPotionBoxUI;
    [SerializeField] private GameObject SPotionUI;

    RuitoshiPlayer ruiToshiPlayer = null;

    void Start()
    {

        SPotionBoxUI.SetActive(false);
        SPotionUI.SetActive(false);
    }

    void Update()
    {
        if (ruiToshiPlayer == null)
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
