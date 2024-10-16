using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private float speed;
    private Vector2 direction; //向き
    //public bool isTouchingHeart; // ハートに接触しているかどうか
    private NoteSpawn noteSpawn; // NoteSpawnのインスタンスを参照
    public Function function;

    private void Start()
    {
        noteSpawn = FindObjectOfType<NoteSpawn>(); //NoteSpawnを探して参照を取得
        function = FindObjectOfType<Function>();
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

        if (function.isTouchingHeart && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed");
            
            noteSpawn.PlaySound1(); //NoteSpawnのPlaySoundメソッドを呼び出す
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) //heartに触れた瞬間
    {
        if(collider.CompareTag("heart"))
        {
            noteSpawn.PlaySound2();
        }
    }

    private void OnTriggerStay2D(Collider2D collider) //heartに触れているとき
    {
        if (collider.CompareTag("heart"))
        {
            function.isTouchingHeart = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) //heartに触れてから離れるとき
    {
        if (collider.CompareTag("heart"))
        {
            function.isTouchingHeart = false;
        }
    }
}