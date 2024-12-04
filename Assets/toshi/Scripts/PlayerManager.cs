using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    toshiEnemy ToshiEnemy = null;  
    EnemyManager enemyManager = null;
    toshiPlayer ToshiPlayer = null;
    MapGenerator mapGenerator;

    [SerializeField] int playerHP; // �v���C���[�̗̑�
    int playerMaxHP = 5;           // �v���C���[�̍ő�̗�
    public Vector2Int attackedPlayerPos; // �G����U�����ꂽ�v���C���[�̍��W
    
    void Start()
    {
        mapGenerator = transform.parent.GetComponent<MapGenerator>();
    }

    void Update()
    {
        if (ToshiPlayer == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Player");
            ToshiPlayer = inst.GetComponent<toshiPlayer>();
        }
        if (ToshiEnemy == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Enemy");
            ToshiEnemy = inst.GetComponent<toshiEnemy>();
        }
        if (enemyManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Enemy");
            enemyManager = inst.GetComponent<EnemyManager>();
        }
        if (ToshiEnemy.isEnemyAttack)
        {
            Debug.Log("�G�̍U��");
            playerHP -= 1;
            if (playerHP == 0)
            {
                attackedPlayerPos = enemyManager.enemyNextPos; // �G��nextPos��������
                if (attackedPlayerPos == ToshiPlayer.playerCurrentPos) // �G����U�����ꂽ���W�ƃv���C���[�̍��W���ׂ�
                {
                    Destroy(gameObject); // �v���C���[�̃I�u�W�F�N�g��Destroy����
                }
            }
        }
    }
}
