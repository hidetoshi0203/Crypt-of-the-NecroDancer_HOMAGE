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

    //    public GameObject leftNoteObject;
    //    public GameObject rightNoteObject;

    bool isTouchingHeart = false;
    bool playingTouchSound = false;
    public bool canMove = false;
    public bool IsTouchingHeart => isTouchingHeart;

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

    public void OnTouchHeart()
    {
        if (!isTouchingHeart)
        {
            canMove = true;
            isTouchingHeart = true;
            PlayTouchSound();
        }
    }

    public void OnTimeLimit()
    {
        isTouchingHeart = false;
        StopTouchSound();
    }

    public bool CanInputKey()
    {
        return isTouchingHeart;
    }

    void PlayTouchSound()
    {
        audioSource.PlayOneShot(touchSound); //ハートに触れたときの音を鳴らす
        playingTouchSound = true;
    }

    public void StopTouchSound()
    {
        if (playingTouchSound)
        {
            audioSource.Stop(); //ハートに触れたときの音を停止
            playingTouchSound = false;
        }
    }

    public void PlaySpaceSound()
    {
        audioSource.PlayOneShot(spaceSound); //スペースキーを押したときの音を鳴らす
    }
}
