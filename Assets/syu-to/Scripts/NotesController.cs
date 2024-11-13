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
    private ComboManager comboManager;

    private void Awake()
    {
        startPos = transform.position; //現在の位置を生成位置として設定
        endPos = new Vector3(endX, startPos.y, startPos.z); //ノーツの目的地を設定
        notesManager = FindObjectOfType<NotesManager>(); //NotesManagerを取得
        comboManager = FindObjectOfType<ComboManager>();
    }

    private void Update()
    {
        currentTime += Time.deltaTime; //経過時間を更新

        transform.position = Vector3.Lerp(startPos, endPos, currentTime / tempo); //ノーツの位置を移動

        //ハートに触れた場合のチェック
        if (!notesManager.CanInputKey() && transform.position.x >= -Heart_range && transform.position.x <= Heart_range)
        {
            notesManager.OnTouchHeart();
            notesManager.playerCanMove = true; //ハートに触れた後に音を鳴らせるようにフラグをリセット

        }

        //ハートに触れている状態でキーが押されたとき
        if (notesManager.CanInputKey() && Input.GetKeyDown(KeyCode.Space) && notesManager.playerCanMove)
        {
            notesManager.StopTouchSound();
            notesManager.PlaySpaceSound(); //スペースキーを押したときの音を鳴らす
            notesManager.playerCanMove = false; //フラグをオフにして音を鳴らせないようにする

            notesManager.OnTimeLimit();
            comboManager.IncreaseCombo();

            Destroy(this.gameObject);
        }

        //ノーツが移動し終わったら削除
        if (currentTime > tempo)
        {
            notesManager.OnTimeLimit();
            comboManager.ResetCombo();
            Destroy(this.gameObject);

        }
    }

    private void OnDestroy()
    {
        notesManager.playerCanMove = true; //ノーツが削除されるときフラグをリセット
    }
}