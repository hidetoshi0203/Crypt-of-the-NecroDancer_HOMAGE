using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    public Text comboText;               //�R���{����\������UI�e�L�X�g
    private float comboCount = 0;        //���݂̃R���{��
    private float recovercomboCount = 0; //�R���{�ŉ񕜂��邽�߂̃R���{��

    public bool comboreset = false;
    private TempoManager tempoManager;  //empoManager�̎Q��
    private toshiPlayer toshiplayer;

    [SerializeField] float ChangedCombos = 10;
    [SerializeField] int ChangeBPM = 30;
    [SerializeField] int ChangeBPMCount = 3; //BPM�̕ύX��
    [SerializeField] float recoverCount = 20;

    private int bpmChangeCount = 0; //BPM��ύX������


    private void Start()
    {
        tempoManager = FindObjectOfType<TempoManager>(); //TempoManager���擾
        toshiplayer = FindObjectOfType<toshiPlayer>();  //TempoManager���擾

        UpdateComboText(); //�R���{���\����������
    }

    public void IncreaseCombo()
    {
        comboCount += 1f;
        recovercomboCount += 1f;
        UpdateComboText(); //�R���{�����X�V

        Recovercombo();

        //�R���{��ChangedCombos�̔{���ɂȂ�����BPM�𑝉�
        if (comboCount % ChangedCombos == 0 && bpmChangeCount < ChangeBPMCount)
        {
            tempoManager.BPM += ChangeBPM;  //BPM���X�V
            bpmChangeCount++;               //BPM�ύX�񐔂𑝉�

        }
    }
    
    public void ResetCombo()
    {
        comboCount = 0;
        recovercomboCount = 0;
        UpdateComboText(); //�R���{�������Z�b�g���čX�V

        tempoManager.ResetBPM(); //BPM�������l�ɖ߂�
        bpmChangeCount = 0;      //BPM�ύX�񐔂����Z�b�g

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