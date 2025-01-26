using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    toshiEnemy toshiEnemy = null;  
    EnemyManager enemyManager = null;
    toshiPlayer toshiPlayer = null;
    MapGenerator mapGenerator = null;

    EnemySlimeMoveComponent enemyTopDown = null;
    EnemyRightLeft enemyRightLeft = null;
    Enemy_Zombie_RightLeft enemy_Zombie_RightLeft = null;
    Enemy_Zombie_TopDown enemy_Zombie_TopDown = null;

    [SerializeField] GameObject playerObj;
    public GameObject[] lifeArray = new GameObject[3];
    int playerHP = 3;
    [SerializeField] float cycle;
    bool isBlinking = false;
    double time;

    public Vector2Int attackedPlayerPos; // �G����U�����ꂽ�v���C���[�̍��W

    
    void Update()
    {
        if (mapGenerator == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("MapChip");
            mapGenerator = inst.GetComponent<MapGenerator>();
        }
        if (toshiPlayer == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Player");
            toshiPlayer = inst.GetComponent<toshiPlayer>();
        }        //if (toshiEnemy == null)
        //{
        //    GameObject inst = GameObject.FindGameObjectWithTag("Enemy");
        //    toshiEnemy = inst.GetComponent<toshiEnemy>();
        //}
        

        if (enemyTopDown == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Enemy");
            enemyTopDown = inst.GetComponent<EnemySlimeMoveComponent>();
        }
        
        if (enemyRightLeft == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Enemy");
            enemyRightLeft = inst.GetComponent<EnemyRightLeft>();
        }

        if (enemy_Zombie_RightLeft == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Enemy_Zombie");
            enemy_Zombie_RightLeft = inst.GetComponent<Enemy_Zombie_RightLeft>();
        }
        if (enemy_Zombie_TopDown == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Enemy_Zombie");
            enemy_Zombie_TopDown = inst.GetComponent<Enemy_Zombie_TopDown>();
        }

        if (enemyManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Enemy");
            enemyManager = inst.GetComponent<EnemyManager>();
        }
        //if (!isBlinking) return;

        //time += Time.deltaTime;

        //var repeatValue = Mathf.Repeat((float)time,cycle);

        //lifeArray[playerHP - 1].GetComponent<Image>().enabled =
        //    repeatValue >= cycle * 0.5f;
    }
    
    public IEnumerator Damage()
    {
        while(time < 0.5f)
        {
            time += Time.deltaTime;
            var repeatValue = Mathf.Repeat((float)time, cycle);

            lifeArray[playerHP - 1].GetComponent<Image>().enabled =
                repeatValue >= cycle * 0.5f;
            yield return null;
        }
        time = 0f;
        Hit();
    }
    public void Hit() 
    {
        lifeArray[playerHP - 1].GetComponent<Image>().enabled = false;
        playerHP--;
        if (playerHP == 0)
        {
            Destroy(playerObj); // �v���C���[�̃I�u�W�F�N�g��Destroy����
            mapGenerator.UpdateTile(toshiPlayer.playerCurrentPos, MapGenerator.MAP_TYPE.GROUND); // MAP_TYAPE�̍U�����ꂽPLAYER��GROUND�ɂ�����
        }
    }

    //public void StartBlink()
    //{
    //    //�_�ŏ���
    //    EndBlink();
    //}
}
