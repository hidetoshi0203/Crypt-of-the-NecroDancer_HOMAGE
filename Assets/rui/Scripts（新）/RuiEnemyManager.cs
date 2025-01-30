using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuiEnemyManager : MonoBehaviour
{
    RuitoshiPlayer ToshiPlayer = null;
    public Vector2Int enemyCurrentPos , enemyNextPos;       // 今の座標
    public Vector2Int attackedEnemyPos; // プレイヤーから攻撃された敵オブジェクトの座標
    RuiMapGenerator mapGenerator;
    private int hp = 0;
    public List<GameObject> HeartObjList;
    //[SerializeField] GameObject HeartObj;
    //[SerializeField] GameObject Spawner;
    //[SerializeField] GameObject Spawner1;

    CheckAliveScripts checkAliveScripts;
    private GameObject checkAliveObjs;

    void Start()
    {
        mapGenerator = transform.parent.GetComponent<RuiMapGenerator>();
        hp = HeartObjList.Count;
        //Instantiate(HeartObj, Spawner.transform.position, Quaternion.identity);
        //Instantiate(HeartObj, Spawner1.transform.position, Quaternion.identity);
        checkAliveObjs = GameObject.Find("CheckAliveObjects");
        checkAliveScripts = checkAliveObjs.GetComponent<CheckAliveScripts>();
    }
    private void Update()
    {
        if (ToshiPlayer == null) 
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Player");
            ToshiPlayer = inst.GetComponent<RuitoshiPlayer>();
        }
    
        if (ToshiPlayer.isAttack)
        {
            attackedEnemyPos = ToshiPlayer.playerNextPos;　// プレイヤーのnextPosを代入する
            if (attackedEnemyPos == enemyCurrentPos) // プレイヤーから攻撃された座標と敵の座標を比べる
            { 
                Destroy(gameObject); // 敵のオブジェクトをDestroyする
                mapGenerator.UpdateTile(enemyCurrentPos, RuiMapGenerator.MAP_TYPE.GROUND); // MAP_TYAPEの攻撃されたENEMYをGROUNDにかえる
            }
        }
    }
    public void Hit()
    {
        hp--;
        if(hp < 0)
        {
            Destroy(gameObject); // 敵のオブジェクトをDestroyする
            mapGenerator.UpdateTile(enemyCurrentPos, RuiMapGenerator.MAP_TYPE.GROUND); // MAP_TYAPEの攻撃されたENEMYをGROUNDにかえる
            checkAliveScripts.isAliveEnemyManagerScr = false;
        }
        HeartObjList[hp].SetActive(false);
    }
}
