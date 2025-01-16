using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuiPlayerManager : MonoBehaviour
{
    RuitoshiEnemy toshiEnemy = null;  
    RuiEnemyManager enemyManager = null;
    RuitoshiPlayer toshiPlayer = null;
    RuiMapGenerator mapGenerator = null;
    RuiEnemy_Zombie enemy_Zombie = null;

    [SerializeField] GameObject playerObj;
    public GameObject[] lifeArray = new GameObject[3];
    public int playerHP = 3;

    public Vector2Int attackedPlayerPos; // 敵から攻撃されたプレイヤーの座標
    
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
        if (toshiEnemy == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Enemy");
            toshiEnemy = inst.GetComponent<RuitoshiEnemy>();
        }
        if (enemy_Zombie == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Enemy_Zombie");
            enemy_Zombie = inst.GetComponent<RuiEnemy_Zombie>();
        }
        if (enemyManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Enemy");
            enemyManager = inst.GetComponent<RuiEnemyManager>();
        }

        //if (toshiEnemy.isEnemyAttack)
        //{
        //    Debug.Log("敵からの攻撃");
           
        //    attackedPlayerPos = enemyManager.enemyNextPos; // 敵のnextPosを代入する
        //    if (attackedPlayerPos == toshiPlayer.playerCurrentPos) // 敵から攻撃された座標とプレイヤーの座標を比べる
        //    {
        //        Debug.Log(attackedPlayerPos);
                
        //        lifeArray[playerHP - 1].SetActive(false);
        //        playerHP--;
        //        if (playerHP == 0)
        //        {
        //            Destroy(gameObject); // プレイヤーのオブジェクトをDestroyする
        //            mapGenerator.UpdateTile(toshiPlayer.playerCurrentPos, MapGenerator.MAP_TYPE.GROUND); // MAP_TYAPEの攻撃されたPLAYERをGROUNDにかえる
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
            Destroy(playerObj); // プレイヤーのオブジェクトをDestroyする
            mapGenerator.UpdateTile(toshiPlayer.playerCurrentPos, RuiMapGenerator.MAP_TYPE.GROUND); // MAP_TYAPEの攻撃されたPLAYERをGROUNDにかえる
        }
    }
}
