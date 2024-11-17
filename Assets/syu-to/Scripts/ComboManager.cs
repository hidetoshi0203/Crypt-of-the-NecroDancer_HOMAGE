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

    private void Start()
    {
        notesManager = FindObjectOfType<NotesManager>(); // NotesManager���擾

        UpdateComboText(); // �R���{���\����������
    }

    public void IncreaseCombo()
    {
        comboCount++;
        comboCount = comboCount - (float)0.5;
        UpdateComboText(); // �R���{�����X�V
    }
    
    public void ResetCombo()
    {
        comboCount = 0;
        UpdateComboText(); // �R���{�������Z�b�g���čX�V

        comboreset = true;
    }
    

    private void UpdateComboText()
    {
        comboText.text = comboCount.ToString();
    }
}