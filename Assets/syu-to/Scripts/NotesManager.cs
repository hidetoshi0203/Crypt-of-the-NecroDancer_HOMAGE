using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UIElements;

public class NotesManager : MonoBehaviour
{
    [SerializeField] private TempoManager tempoManager;
    [SerializeField] private GameObject leftNode;
    [SerializeField] private GameObject rightNode;
    [SerializeField] private Transform leftGenerateTrans;
    [SerializeField] private Transform rightGenerateTrans;
    [SerializeField] GameObject defaultHeart;
    [SerializeField] Vector3 reSizeHeart;
    private AudioSource audioSource;
    [SerializeField] private AudioClip touchSound; //ハートに触れたときの音
    [SerializeField] private AudioClip spaceSound; //スペースキーを押したときの音

    Camera cam;

    private float nextGenerateTime = 1f; //次の生成タイミング
    private float generateTime = 1f; //ノーツの生成間隔

    bool isTouchingHeart = false;
    bool playingTouchSound = false;

    public bool playerCanMove = false;
    public bool enemyCanMove = false;

    private ComboManager comboManager;
    private NotesController notesController = null;

    private void Awake()
    {
        generateTime = tempoManager.Tempo;
        nextGenerateTime = Time.time + generateTime;

        audioSource = gameObject.AddComponent<AudioSource>(); //AudioSourceを追加

        notesController = FindObjectOfType<NotesController>();
        comboManager = FindObjectOfType<ComboManager>();

        comboManager.comboreset = false;

        reSizeHeart = defaultHeart.transform.localScale;
        cam = Camera.main;
    }

    private void Update()
    {
        if (Time.time > nextGenerateTime)
        {
            Instantiate(leftNode, leftGenerateTrans.position, Quaternion.identity, this.transform);
            Instantiate(rightNode, rightGenerateTrans.position, Quaternion.identity, this.transform);
            nextGenerateTime += generateTime;
        }

        if (notesController == null)
        {
            GameObject NCon = GameObject.FindGameObjectWithTag("Notes");
            if (NCon != null)
            {
                notesController = NCon.GetComponent<NotesController>();
            }
            else
            {
                return;
            }
        }
        

        notesController.OffTouchHeart(); //ハートから離れているとき

    }

    public void UpdateGenerateTime(float newTempo)
    {
        generateTime = newTempo; // 新しいテンポに基づいて生成間隔を更新
        nextGenerateTime = Time.time + generateTime; // 次のノーツ生成タイミングを再設定
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
        defaultHeart.transform.localScale = new Vector3(2, 2, 2);
    }

    //ハートに触れる状態が終わったとき
    public void OnTimeLimit()
    {
        isTouchingHeart = false;
        StopTouchSound();
        defaultHeart.transform.localScale = reSizeHeart;
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