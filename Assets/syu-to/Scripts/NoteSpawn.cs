using System.Collections;
using UnityEngine;

public class NoteSpawn : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn; // �o��������I�u�W�F�N�g
    [SerializeField] float spawnInterval = 2f; // �o���Ԋu
    [SerializeField] float moveSpeed = 2f; // �ړ����x
    [SerializeField] float spawnPosition = 5f; // �X�|�[���ʒu

    private void Start()
    {
        // ����I��SpawnObjects���\�b�h���Ăяo��
        InvokeRepeating("SpawnObjects", 0f, spawnInterval);
    }

    private void SpawnObjects()
    {
        // ��������I�u�W�F�N�g���X�|�[��
        Vector3 spawnPositionLeft = new Vector3(-spawnPosition, transform.position.y, transform.position.z);
        GameObject spawnedObjectLeft = Instantiate(objectToSpawn, spawnPositionLeft, Quaternion.identity);
        spawnedObjectLeft.AddComponent<Note>().Initialize(moveSpeed, Vector2.right);

        // �E������I�u�W�F�N�g���X�|�[��
        Vector3 spawnPositionRight = new Vector3(spawnPosition, transform.position.y, transform.position.z);
        GameObject spawnedObjectRight = Instantiate(objectToSpawn, spawnPositionRight, Quaternion.identity);
        spawnedObjectRight.AddComponent<Note>().Initialize(moveSpeed, Vector2.left);
    }
}
