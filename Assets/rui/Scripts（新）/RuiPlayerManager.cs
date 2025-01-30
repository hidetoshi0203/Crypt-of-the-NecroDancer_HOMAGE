using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuiPlayerManager : MonoBehaviour
{
    RuiEnemyManager enemyManager = null;
    RuitoshiPlayer toshiPlayer = null;
    RuiMapGenerator mapGenerator = null;
    CheckAliveScripts checkAliveScripts;
    [SerializeField] private GameObject checkAliveObjs;

    [SerializeField] GameObject playerObj;
    public GameObject[] lifeArray = new GameObject[3];
    public int playerHP = 3;

    public Vector2Int attackedPlayerPos; // �G����U�����ꂽ�v���C���[�̍��W

    void Start()
    {
        checkAliveObjs = GameObject.Find("CheckAliveObjects");
        checkAliveScripts = checkAliveObjs.GetComponent<CheckAliveScripts>();
    }
    void Update()
    {
        if (mapGenerator == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("MapChip");
            mapGenerator = inst.GetComponent<RuiMapGenerator>();
        }
        if (toshiPlayer == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Player");
            toshiPlayer = inst.GetComponent<RuitoshiPlayer>();
        }
        if (enemyManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Enemy");
            enemyManager = inst.GetComponent<RuiEnemyManager>();
        }

        //if (toshiEnemy.isEnemyAttack)
        //{
        //    Debug.Log("�G����̍U��");
           
        //    attackedPlayerPos = enemyManager.enemyNextPos; // �G��nextPos��������
        //    if (attackedPlayerPos == toshiPlayer.playerCurrentPos) // �G����U�����ꂽ���W�ƃv���C���[�̍��W���ׂ�
        //    {
        //        Debug.Log(attackedPlayerPos);
                
        //        lifeArray[playerHP - 1].SetActive(false);
        //        playerHP--;
        //        if (playerHP == 0)
        //        {
        //            Destroy(gameObject); // �v���C���[�̃I�u�W�F�N�g��Destroy����
        //            mapGenerator.UpdateTile(toshiPlayer.playerCurrentPos, MapGenerator.MAP_TYPE.GROUND); // MAP_TYAPE�̍U�����ꂽPLAYER��GROUND�ɂ�����
        //        }
        //        toshiEnemy.isEnemyAttack = false;
        //    }
        //}
    }

    public void Hit()
    {
        lifeArray[playerHP - 1].SetActive(false);
        playerHP--;
        if (playerHP == 0)
        {
            Destroy(playerObj); // �v���C���[�̃I�u�W�F�N�g��Destroy����
            mapGenerator.UpdateTile(toshiPlayer.playerCurrentPos, RuiMapGenerator.MAP_TYPE.GROUND); // MAP_TYAPE�̍U�����ꂽPLAYER��GROUND�ɂ�����
            checkAliveScripts.isAliveToshiPlayerScr = false;
        }
    }
}
