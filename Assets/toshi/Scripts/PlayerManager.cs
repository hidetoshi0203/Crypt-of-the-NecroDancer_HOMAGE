using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    LifeManager lifeManager;
    toshiEnemy ToshiEnemy = null;  
    EnemyManager enemyManager = null;
    toshiPlayer ToshiPlayer = null;
    MapGenerator mapGenerator;

    public Vector2Int attackedPlayerPos; // 敵から攻撃されたプレイヤーの座標
    
    void Start()
    {
        
    }

    void Update()
    {
        if (mapGenerator == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("GameManager");
            lifeManager = inst.GetComponent<LifeManager>();
        }
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
            if (lifeManager.playerHP == 0)
            {
                attackedPlayerPos = enemyManager.enemyNextPos; // 敵のnextPosを代入する
                if (attackedPlayerPos == ToshiPlayer.playerCurrentPos) // 敵から攻撃された座標とプレイヤーの座標を比べる
                {
                    Destroy(gameObject); // プレイヤーのオブジェクトをDestroyする
                }
            }
        }
    }
}
