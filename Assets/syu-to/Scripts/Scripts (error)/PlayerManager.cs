/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private TempoManager tempoManager;
    //[SerializeField] private ComboMove comboMove;
    [SerializeField] private float activeTime = 0.1f;
    private float startDelay;
    private float delay;
    private float nextActiveTime = 0f;
    private float nextDeactiveTime = 0f;
//  [SerializeField] private GameObject debugActive;

    [SerializeField] private GameObject bullet;

    private bool alive = true;

    public float invincibilityDuration = 5.0f; // 無敵時間（秒）
    private float invincibilityTimer = 0.0f;   // 経過時間を格納するタイマー変数(初期値0秒)
    public bool isInvincible = false;         // 無敵状態かどうかのフラグ

    private bool burstModeEnabled = false; // バーストモードが有効かどうかのフラグ

    private int Combo = 0;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        startDelay = tempoManager.StartDelay;
        delay = activeTime / 2f;
        nextActiveTime = Time.time + tempoManager.Tempo - delay + startDelay;
        nextDeactiveTime = nextActiveTime + activeTime;
    }


    private void Update()
    {
        if (Time.time > nextActiveTime)
        {
            // debugActive.SetActive(true);
            // Debug.Log("move");
            Active();
        }
        else
        {
            Inactive();
        }
        
        if (Time.time > nextDeactiveTime)
        {
//          debugActive.SetActive(false);
            UpdateActiveTime();
        }
        
    }
    private void UpdateActiveTime()
    {
        nextActiveTime += tempoManager.Tempo;
        nextDeactiveTime += tempoManager.Tempo;
    }
    private void Active()
    {
      //  if (alive)
        

            if (Input.GetKeyDown(KeyCode.W) && CanUp()) //上
            {
                transform.position += new Vector3(0f, 1.5f, 0f);
                
            }
            else if (Input.GetKeyDown(KeyCode.S) && CanDown()) //下
            {
                transform.position += new Vector3(0f, -1.5f, 0f);
                
            }
            else if (Input.GetKeyDown(KeyCode.Space)) //発射
            {
                if (!burstModeEnabled)
                {

                }
                
            }

            bool CanUp() => transform.localPosition.y < 2f;
            bool CanDown() => transform.localPosition.y > -2.8f;
        
    }

    private void Inactive()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }

    
}*/