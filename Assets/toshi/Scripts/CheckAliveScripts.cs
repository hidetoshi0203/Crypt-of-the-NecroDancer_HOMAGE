using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckAliveScripts : MonoBehaviour
{
    //上記のスクリプトがアタッチされているオブジェクトの生死のフラグ
    public bool isAliveToshiPlayerScr = true; // RuitoshiPlayerのスクリプト0
    public bool isAliveEnemyManagerScr = true; // EnemyManagerのスクリプト0
    public bool isAliveRLSlimeScr = true; // RuiRL_Slimeのスクリプト
    public bool isAliveTDSlimeScr = true; // RuiTD_Slimeのスクリプト
    public bool isAliveruiRLZombieScr = true; // RuiRL_Zombieのスクリプト
    public bool isAliveruiTDZombieScr = true; // RuiTD_Zombieのスクリプト
    public bool isAliveCentaurScr = true; // RuiCentaurのスクリプト
    public bool isAliveItemScr = true; // Itemのスクリプト
}
