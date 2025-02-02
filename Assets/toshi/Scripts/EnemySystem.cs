using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    List<EnemyManager> enemyManagers = new();
    public void SetEnemyManagers(List<EnemyManager> enemyManagers)
    {
        this.enemyManagers = new(); // 初期化
        this.enemyManagers = enemyManagers; // 現在のMapの敵を格納
    }

    /// <summary>
    /// 攻撃する場所にいる敵を検索＋攻撃
    /// </summary>
    /// <param name="hitPosition">攻撃する場所</param>
    public void Hit(Vector2 hitPosition)
    {
        foreach(EnemyManager enemyManager in enemyManagers)
        {
            if(enemyManager.enemyCurrentPos != hitPosition) continue; // 攻撃する場所にいない場合スキップ
            enemyManager.Hit(); // いたら攻撃
        }
    }
}
