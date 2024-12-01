using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    toshiEnemy ToshiEnemy = null;
    EnemyManager enemyManager = null;
    toshiPlayer ToshiPlayer = null;
    public Vector2Int attackedPlayerPos; // 敵から攻撃されたプレイヤーの座標
    MapGenerator mapGenerator;
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
            Debug.Log("敵の攻撃");
            attackedPlayerPos = enemyManager.enemyNextPos; // 敵のnextPosを代入する
            if (attackedPlayerPos == ToshiPlayer.playerCurrentPos) // 敵から攻撃された座標とプレイヤーの座標を比べる
            {
                Destroy(gameObject); // プレイヤーのオブジェクトをDestroyする
               
            }
        }
    }
}
