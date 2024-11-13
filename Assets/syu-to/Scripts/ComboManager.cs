using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // �R���{���\���p

public class ComboManager : MonoBehaviour
{
    public Text comboText; // �R���{����\������UI�e�L�X�g
    private float comboCount = 0; // ���݂̃R���{��
    private NotesManager notesManager; // NotesManager�̎Q��

    private void Start()
    {
        notesManager = FindObjectOfType<NotesManager>(); // NotesManager���擾
        UpdateComboText(); // �R���{���\����������
    }

    private void Update()
    {

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
    }
    

    private void UpdateComboText()
    {
        comboText.text = comboCount.ToString();
    }
}
