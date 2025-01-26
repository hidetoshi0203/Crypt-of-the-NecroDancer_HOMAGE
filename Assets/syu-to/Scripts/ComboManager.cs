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
    [SerializeField] int ChangeBPMCount = 3; //BPM�̕ύX��

    private int bpmChangeCount = 0; // BPM��ύX������


    private void Start()
    {
        notesManager = FindObjectOfType<NotesManager>(); // NotesManager���擾
        tempoManager = FindObjectOfType<TempoManager>(); // TempoManager���擾

        UpdateComboText(); // �R���{���\����������
    }

    public void IncreaseCombo()
    {
        comboCount += 1f;
        UpdateComboText(); // �R���{�����X�V

        // �R���{��ChangedCombos�̔{���ɂȂ�����BPM�𑝉�
        if (comboCount % ChangedCombos == 0 && bpmChangeCount < ChangeBPMCount)
        {
            tempoManager.BPM += ChangeBPM; // BPM���X�V
            bpmChangeCount++; // BPM�ύX�񐔂𑝉�

        }
    }
    
    public void ResetCombo()
    {
        comboCount = 0;
        UpdateComboText(); // �R���{�������Z�b�g���čX�V

        tempoManager.ResetBPM(); // BPM�������l�ɖ߂�
        bpmChangeCount = 0; // BPM�ύX�񐔂����Z�b�g

        comboreset = true;
    }
    

    private void UpdateComboText()
    {
        comboText.text = comboCount.ToString();
    }
}