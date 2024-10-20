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

    private bool isTouchingHeart = false; //ハートに触れているかどうか
    public bool IsTouchingHeart => isTouchingHeart;

    private NotesManager notesManager; //NotesManagerのインスタンス
    private bool canPlaySpaceSound = true; //スペースキーの音を鳴らせるかどうか
    private bool isTouchSoundPlaying = false; //ハートに触れた音が再生中かどうか

    private void Awake()
    {
        startPos = transform.position; //現在の位置を生成位置として設定
        endPos = new Vector3(endX, startPos.y, startPos.z); //ノーツの目的地を設定
        notesManager = FindObjectOfType<NotesManager>(); //NotesManagerを取得
    }

    private void Update()
    {
        currentTime += Time.deltaTime; //経過時間を更新
        
        transform.position = Vector3.Lerp(startPos, endPos, currentTime / tempo);　//ノーツの位置を移動

        
        if (!isTouchingHeart && transform.position.x >= -Heart_range && transform.position.x <= Heart_range) //ハートに触れた場合のチェック
        {
            OnTouchHeart();
        }

        
        if (isTouchingHeart && Input.GetKeyDown(KeyCode.Space) && canPlaySpaceSound) //ハートに触れている状態でスペースキーが押されたとき
        {
            if (isTouchSoundPlaying)
            {
                notesManager.StopTouchSound(); //ハートに触れた音を停止
            }
            notesManager.PlaySpaceSound(); //スペースキーを押したときの音を鳴らす
            canPlaySpaceSound = false; //フラグをオフにして音を鳴らせないようにする
        }

        
        if (currentTime > tempo) //ノーツが移動し終わったら削除
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTouchHeart()
    {
        if (!isTouchingHeart)
        {
            isTouchingHeart = true; //ハートに触れた状態
            notesManager.PlayTouchSound(); // ハートに触れたときの音を鳴らす
            isTouchSoundPlaying = true; //ハートの音が再生中であることをフラグに設定
            canPlaySpaceSound = true; //ハートに触れた後にスペースキーで音を鳴らせるようにフラグをリセット
        }
    }

    private void OnDestroy()
    {
        canPlaySpaceSound = true; //ノーツが削除されるときフラグをリセット
    }
}