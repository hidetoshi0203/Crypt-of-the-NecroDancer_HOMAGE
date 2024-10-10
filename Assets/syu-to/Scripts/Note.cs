using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private float speed;
    private Vector2 direction;

    public AudioClip sound1;
    AudioSource audioSource;

    //private Rigidbody2D rb;

    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }


    public void Initialize(float moveSpeed, Vector2 moveDirection)
    {
        speed = moveSpeed;
        direction = moveDirection.normalized;

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

    private void OnTriggerStay2D(Collider2D collider)
    {

        if (collider.CompareTag("heart"))
        {
            Debug.Log("Collision detected");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Space pressed");

                audioSource.PlayOneShot(sound1);
            }
        }

    }
}