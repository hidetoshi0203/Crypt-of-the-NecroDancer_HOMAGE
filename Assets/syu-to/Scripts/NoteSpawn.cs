using System.Collections;
using UnityEngine;

public class NoteSpawn : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn; // 出現させるオブジェクト
    [SerializeField] float spawnInterval = 2f; // 出現間隔
    [SerializeField] float moveSpeed = 2f; // 移動速度
    [SerializeField] float spawnPosition = 5f; // スポーン位置

    private void Start()
    {
        // 定期的にSpawnObjectsメソッドを呼び出す
        InvokeRepeating("SpawnObjects", 0f, spawnInterval);
    }

    private void SpawnObjects()
    {
        // 左側からオブジェクトをスポーン
        Vector3 spawnPositionLeft = new Vector3(-spawnPosition, transform.position.y, transform.position.z);
        GameObject spawnedObjectLeft = Instantiate(objectToSpawn, spawnPositionLeft, Quaternion.identity);
        spawnedObjectLeft.AddComponent<Note>().Initialize(moveSpeed, Vector2.right);

        // 右側からオブジェクトをスポーン
        Vector3 spawnPositionRight = new Vector3(spawnPosition, transform.position.y, transform.position.z);
        GameObject spawnedObjectRight = Instantiate(objectToSpawn, spawnPositionRight, Quaternion.identity);
        spawnedObjectRight.AddComponent<Note>().Initialize(moveSpeed, Vector2.left);
    }
}
