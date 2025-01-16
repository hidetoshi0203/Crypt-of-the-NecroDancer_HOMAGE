using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UIElements;

public class NotesManager : MonoBehaviour
{
    [SerializeField] private TempoManager tempoManager;
    [SerializeField] private GameObject node;
    [SerializeField] private float tempo = 2.0f;
    [SerializeField] private Transform generateTrans;
    [SerializeField] private Transform leftGenerateTrans;
    [SerializeField] private Transform rightGenerateTrans;
    [SerializeField] GameObject defaultHeart;
    [SerializeField] Vector3 reSizeHeart;
    private AudioSource audioSource;
    [SerializeField] private AudioClip touchSound; //ハートに触れたときの音
    [SerializeField] private AudioClip spaceSound; //スペースキーを押したときの音

    //Camera cam;

    private float nextGenerateTime = 1f; //次の生成タイミング
    private float nextTouchTime;
    [SerializeField] private float touchTime;
    private float generateTime; //ノーツの生成間隔

    bool isTouchingHeart = false;
    bool canInputKey = false;
    bool playingTouchSound = false;

    public bool playerCanMove = false;
    public bool enemyCanMove = false;

    private ComboManager comboManager;
    private NotesCont notesController = null;

    public int moveControl; // ノーツがハートに触れた回数
    public bool notesCountFlag;

    private void Awake()
    {
        generateTime = tempoManager.Tempo;
        nextGenerateTime = Time.time + generateTime;
        nextTouchTime = nextGenerateTime + tempo;
        audioSource = gameObject.AddComponent<AudioSource>(); //AudioSourceを追加

        notesController = FindObjectOfType<NotesCont>();
        comboManager = FindObjectOfType<ComboManager>();

        comboManager.comboreset = false;

        reSizeHeart = defaultHeart.transform.localScale;
    }

    private void Update()
    {
        
        if (Time.time > nextGenerateTime)
        {
            //Instantiate(leftNode, leftGenerateTrans.position, Quaternion.identity, this.transform);
            //Instantiate(rightNode, rightGenerateTrans.position, Quaternion.identity, this.transform);
            GameObject n = Instantiate(node, generateTrans.position, Quaternion.identity, this.transform);
            NotesCont notesCont = n.GetComponent<NotesCont>();
            notesCont.Set(leftGenerateTrans.position, rightGenerateTrans.position);
            nextGenerateTime += generateTime;
        }
        
        if(Time.time < nextTouchTime + touchTime && Time.time > nextTouchTime - touchTime)
        {
            OnTouchHeart();
        }
        else
        {
            if (isTouchingHeart)
            {
                isTouchingHeart = false;
                nextTouchTime += tempoManager.Tempo;
                OnTimeLimit();
            }
        }
        if (isTouchingHeart && canInputKey)
        {
            // キー入力
            //　入力されたら
            if (Input.GetKeyDown(KeyCode.W)) 
            {
                canInputKey = false;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                canInputKey = false;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                canInputKey = false;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                canInputKey = false;
            }
        }
        

        //notesController.OffTouchHeart(); //ハートから離れているとき

    }


    public void UpdateGenerateTime(float newTempo)
    {
        float elapsedTime = Time.time - (nextGenerateTime - generateTime); // 経過時間を計算
        generateTime = newTempo;
        nextGenerateTime = Time.time + (generateTime - elapsedTime); // 補正
    }


    //ハートに触れたとき
    public void OnTouchHeart()
    {
        if (!isTouchingHeart)
        {
            playerCanMove = true;
            isTouchingHeart = true;
            canInputKey = true;
            PlayTouchSound();
        }
        defaultHeart.transform.localScale = new Vector3(2, 2, 2);
    }

    //ハートに触れる状態が終わったとき
    public void OnTimeLimit()
    {
        Debug.Log("move");
        isTouchingHeart = false;
        enemyCanMove = true;
        notesCountFlag = true;
        StopTouchSound();
        defaultHeart.transform.localScale = reSizeHeart;
        moveControl = (moveControl + 1) % 2;
        Debug.Log(moveControl);
        //if (notesCountFlag)
        //{
        //    moveControl++;
        //    if (moveControl >= 1)
        //    {
        //        notesCountFlag = false;
        //    }
        //}
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