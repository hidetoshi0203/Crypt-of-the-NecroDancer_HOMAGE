using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesController : MonoBehaviour
{
    [SerializeField] private float endX = 0.5f; //ノーツの移動先のX座標
    [SerializeField] private float tempo = 2.0f; //ノーツの移動時間
    [SerializeField] private float Heart_range = 0; //ハートに触れる範囲

    private Vector3 startPos; //ノーツの生成位置
    private Vector3 endPos; //ノーツの終了位置
    private float currentTime = 0f; //ノーツの移動の経過時間

    private NotesManager notesManager; //NotesManagerのインスタンス

    private void Awake()
    {
        startPos = transform.position; //現在の位置を生成位置として設定
        endPos = new Vector3(endX, startPos.y, startPos.z); //ノーツの目的地を設定
        notesManager = FindObjectOfType<NotesManager>(); //NotesManagerを取得
    }

    private void Update()
    {
        currentTime += Time.deltaTime; //経過時間を更新

        transform.position = Vector3.Lerp(startPos, endPos, currentTime / tempo); //ノーツの位置を移動


        if (!notesManager.CanInputKey() && transform.position.x >= -Heart_range && transform.position.x <= Heart_range) //ハートに触れた場合のチェック
        {
            notesManager.OnTouchHeart();
            notesManager.canMove = true; //ハートに触れた後にスペースキーで音を鳴らせるようにフラグをリセット
        }

        if (notesManager.CanInputKey() && Input.GetKeyDown(KeyCode.Space) && notesManager.canMove) //ハートに触れている状態でスペースキーが押されたとき
        {
            notesManager.StopTouchSound();
            notesManager.PlaySpaceSound(); //スペースキーを押したときの音を鳴らす
            notesManager.canMove = false; //フラグをオフにして音を鳴らせないようにする
        }


        if (currentTime > tempo) //ノーツが移動し終わったら削除
        {
            notesManager.OnTimeLimit();
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        notesManager.canMove = true; //ノーツが削除されるときフラグをリセット
    }
}