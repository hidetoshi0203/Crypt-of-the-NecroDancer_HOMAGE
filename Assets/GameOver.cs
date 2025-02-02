using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private GameObject checkAliveObjs;
    toshiPlayer toshiPlayer;
    PlayerManager playerManager;
    CheckAliveScripts checkAliveScripts;
    private void Start()
    {
        checkAliveObjs = GameObject.Find("CheckAliveObjects");
        playerManager = GetComponent<PlayerManager>();
        checkAliveScripts = checkAliveObjs.GetComponent<CheckAliveScripts>();
    }
    private void Update()
    {
        if (toshiPlayer == null && checkAliveScripts.isAliveToshiPlayerScr)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Player");
            toshiPlayer = inst.GetComponent<toshiPlayer>();
        }        //if (toshiEnemy == null)
        if (playerManager.playerHP == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
