using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public enum State // ノードの状態
    {
        None, // 何もしない
        Open, // ノードをOpenにし、周りのcostとestimatedCostとscoreを求める
        Closed, // Openしたノードを閉じる
    }

    [SerializeField] private GameObject enemy = null; // 敵GameObject
    [SerializeField] private GameObject player = null; // プレイヤーGameObject
    private float enemyPosX; // 敵のX座標
    private float enemyPosY; // 敵のY座標

    private float cost = -1f; // 実際のコスト(敵の移動量)
    private float estimatedCost; // 推定コスト(敵がプレイヤーに追いつくまでの推定の移動量)
    private float score; // (costとestimatedCostを足した数)

    void Start()
    {
        
    }

    void Update()
    {
        if (enemy == null)
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy");
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void CalculateScore() // cost,estimatedCost,scoreを求める
    {
        float estimatedCostX;
        float estimatedCostY;

        cost++; // 敵はマップを1マスずつ動くので+1する

        estimatedCostX = player.transform.position.x - enemy.transform.position.x; // X座標の推定コストを求める
        estimatedCostY = player.transform.position.y - enemy.transform.position.y; // Y座標の推定コストを求める
        estimatedCost = estimatedCostX + estimatedCostY; // 推定コストはX座標とY座標をそれぞれ足して求める

        score = cost + estimatedCost; // scoreはcostとestimatedCostを足して求める
    }
}
