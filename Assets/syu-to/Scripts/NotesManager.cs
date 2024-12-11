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
    [SerializeField] private AudioClip touchSound; //�n�[�g�ɐG�ꂽ�Ƃ��̉�
    [SerializeField] private AudioClip spaceSound; //�X�y�[�X�L�[���������Ƃ��̉�

    Camera cam;

    private float nextGenerateTime = 1f; //���̐����^�C�~���O
    private float generateTime = 1f; //�m�[�c�̐����Ԋu

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

        audioSource = gameObject.AddComponent<AudioSource>(); //AudioSource��ǉ�

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
        

        notesController.OffTouchHeart(); //�n�[�g���痣��Ă���Ƃ�

    }

    public void UpdateGenerateTime(float newTempo)
    {
        generateTime = newTempo; // �V�����e���|�Ɋ�Â��Đ����Ԋu���X�V
        nextGenerateTime = Time.time + generateTime; // ���̃m�[�c�����^�C�~���O���Đݒ�
    }

    //�n�[�g�ɐG�ꂽ�Ƃ�
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

    //�n�[�g�ɐG����Ԃ��I������Ƃ�
    public void OnTimeLimit()
    {
        isTouchingHeart = false;
        StopTouchSound();
        defaultHeart.transform.localScale = reSizeHeart;
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