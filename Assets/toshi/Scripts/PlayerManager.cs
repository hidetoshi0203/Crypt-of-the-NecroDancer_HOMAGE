using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    LifeManager lifeManager = null;
    toshiEnemy toshiEnemy = null;  
    EnemyManager enemyManager = null;
    toshiPlayer toshiPlayer = null;

    public GameObject[] lifeArray = new GameObject[3];
    int playerHP = 3;

    public Vector2Int attackedPlayerPos; // �G����U�����ꂽ�v���C���[�̍��W
    
    void Update()
    {
        if (lifeManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("GameManager");
            lifeManager = inst.GetComponent<LifeManager>();
        }
        if (toshiPlayer == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Player");
            toshiPlayer = inst.GetComponent<toshiPlayer>();
        }
        if (toshiEnemy == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Enemy");
            toshiEnemy = inst.GetComponent<toshiEnemy>();
        }
        if (enemyManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Enemy");
            enemyManager = inst.GetComponent<EnemyManager>();
        }
        if (toshiEnemy.isEnemyAttack)
        {
            Debug.Log("�G����̍U��");
           
            attackedPlayerPos = enemyManager.enemyNextPos; // �G��nextPos��������
            if (attackedPlayerPos == toshiPlayer.playerCurrentPos) // �G����U�����ꂽ���W�ƃv���C���[�̍��W���ׂ�
            {
                lifeArray[playerHP - 1].SetActive(false);
                playerHP--;
                if (playerHP == 0)
                {
                    Destroy(gameObject); // �v���C���[�̃I�u�W�F�N�g��Destroy����
                }
            }
        }
    }
}
