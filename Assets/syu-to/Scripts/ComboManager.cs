using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // コンボ数表示用

public class ComboManager : MonoBehaviour
{
    public Text comboText; // コンボ数を表示するUIテキスト
    private float comboCount = 0; // 現在のコンボ数
    private NotesManager notesManager; // NotesManagerの参照

    private void Start()
    {
        notesManager = FindObjectOfType<NotesManager>(); // NotesManagerを取得
        UpdateComboText(); // コンボ数表示を初期化
    }

    private void Update()
    {

    }

    public void IncreaseCombo()
    {
        comboCount++;
        comboCount = comboCount - (float)0.5;
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
