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
    [SerializeField] private AudioClip touchSound; // �n�[�g�ɐG�ꂽ�Ƃ��̉�
    [SerializeField] private AudioClip spaceSound; // �X�y�[�X�L�[���������Ƃ��̉�


    private float nextGenerateTime = 1f;
    private float generateTime = 1f;

    private void Awake()
    {
        generateTime = tempoManager.Tempo;
        nextGenerateTime = Time.time + generateTime;

        audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource��ǉ�
    }
    private void Update()
    {
        if (Time.time > nextGenerateTime)
        {
            //Debug.Log(Time.time);
            Instantiate(leftNode, leftGenerateTrans.position, Quaternion.identity, this.transform);
            Instantiate(rightNode, rightGenerateTrans.position, Quaternion.identity, this.transform);
            nextGenerateTime += generateTime;
        }
    }

    public void PlayTouchSound()
    {
        audioSource.PlayOneShot(touchSound); // �n�[�g�ɐG�ꂽ�Ƃ��̉���炷
    }

    public void PlaySpaceSound()
    {
        audioSource.PlayOneShot(spaceSound); // �X�y�[�X�L�[���������Ƃ��̉���炷
    }
}