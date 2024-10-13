using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawn : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn; //�o��������I�u�W�F�N�g
    [SerializeField] float spawnInterval = 2f; //�o���Ԋu
    [SerializeField] float moveSpeed = 2f; //�ړ����x

    [SerializeField] float spawnPositionX = 5f; //�X�|�[���ʒu
    [SerializeField] float spawnPositionY = 5f; //�X�|�[���ʒu


    [SerializeField] AudioClip sound1;
    [SerializeField] AudioClip sound2;
    AudioSource audioSource; //AudioSource�R���|�[�l���g���i�[

    private Function function;

    private void Start()
    {
        function = FindObjectOfType<Function>();
        audioSource = GetComponent<AudioSource>();

        InvokeRepeating("SpawnObjects", 0f, spawnInterval); //���Ԋu��SpawnObjects���\�b�h���Ăяo��

    }

    private void SpawnObjects()
    {
        Vector3 spawnPositionRight = new Vector3(spawnPositionX, spawnPositionY, transform.position.z); ; //�E������I�u�W�F�N�g���X�|�[��
        GameObject spawnedObjectRight = Instantiate(objectToSpawn, spawnPositionRight, Quaternion.identity);
        spawnedObjectRight.AddComponent<Note>().Initialize(moveSpeed, Vector2.left);

        Vector3 spawnPositionLeft = new Vector3(-spawnPositionX, spawnPositionY, transform.position.z); ; //��������I�u�W�F�N�g���X�|�[��
        GameObject spawnedObjectLeft = Instantiate(objectToSpawn, spawnPositionLeft, Quaternion.identity);
        spawnedObjectLeft.AddComponent<Note>().Initialize(moveSpeed, Vector2.right);
    }

    public void PlaySound1()
    {
        if (audioSource != null && sound1 != null) //AudioSource�Ɖ����N���b�v���ݒ肳��Ă���Ƃ�
        {
            audioSource.PlayOneShot(sound1); //�����Đ�
        }
    }

    public void PlaySound2()
    {
        if (audioSource != null && sound2 != null) //AudioSource�Ɖ����N���b�v���ݒ肳��Ă���Ƃ�
        {
            audioSource.PlayOneShot(sound2); //�����Đ�
        }
    }
}
