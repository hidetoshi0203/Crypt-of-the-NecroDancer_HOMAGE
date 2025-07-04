using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TempoManager : MonoBehaviour
{
    public const float MINUTES = 60f;
    [SerializeField] private float startDelay = 1.5f;
    public float StartDelay { get { return startDelay; } }
    [SerializeField] public int bpm;
    private int initialBPM; //����BPM��ێ�

    public int BPM
    {
        get { return bpm; }
        set
        {
            bpm = value;
            Tempo = MINUTES / bpm;  //BPM�ύX���Ƀe���|���Čv�Z
            NotifyBPMChanged();     //BPM���ύX���ꂽ���Ƃ�ʒm
        }
    }

    private float tempo = 1f;
    public float Tempo {get{ return tempo; }set{ tempo = value; } }

    private void Awake()
    {
        initialBPM = bpm;      //�����l���L�^
        Tempo = MINUTES / bpm; //�����e���|�v�Z
    }

    // BPM�������l�Ƀ��Z�b�g
    public void ResetBPM()
    {
        BPM = initialBPM; // ����BPM�ɖ߂�
    }

    private void NotifyBPMChanged()
    {
        //NotesManager��BPM�ύX�̒ʒm�𑗂�
        FindObjectOfType<NotesManager>().UpdateGenerateTime(Tempo);

        //BGM��BPM�ύX�̒ʒm�𑗂�
        FindObjectOfType<BGM>()?.UpdatePlaybackSpeed(BPM);
    }
}
