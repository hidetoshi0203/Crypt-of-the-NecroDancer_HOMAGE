using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn; // 出現させるオブジェクト
    [SerializeField] float spawnInterval = 2f; // 出現間隔
    [SerializeField] float moveSpeed = 2f; // 移動速度
    [SerializeField] float Spawnposition = 5f;



    private void Start()
    {
        InvokeRepeating("SpawnObjects", 0f, spawnInterval);

        
    }

    private void SpawnObjects()
    {
        // -5の位置からオブジェクトをスポーン
        Vector3 spawnPositionLeft = new Vector3(-Spawnposition, transform.position.y, transform.position.z);
        GameObject spawnedObjectLeft = Instantiate(objectToSpawn, spawnPositionLeft, Quaternion.identity);
        spawnedObjectLeft.AddComponent<MovingObject>().Initialize(moveSpeed, Vector2.right);

        // 5の位置からオブジェクトをスポーン
        Vector3 spawnPositionRight = new Vector3(Spawnposition, transform.position.y, transform.position.z);
        GameObject spawnedObjectRight = Instantiate(objectToSpawn, spawnPositionRight, Quaternion.identity);
        spawnedObjectRight.AddComponent<MovingObject>().Initialize(moveSpeed, Vector2.left);
    }
}




public class MovingObject : MonoBehaviour
{
    private float speed;
    private Vector2 direction;

    public AudioClip sound1;
    AudioSource audioSource;

    public void Initialize(float moveSpeed, Vector2 moveDirection)
    {
        speed = moveSpeed;
        direction = moveDirection.normalized;

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        // X軸が0に到達したらオブジェクトを削除
        if (Mathf.Abs(transform.position.x) < 0.01f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("heart"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //音(sound1)を鳴らす
                audioSource.PlayOneShot(sound1);

                Debug.Log("a");
            }
        }
        

    }
}