using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private float speed;
    private Vector2 direction; //向き
    public bool isTouchingHeart; // ハートに接触しているかどうか
    private NoteSpawn noteSpawn; // NoteSpawnのインスタンスを参照

    private void Start()
    {
        noteSpawn = FindObjectOfType<NoteSpawn>(); //NoteSpawnを探して参照を取得
    }

    public void Initialize(float moveSpeed, Vector2 moveDirection)
    {
        speed = moveSpeed;
        direction = moveDirection.normalized; // 方向を正規化
    }

    private void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime); // オブジェクトの位置を更新

        if (Mathf.Abs(transform.position.x) < 0.01f) // X軸0でオブジェクト削除
        {
            Destroy(gameObject);
        }

        if (isTouchingHeart && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed");
            
            noteSpawn.PlaySound(); //NoteSpawnのPlaySoundメソッドを呼び出す
        }
    }

    private void OnTriggerStay2D(Collider2D collider) // heartに触れているとき
    {
        if (collider.CompareTag("heart"))
        {
            isTouchingHeart = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) // heartに触れてから離れるとき
    {
        if (collider.CompareTag("heart"))
        {
            isTouchingHeart = false;
        }
    }
}