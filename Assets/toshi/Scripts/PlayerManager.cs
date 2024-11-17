using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    toshiEnemy ToshiEnemy = null;
    EnemyManager enemyManager = null;
    toshiPlayer ToshiPlayer = null;
    public Vector2Int attackedPlayerPos; // �G����U�����ꂽ�v���C���[�̍��W
    MapGenerator mapGenerator;
    void Start()
    {
        mapGenerator = transform.parent.GetComponent<MapGenerator>();
    }

    void Update()
    {
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
        if (ToshiPlayer == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Player1");
            ToshiPlayer = inst.GetComponent<toshiPlayer>();
        }

        if (ToshiEnemy.isEnemyAttack)
        {
            attackedPlayerPos = enemyManager.enemyNextPos; // �G��nextPos��������
            if (attackedPlayerPos == ToshiPlayer.playerCurrentPos) // �G����U�����ꂽ���W�ƃv���C���[�̍��W���ׂ�
            {
                Destroy(gameObject); // �v���C���[�̃I�u�W�F�N�g��Destroy����
                mapGenerator.UpdateTilie(ToshiPlayer.playerCurrentPos, MapGenerator.MAP_TYPE.GROUND); // MAP_TYPE�̍U�����ꂽPLAYER��GROUND�ɕς���
            }
        }
    }
}
