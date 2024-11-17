using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesManager : MonoBehaviour
{
    [SerializeField] private TempoManager tempoManager;
    [SerializeField] private GameObject leftNode;
    [SerializeField] private GameObject rightNode;
    [SerializeField] private Transform leftGenerateTrans;
    [SerializeField] private Transform rightGenerateTrans;

    private AudioSource audioSource;
    [SerializeField] private AudioClip touchSound; //ハートに触れたときの音
    [SerializeField] private AudioClip spaceSound; //スペースキーを押したときの音

    private float nextGenerateTime = 1f;
    private float generateTime = 1f;

    bool isTouchingHeart = false;
    bool playingTouchSound = false;

    public bool playerCanMove = false;
    public bool enemyCanMove = false;
    //public bool IsTouchingHeart => isTouchingHeart;

    private void Awake()
    {
        generateTime = tempoManager.Tempo;
        nextGenerateTime = Time.time + generateTime;

        audioSource = gameObject.AddComponent<AudioSource>(); //AudioSourceを追加
    }

    private void Update()
    {
        if (Time.time > nextGenerateTime)
        {
            Instantiate(leftNode, leftGenerateTrans.position, Quaternion.identity, this.transform);
            Instantiate(rightNode, rightGenerateTrans.position, Quaternion.identity, this.transform);
            nextGenerateTime += generateTime;
        }
    }

    //ハートに触れたとき
    public void OnTouchHeart()
    {
        if (!isTouchingHeart)
        {
            playerCanMove = true;
            enemyCanMove = true;
            isTouchingHeart = true;
            PlayTouchSound();
        }
    }

    //ハートに触れる状態が終わったとき
    public void OnTimeLimit()
    {
        isTouchingHeart = false;
        StopTouchSound();
    }

    //入力可能かどうか
    public bool CanInputKey()
    {
        return isTouchingHeart;
    }

    //ハートに触れたときの音を鳴らす
    public void PlayTouchSound()
    {
        audioSource.PlayOneShot(touchSound);
        playingTouchSound = true;
    }

    //ハートに触れたときの音を停止
    public void StopTouchSound()
    {
        if (playingTouchSound)
        {
            audioSource.Stop();
            playingTouchSound = false;
        }
    }

    //スペースキーを押したときの音を鳴らす
    public void PlaySpaceSound()
    {
        audioSource.PlayOneShot(spaceSound);
    }
}