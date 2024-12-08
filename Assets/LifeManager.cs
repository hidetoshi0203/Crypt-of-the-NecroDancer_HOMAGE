using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    toshiEnemy toshiEnemy = null;
    public GameObject[] lifeArray = new GameObject[3];
    public int playerHP = 3;

    void Update()
    {
        if (toshiEnemy != null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Enemy");
            toshiEnemy = inst.GetComponent<toshiEnemy>();
        }
        if (toshiEnemy.isEnemyAttack)
        {
            lifeArray[playerHP - 1].SetActive(false);
            playerHP--;
        }
    }
}
