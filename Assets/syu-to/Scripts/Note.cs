using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private float speed;
    private Vector2 direction;
    private bool isTouchingHeart;

    public AudioClip sound1;
    private AudioSource audioSource;

    private void Start()
    {
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

        if (Mathf.Abs(transform.position.x) < 0.01f)
        {
            Destroy(gameObject);
        }

        // スペースキーが押されたときの処理
        if (isTouchingHeart && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed");
            PlaySound();
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("heart"))
        {
            isTouchingHeart = true;
            //Debug.Log("Collision detected");
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("heart"))
        {
            isTouchingHeart = false;
        }
    }

    private void PlaySound()
    {
        if (!audioSource.isPlaying) // すでに再生中でない場合のみ再生
        {
            audioSource.clip = sound1;
            audioSource.Play();
        }
    }
}