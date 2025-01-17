using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesController : MonoBehaviour
{
    //[SerializeField] private float endX;


    ////private float endX; //ノーツの移動先のX座標
    //[SerializeField] private float tempo = 2.0f; //ノーツの移動時間
    //[SerializeField] public float Heart_range = 0; //ハートに触れる範囲
    ////private GameObject heartImage; // ノーツの終着点となるオブジェクト 

    //private Vector3 startPos; //ノーツの生成位置
    //private Vector3 endPos; //ノーツの終了位置
    //private float currentTime = 0f; //ノーツの移動の経過時間

    //private NotesManager notesManager; //NotesManagerのインスタンス
    //private ComboManager comboManager;
    //private ColorChange ColorChange;
    //private toshiPlayer toshiPlayer = null;
    ////private Image a;
    //private void Awake()
    //{
    //    /*heartImage = GameObject.Find("Heart");
    //    endX = heartImage.transform.position.x;
    //    //Heart_range = heartImage.transform.position.x + Heart_range;
    //    */
    //    startPos = transform.position; //現在の位置を生成位置として設定
    //    endPos = new Vector3(endX, startPos.y, startPos.z); //ノーツの目的地を設定
    //    notesManager = FindObjectOfType<NotesManager>(); //NotesManagerを取得
    //    comboManager = FindObjectOfType<ComboManager>();
    //    ColorChange = FindObjectOfType<ColorChange>();
    //}

    //private void Update()
    //{
    //    //Debug.Log(Heart_range);
    //    if (toshiPlayer == null)
    //    {
    //        GameObject inst = GameObject.FindGameObjectWithTag("Player");
    //        toshiPlayer = inst.GetComponent<toshiPlayer>();
    //    }
    //    currentTime += Time.deltaTime; //経過時間を更新

    //    transform.position = Vector3.Lerp(startPos, endPos, currentTime / tempo); //ノーツの位置を移動

    //    //ハートに触れた場合のチェック
    //    if (!notesManager.CanInputKey() && transform.position.x >= -Heart_range && transform.position.x <= Heart_range)
    //    {
    //        notesManager.OnTouchHeart();
    //        notesManager.playerCanMove = true; //ハートに触れた後に音を鳴らせるようにフラグをリセット

    //    }

    //    //ハートに触れている状態でキーが押されたとき
    //    if (notesManager.CanInputKey() && notesManager.playerCanMove && Input.GetKeyDown(KeyCode.Space))
    //    {
    //        KyeDown();
    //        //ColorChange.Changecolor();
    //    }

    //    if (notesManager.CanInputKey() && notesManager.playerCanMove && Input.GetKeyDown(KeyCode.W))
    //    {
    //        KyeDown();
    //        //ColorChange.Changecolor();
    //    }
    //    if (notesManager.CanInputKey() && notesManager.playerCanMove && Input.GetKeyDown(KeyCode.A))
    //    {
    //        KyeDown();
    //        //ColorChange.Changecolor();
    //    }
    //    if (notesManager.CanInputKey() && notesManager.playerCanMove && Input.GetKeyDown(KeyCode.S))
    //    {
    //        KyeDown();
    //        //ColorChange.Changecolor();
    //    }
    //    if (notesManager.CanInputKey() && notesManager.playerCanMove && Input.GetKeyDown(KeyCode.D))
    //    {
    //        KyeDown();
    //        //ColorChange.Changecolor();
    //    }

    //    //ノーツが移動し終わったら削除
    //    if (currentTime > tempo)
    //    {
    //        notesManager.OnTimeLimit();
    //        comboManager.ResetCombo();
    //        currentTime = 0f;
    //        Destroy(this.gameObject);

    //    }
    //}

    //private void OnDestroy()
    //{
    //    notesManager.playerCanMove = true; //ノーツが削除されるときフラグをリセット
    //}

    //public void KyeDown()
    //{
    //    notesManager.StopTouchSound();
    //    notesManager.PlaySpaceSound(); //スペースキーを押したときの音を鳴らす
    //    notesManager.playerCanMove = false; //フラグをオフにして音を鳴らせないようにする

    //    notesManager.OnTimeLimit();
    //    comboManager.IncreaseCombo();

    //    Destroy(this.gameObject);
    //}

    //public void OffTouchHeart()
    //{
    //    //ハートの外にいるとき
    //    if (transform.position.x < -Heart_range || transform.position.x > Heart_range)
    //    {
    //        comboManager.comboreset = true;
    //    }
    //    else
    //    {
    //        comboManager.comboreset = false;
    //    }

    //    if (comboManager.comboreset && Input.GetKeyDown(KeyCode.Space))
    //    {
    //        comboManager.ResetCombo();
    //        //Debug.Log("reset");
    //    }



    //    if (comboManager.comboreset && Input.GetKeyDown(KeyCode.W))
    //    {
    //        comboManager.ResetCombo();
    //        //Debug.Log("W_reset");
    //    }

    //    if (comboManager.comboreset && Input.GetKeyDown(KeyCode.A))
    //    {
    //        comboManager.ResetCombo();
    //        //Debug.Log("A_reset");
    //    }
    //    if (comboManager.comboreset && Input.GetKeyDown(KeyCode.S))
    //    {
    //        comboManager.ResetCombo();
    //        //Debug.Log("S_reset");
    //    }
    //    if (comboManager.comboreset && Input.GetKeyDown(KeyCode.D))
    //    {
    //        comboManager.ResetCombo();
    //        //Debug.Log("D_reset");
    //    }
    //}
}