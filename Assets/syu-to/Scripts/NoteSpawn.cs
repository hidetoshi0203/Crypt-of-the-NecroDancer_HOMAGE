using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawn : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn; //�o��������I�u�W�F�N�g
    [SerializeField] float spawnInterval = 2f; //�o���Ԋu
    [SerializeField] float moveSpeed = 2f; //�ړ����x
    [SerializeField] float spawnPosition = 5f; //�X�|�[���ʒu
    [SerializeField] AudioClip sound1;
    private AudioSource audioSource; //AudioSource�R���|�[�l���g���i�[

    private Vector3 pPos;
    private float spawnY = -5.0f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); //AudioSource�R���|�[�l���g���擾

        InvokeRepeating("SpawnObjects", 0f, spawnInterval); //���Ԋu��SpawnObjects���\�b�h���Ăяo��

        pPos = new Vector3(spawnPosition, spawnY, transform.position.z);
    }

    private void SpawnObjects()
    {
        Vector3 spawnPositionRight = pPos; //�E������I�u�W�F�N�g���X�|�[��
        GameObject spawnedObjectRight = Instantiate(objectToSpawn, spawnPositionRight, Quaternion.identity);
        spawnedObjectRight.AddComponent<Note>().Initialize(moveSpeed, Vector2.left);

        Vector3 spawnPositionLeft = pPos; //��������I�u�W�F�N�g���X�|�[��
        GameObject spawnedObjectLeft = Instantiate(objectToSpawn, spawnPositionLeft, Quaternion.identity);
        spawnedObjectLeft.AddComponent<Note>().Initialize(moveSpeed, Vector2.right);
    }

    public void PlaySound()
    {
        if (audioSource != null && sound1 != null) //AudioSource�Ɖ����N���b�v���ݒ肳��Ă���Ƃ�
        {
            audioSource.PlayOneShot(sound1); //�����Đ�
        }
    }
}
