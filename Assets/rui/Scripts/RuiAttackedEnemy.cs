using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuiAttackedEnemy : MonoBehaviour
{
    RuiPlayerManager ruiPlayerManager = null;
    public Vector2Int enemyCurrentPos, enemyNextPos;       // 今の座標
    public Vector2Int attackedEnemyPos; // プレイヤーから攻撃された敵オブジェクトの座標
    MapGenerator mapGenerator;
    void Start()
    {
        mapGenerator = transform.parent.GetComponent<MapGenerator>();
    }
    private void Update()
    {
        if (ruiPlayerManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Player");
            ruiPlayerManager = inst.GetComponent<RuiPlayerManager>();
        }

        if (ruiPlayerManager.isPlayerAttack)
        {
            attackedEnemyPos = ruiPlayerManager.playerNextPos;　// プレイヤーのnextPosを代入する
            if (attackedEnemyPos == enemyCurrentPos) // プレイヤーから攻撃された座標と敵の座標を比べる
            {
                Destroy(gameObject); // 敵のオブジェクトをDestroyする
                //mapGenerator.UpdateTilie(enemyCurrentPos, MapGenerator.MAP_TYPE.GROUND); // MAP_TYAPEの攻撃されたENEMYをGROUNDにかえる
            }

        }
    }
}
