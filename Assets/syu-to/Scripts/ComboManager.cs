using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // コンボ数表示用

public class ComboManager : MonoBehaviour
{
    public Text comboText; // コンボ数を表示するUIテキスト
    private float comboCount = 0; // 現在のコンボ数

    public bool comboreset = false;
    private NotesManager notesManager; // NotesManagerの参照
    private TempoManager tempoManager; // TempoManagerの参照

    [SerializeField] float ChangedCombos = 10;
    [SerializeField] int ChangeBPM = 30;

    private void Start()
    {
        notesManager = FindObjectOfType<NotesManager>(); // NotesManagerを取得
        tempoManager = FindObjectOfType<TempoManager>(); // TempoManagerを取得

        UpdateComboText(); // コンボ数表示を初期化
    }

    public void IncreaseCombo()
    {
        comboCount += (float)0.5;
        UpdateComboText(); // コンボ数を更新
        //Debug.Log("combo");

        // コンボがChangedCombosの倍数になったらBPMを増加
        if (comboCount % ChangedCombos == 0)
        {
            tempoManager.BPM += ChangeBPM; // BPMを更新
            //Debug.Log("BPM Updated ");
        }
    }
    
    public void ResetCombo()
    {
        comboCount = 0;
        UpdateComboText(); // コンボ数をリセットして更新

        tempoManager.ResetBPM(); // BPMを初期値に戻す
        //Debug.Log("BPMReset");

        comboreset = true;
    }
    

    private void UpdateComboText()
    {
        comboText.text = comboCount.ToString();
    }
}