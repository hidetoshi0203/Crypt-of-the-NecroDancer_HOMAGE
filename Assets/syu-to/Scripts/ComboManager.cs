using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // �R���{���\���p

public class ComboManager : MonoBehaviour
{
    public Text comboText; // �R���{����\������UI�e�L�X�g
    private float comboCount = 0; // ���݂̃R���{��

    public bool comboreset = false;
    private NotesManager notesManager; // NotesManager�̎Q��
    private TempoManager tempoManager; // TempoManager�̎Q��

    [SerializeField] float ChangedCombos = 10;
    [SerializeField] int ChangeBPM = 30;

    private void Start()
    {
        notesManager = FindObjectOfType<NotesManager>(); // NotesManager���擾
        tempoManager = FindObjectOfType<TempoManager>(); // TempoManager���擾

        UpdateComboText(); // �R���{���\����������
    }

    public void IncreaseCombo()
    {
        comboCount += (float)0.5;
        UpdateComboText(); // �R���{�����X�V
        //Debug.Log("combo");

        // �R���{��ChangedCombos�̔{���ɂȂ�����BPM�𑝉�
        if (comboCount % ChangedCombos == 0)
        {
            tempoManager.BPM += ChangeBPM; // BPM���X�V
            //Debug.Log("BPM Updated ");
        }
    }
    
    public void ResetCombo()
    {
        comboCount = 0;
        UpdateComboText(); // �R���{�������Z�b�g���čX�V

        tempoManager.ResetBPM(); // BPM�������l�ɖ߂�
        //Debug.Log("BPMReset");

        comboreset = true;
    }
    

    private void UpdateComboText()
    {
        comboText.text = comboCount.ToString();
    }
}