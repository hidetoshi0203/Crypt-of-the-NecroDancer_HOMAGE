using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    toshiPlayer ToshiPlayer = null;
    public Vector2Int enemyCurrentPos , enemyNextPos;       // 今の座標
    public Vector2Int attackedEnemyPos; // プレイヤーから攻撃された敵オブジェクトの座標
    MapGenerator mapGenerator;
    private int hp = 0;
    public List<GameObject> HeartObjList;

    CheckAliveScripts checkAliveScripts;
    private GameObject checkAliveObjs;

    void Start()
    {
        mapGenerator = transform.parent.GetComponent<MapGenerator>();
        hp = HeartObjList.Count;
        checkAliveObjs = GameObject.Find("CheckAliveObjects");
        checkAliveScripts = checkAliveObjs.GetComponent<CheckAliveScripts>();
    }
    private void Update()
    {
        if (ToshiPlayer == null && checkAliveScripts.isAliveToshiPlayerScr)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Player");
            ToshiPlayer = inst.GetComponent<toshiPlayer>();
        }
    }
    public void Hit()
    {
        attackedEnemyPos = ToshiPlayer.playerNextPos; // プレイヤーのnextPosを代入する
        if (attackedEnemyPos == enemyCurrentPos) // プレイヤーから攻撃された座標と敵の座標を比べる
        {
            hp -= ToshiPlayer.playerAttackPower;
            if(hp <= 0)
            {
                Destroy(gameObject); // 敵のオブジェクトをDestroyする
                mapGenerator.UpdateTile(enemyCurrentPos, MapGenerator.MAP_TYPE.GROUND); // MAP_TYAPEの攻撃されたENEMYをGROUNDにかえる
                checkAliveScripts.isAliveEnemyManagerScr = false;
            }
          HeartObjList[hp].SetActive(false);
        }
    }
}
