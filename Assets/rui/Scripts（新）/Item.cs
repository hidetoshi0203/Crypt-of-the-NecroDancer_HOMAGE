using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Vector2Int myPosition;
    public bool aliveItemScript;
    //RuiMapGenerator ruiMapGenerator = null;
    //RuitoshiPlayer ruiToshiPlayer = null;
    //RuiPlayerManager ruiPlayerManager = null;

    //GameObject HPotionObj;
    //GameObject SPotionObj;
    //public bool isAliveHPotion = true; // 回復ポーションが存在しているか
    //public bool isAliveSPotion = true; // 攻撃力UPポーションが存在しているか

    //public Vector2Int HPotionCurrentPos; // 現在の回復ポーションの位置
    //public Vector2Int SPotionCurrentPos; // 現在の攻撃力UPポーションの位置
    //public Vector2Int takenHPotionPos; // プレイヤーが回復ポーションを取った位置
    //public Vector2Int takenSPotionPos; // プレイヤーが攻撃力UPポーションを取った位置

    //// Update is called once per frame
    //void Update()
    //{
    //    Debug.Log(takenHPotionPos);
    //    if (ruiMapGenerator == null)
    //    {
    //        GameObject inst = GameObject.FindGameObjectWithTag("MapChip");
    //        ruiMapGenerator = inst.GetComponent<RuiMapGenerator>();
    //    }

    //    if (ruiPlayerManager == null)
    //    {
    //        GameObject inst = GameObject.FindGameObjectWithTag("PlayerManager");
    //        ruiPlayerManager = inst.GetComponent<RuiPlayerManager>();
    //    }

    //    if (ruiToshiPlayer == null)
    //    {
    //        GameObject inst = GameObject.FindGameObjectWithTag("Player");
    //        ruiToshiPlayer = inst.GetComponent<RuitoshiPlayer>();
    //    }
    //    //HPotionObj = GameObject.FindGameObjectWithTag("HealingPotion");
    //    //SPotionObj = GameObject.FindGameObjectWithTag("StrengthPotion");
    //}

    //public void HealingHP() // プレイヤーのHPを回復する関数
    //{
    //    if (ruiPlayerManager.playerHP < 3) // プレイヤーの体力が減ってたら(3HP未満だったら)
    //    {
    //        ruiPlayerManager.playerHP++; // プレイヤー体力(HP)を回復する
    //    }
    //    //takenHPotionPos = ruiToshiPlayer.playerNextPos;
    //    //if (takenHPotionPos == HPotionCurrentPos)
    //    //{
    //    //    Debug.Log("aaa");
    //    //    if (ruiPlayerManager.playerHP < 3) // プレイヤーの体力が減ってたら(3HP未満だったら)
    //    //    {
    //    //        ruiPlayerManager.playerHP++; // プレイヤー体力(HP)を回復する
    //    //    }
    //    //    Destroy(HPotionObj);
    //    //    isAliveHPotion = false; // プレイヤーが回復ポーションを取ったので、回復ポーションを消す
    //    //}
    //}
}
