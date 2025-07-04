using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TempoManager : MonoBehaviour
{
    public const float MINUTES = 60f;
    [SerializeField] private float startDelay = 1.5f;
    public float StartDelay { get { return startDelay; } }
    [SerializeField] public int bpm;
    private int initialBPM; //初期BPMを保持

    public int BPM
    {
        get { return bpm; }
        set
        {
            bpm = value;
            Tempo = MINUTES / bpm;  //BPM変更時にテンポを再計算
            NotifyBPMChanged();     //BPMが変更されたことを通知
        }
    }

    private float tempo = 1f;
    public float Tempo {get{ return tempo; }set{ tempo = value; } }

    private void Awake()
    {
        initialBPM = bpm;      //初期値を記録
        Tempo = MINUTES / bpm; //初期テンポ計算
    }

    // BPMを初期値にリセット
    public void ResetBPM()
    {
        BPM = initialBPM; // 初期BPMに戻す
    }

    private void NotifyBPMChanged()
    {
        //NotesManagerにBPM変更の通知を送る
        FindObjectOfType<NotesManager>().UpdateGenerateTime(Tempo);

        //BGMにBPM変更の通知を送る
        FindObjectOfType<BGM>()?.UpdatePlaybackSpeed(BPM);
    }
}
