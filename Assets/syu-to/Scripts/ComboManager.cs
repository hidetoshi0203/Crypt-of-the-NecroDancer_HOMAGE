using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    public Text comboText;               //コンボ数を表示するUIテキスト
    private float comboCount = 0;        //現在のコンボ数
    private float recovercomboCount = 0; //コンボで回復するためのコンボ数

    public bool comboreset = false;
    private TempoManager tempoManager;  //empoManagerの参照
    private toshiPlayer toshiplayer;

    [SerializeField] float ChangedCombos = 10;
    [SerializeField] int ChangeBPM = 30;
    [SerializeField] int ChangeBPMCount = 3; //BPMの変更回数
    [SerializeField] float recoverCount = 20;

    private int bpmChangeCount = 0; //BPMを変更した回数


    private void Start()
    {
        tempoManager = FindObjectOfType<TempoManager>(); //TempoManagerを取得
        toshiplayer = FindObjectOfType<toshiPlayer>();  //TempoManagerを取得

        UpdateComboText(); //コンボ数表示を初期化
    }

    public void IncreaseCombo()
    {
        comboCount += 1f;
        recovercomboCount += 1f;
        UpdateComboText(); //コンボ数を更新

        Recovercombo();

        //コンボがChangedCombosの倍数になったらBPMを増加
        if (comboCount % ChangedCombos == 0 && bpmChangeCount < ChangeBPMCount)
        {
            tempoManager.BPM += ChangeBPM;  //BPMを更新
            bpmChangeCount++;               //BPM変更回数を増加

        }
    }
    
    public void ResetCombo()
    {
        comboCount = 0;
        recovercomboCount = 0;
        UpdateComboText(); //コンボ数をリセットして更新

        tempoManager.ResetBPM(); //BPMを初期値に戻す
        bpmChangeCount = 0;      //BPM変更回数をリセット

        comboreset = true;
    }
    

    private void UpdateComboText()
    {
        comboText.text = comboCount.ToString();
    }

    private void Recovercombo()
    {
        if(recovercomboCount == recoverCount)
        {
            toshiplayer.Heal();

            recovercomboCount = 0;
        }
    }

}