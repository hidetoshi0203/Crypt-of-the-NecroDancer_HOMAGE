using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // コンボ数表示用

public class ComboManager : MonoBehaviour
{
    public Text comboText; // コンボ数を表示するUIテキスト
    private int comboCount = 0; // 現在のコンボ数
    private NotesManager notesManager; // NotesManagerの参照

    private void Start()
    {
        notesManager = FindObjectOfType<NotesManager>(); // NotesManagerを取得
        UpdateComboText(); // コンボ数表示を初期化
    }

    private void Update()
    {/*
        if (notesManager.CanInputKey() && Input.GetKeyDown(KeyCode.Space))
        {
            IncreaseCombo(); // ハートに触れているときにスペースキーが押された場合コンボ数を増加
        }
        else
        {
            ResetCombo();
        }
        /*
                // ハートに触れていない、または入力に失敗した場合
                if (!notesManager.isCombo)
                {
                   ResetCombo(); // コンボ数をリセット
                }
        */
    }

    public void IncreaseCombo()
    {
        comboCount++;
        UpdateComboText(); // コンボ数を更新
    }
    
    public void ResetCombo()
    {
        comboCount = 0;
        UpdateComboText(); // コンボ数をリセットして更新
    }
    

    private void UpdateComboText()
    {
        comboText.text = comboCount.ToString();
    }
}
