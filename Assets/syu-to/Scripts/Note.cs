using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private float speed;
    private Vector2 direction;
    private bool isTouchingHeart; // �n�[�g�ɐڐG���Ă��邩�ǂ���
    private NoteSpawn noteSpawn; // NoteSpawn�̃C���X�^���X���Q��

    private void Start()
    {
        // NoteSpawn��T���ĎQ�Ƃ��擾
        noteSpawn = FindObjectOfType<NoteSpawn>();
    }

    public void Initialize(float moveSpeed, Vector2 moveDirection)
    {
        speed = moveSpeed;
        direction = moveDirection.normalized; // �����𐳋K��
    }

    private void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime); // �I�u�W�F�N�g�̈ʒu���X�V

        if (Mathf.Abs(transform.position.x) < 0.01f) // X��0�ŃI�u�W�F�N�g�폜
        {
            Destroy(gameObject);
        }

        if (isTouchingHeart && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed");
            // NoteSpawn��PlaySound���\�b�h���Ăяo��
            noteSpawn.PlaySound();
        }
    }

    private void OnTriggerStay2D(Collider2D collider) // heart�ɐG��Ă���Ƃ�
    {
        if (collider.CompareTag("heart"))
        {
            isTouchingHeart = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) // heart�ɐG��Ă��痣���Ƃ�
    {
        if (collider.CompareTag("heart"))
        {
            isTouchingHeart = false;
        }
    }
}