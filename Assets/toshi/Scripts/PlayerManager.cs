using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    toshiEnemy toshiEnemy = null;  
    EnemyManager enemyManager = null;
    toshiPlayer toshiPlayer = null;
    MapGenerator mapGenerator = null;
    CheckAliveScripts checkAliveScripts;
    private GameObject checkAliveObjs;

    EnemySlimeMoveComponent enemyTopDown = null;
    EnemyRightLeft enemyRightLeft = null;
    Enemy_Zombie_RightLeft enemy_Zombie_RightLeft = null;
    Enemy_Zombie_TopDown enemy_Zombie_TopDown = null;

    [SerializeField] GameObject playerObj;
    //GameObject substitutePlayer; // Prefabに入ってぁE��PlayerObjの代わりにDestroyされるもの
    public GameObject[] lifeArray = new GameObject[3];
    public int playerHP = 3;
    [SerializeField] float cycle;
    bool isBlinking = false;
    double time;

    public Vector2Int attackedPlayerPos; // 敵から攻撁E��れたプレイヤーの座樁E

    AudioSource audioSource;
    [SerializeField] private AudioClip deadPlayerSE;

    private void Start()
    {
        //substitutePlayer = playerObj;
        checkAliveObjs = GameObject.Find("CheckAliveObjects");
        checkAliveScripts = checkAliveObjs.GetComponent<CheckAliveScripts>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (mapGenerator == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("MapChip");
            mapGenerator = inst.GetComponent<MapGenerator>();
        }
        if (toshiPlayer == null && checkAliveScripts.isAliveToshiPlayerScr)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Player");
            toshiPlayer = inst.GetComponent<toshiPlayer>();
        }        //if (toshiEnemy == null)
        
        if (enemyManager == null && checkAliveScripts.isAliveEnemyManagerScr)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Enemy");
            enemyManager = inst.GetComponent<EnemyManager>();
        }
        if (playerHP <= 1)
        {
            playerObj.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    
    public IEnumerator Damage()
    {
        while(time < 0.5f)
        {
            time += Time.deltaTime;
            var repeatValue = Mathf.Repeat((float)time, cycle);

            lifeArray[playerHP - 1].GetComponent<Image>().enabled =
                repeatValue >= cycle * 0.5f;
            yield return null;
        }
        time = 0f;
        Hit();
    }
    public void Hit() 
    {
        lifeArray[playerHP - 1].GetComponent<Image>().enabled = false;
        playerHP--;
        if (playerHP == 0)
        {

            mapGenerator.UpdateTile(toshiPlayer.playerCurrentPos, MapGenerator.MAP_TYPE.GROUND); // MAP_TYAPE�̍U�����ꂽPLAYER��GROUND�ɂ�����
            checkAliveScripts.isAliveToshiPlayerScr = false;
            //playerObj.SetActive(false);

            Destroy(playerObj); // プレイヤーのオブジェクトをDestroyする
            audioSource.PlayOneShot(deadPlayerSE);
            //playerObj.SetActive(false);

            mapGenerator.UpdateTile(toshiPlayer.playerCurrentPos, MapGenerator.MAP_TYPE.GROUND); // MAP_TYAPEの攻撁E��れたPLAYERをGROUNDにかえめE
            checkAliveScripts.isAliveToshiPlayerScr = false;
            //playerObj.SetActive(false);
            playerObj.GetComponent<SpriteRenderer>().enabled = false; 
        }
    }
}
