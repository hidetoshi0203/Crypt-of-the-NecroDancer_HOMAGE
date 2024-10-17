using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesController : MonoBehaviour
{
    [SerializeField] private float endX = 0.5f; // ノーツの移動先のX座標
    [SerializeField] private float tempo = 2.0f; // ノーツの移動時間

    private Vector3 startPos; // ノーツの生成位置
    private Vector3 endPos; // ノーツの目的地
    private float currentTime = 0f; // ノーツの移動にかかる経過時間

    private bool isTouchingHeart = false; // ハートに触れているかどうかの状態
    public bool IsTouchingHeart => isTouchingHeart; // プロパティを追加

    private NotesManager notesManager; // NotesManagerのインスタンス
    private bool canPlaySpaceSound = true; // スペースキーの音を鳴らせるかどうかのフラグ

    private void Awake()
    {
        startPos = transform.position; // 現在の位置を生成位置として設定
        endPos = new Vector3(endX, startPos.y, startPos.z); // ノーツの目的地を設定（YとZはそのまま）
        notesManager = FindObjectOfType<NotesManager>(); // NotesManagerを取得
    }

    private void Update()
    {
        currentTime += Time.deltaTime; // 経過時間を更新
        // ノーツの位置を移動
        transform.position = Vector3.Lerp(startPos, endPos, currentTime / tempo);

        // ハートに触れた場合のチェック
        if (!isTouchingHeart && transform.position.x >= -1f && transform.position.x <= 1f)
        {
            OnTouchHeart(); // ハートに触れたと見なす
        }

        if (isTouchingHeart)
        {
            if (Input.GetKeyDown(KeyCode.Space) && canPlaySpaceSound)
            {
                notesManager.PlaySpaceSound(); // 音を鳴らす
                canPlaySpaceSound = false; // フラグをオフにして音を鳴らせないようにする
            }
        }

        // ノーツが移動し終わったら削除
        if (currentTime > tempo)
        {
            Destroy(this.gameObject); // ノーツを削除
        }
    }

    public void OnTouchHeart()
    {
        if (!isTouchingHeart)
        {
            isTouchingHeart = true; // ハートに触れた状態
            notesManager.PlayTouchSound(); // ハートに触れたときの音を鳴らす
        }
    }

    private void OnDestroy()
    {
        // ノーツが削除されるとき、フラグをリセット
        canPlaySpaceSound = true;
    }
}