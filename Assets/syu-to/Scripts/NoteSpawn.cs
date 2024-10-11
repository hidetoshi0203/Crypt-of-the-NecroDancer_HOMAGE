using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawn : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn; //出現させるオブジェクト
    [SerializeField] float spawnInterval = 2f; //出現間隔
    [SerializeField] float moveSpeed = 2f; //移動速度
    [SerializeField] float spawnPosition = 5f; //スポーン位置
    [SerializeField] AudioClip sound1;
    private AudioSource audioSource; //AudioSourceコンポーネントを格納

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); //AudioSourceコンポーネントを取得

        InvokeRepeating("SpawnObjects", 0f, spawnInterval); //一定間隔でSpawnObjectsメソッドを呼び出す
    }

    private void SpawnObjects()
    {
        Vector3 spawnPositionRight = new Vector3(spawnPosition, transform.position.y, transform.position.z); //右側からオブジェクトをスポーン
        GameObject spawnedObjectRight = Instantiate(objectToSpawn, spawnPositionRight, Quaternion.identity);
        spawnedObjectRight.AddComponent<Note>().Initialize(moveSpeed, Vector2.left);

        Vector3 spawnPositionLeft = new Vector3(-spawnPosition, transform.position.y, transform.position.z); //左側からオブジェクトをスポーン
        GameObject spawnedObjectLeft = Instantiate(objectToSpawn, spawnPositionLeft, Quaternion.identity);
        spawnedObjectLeft.AddComponent<Note>().Initialize(moveSpeed, Vector2.right);
    }

    public void PlaySound()
    {
        if (audioSource != null && sound1 != null) //AudioSourceと音声クリップが設定されているとき
        {
            audioSource.PlayOneShot(sound1); //音を再生
        }
    }
}
