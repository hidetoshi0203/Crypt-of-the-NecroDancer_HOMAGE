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
    [SerializeField] private AudioClip touchSound; //�n�[�g�ɐG�ꂽ�Ƃ��̉�
    [SerializeField] private AudioClip spaceSound; //�X�y�[�X�L�[���������Ƃ��̉�

    //Camera cam;

    private float nextGenerateTime = 1f; //���̐����^�C�~���O
    private float nextTouchTime;
    [SerializeField] private float touchTime;
    private float generateTime; //�m�[�c�̐����Ԋu

    bool isTouchingHeart = false;
    bool canInputKey = false;
    bool playingTouchSound = false;

    public bool playerCanMove = false;
    public bool enemyCanMove = false;

    private ComboManager comboManager;
    private NotesCont notesController = null;

    public int moveControl; // �m�[�c���n�[�g�ɐG�ꂽ��
    public bool notesCountFlag;

    private void Awake()
    {
        generateTime = tempoManager.Tempo;
        nextGenerateTime = Time.time + generateTime;
        nextTouchTime = nextGenerateTime + tempo;
        audioSource = gameObject.AddComponent<AudioSource>(); //AudioSource��ǉ�

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
            // �L�[����
            //�@���͂��ꂽ��
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
        

        //notesController.OffTouchHeart(); //�n�[�g���痣��Ă���Ƃ�

    }


    public void UpdateGenerateTime(float newTempo)
    {
        float elapsedTime = Time.time - (nextGenerateTime - generateTime); // �o�ߎ��Ԃ��v�Z
        generateTime = newTempo;
        nextGenerateTime = Time.time + (generateTime - elapsedTime); // �␳
    }


    //�n�[�g�ɐG�ꂽ�Ƃ�
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

    //�n�[�g�ɐG����Ԃ��I������Ƃ�
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

    //���͉\���ǂ���
    public bool CanInputKey()
    {
        return isTouchingHeart;
    }

    //�n�[�g�ɐG�ꂽ�Ƃ��̉���炷
    public void PlayTouchSound()
    {
        audioSource.PlayOneShot(touchSound);
        playingTouchSound = true;
    }

    //�n�[�g�ɐG�ꂽ�Ƃ��̉����~
    public void StopTouchSound()
    {
        if (playingTouchSound)
        {
            audioSource.Stop();
            playingTouchSound = false;
        }
    }

    //�X�y�[�X�L�[���������Ƃ��̉���炷
    public void PlaySpaceSound()
    {
        audioSource.PlayOneShot(spaceSound);
    }
}