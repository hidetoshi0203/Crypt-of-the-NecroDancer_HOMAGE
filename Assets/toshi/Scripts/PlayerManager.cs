using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    toshiEnemy toshiEnemy = null;  
    EnemyManager enemyManager = null;
    toshiPlayer toshiPlayer = null;
    MapGenerator mapGenerator = null;
    Enemy_Zombie enemy_Zombie = null;

    public GameObject[] lifeArray = new GameObject[3];
    int playerHP = 3;

    public Vector2Int attackedPlayerPos; // 敵から攻撃されたプレイヤーの座標
    
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
        }
        if (toshiEnemy == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Enemy");
            toshiEnemy = inst.GetComponent<toshiEnemy>();
        }
        if (enemy_Zombie == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Enemy_Zombie");
            enemy_Zombie = inst.GetComponent<Enemy_Zombie>();
        }
        if (enemyManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Enemy");
            enemyManager = inst.GetComponent<EnemyManager>();
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
            Destroy(gameObject); // プレイヤーのオブジェクトをDestroyする
            mapGenerator.UpdateTile(toshiPlayer.playerCurrentPos, MapGenerator.MAP_TYPE.GROUND); // MAP_TYAPEの攻撃されたPLAYERをGROUNDにかえる
        }
    }
}
