using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    toshiEnemy toshiEnemy;
    public GameObject[] lifeArray = new GameObject[3];
    public int playerHP = 3;

    private void Start()
    {
        toshiEnemy = GetComponent<toshiEnemy>();
    }
    void Update()
    {
        if (toshiEnemy.isEnemyAttack)
        {
            lifeArray[playerHP - 1].SetActive(false);
            playerHP--;
        }
    }
}
