using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // �R���{���\���p

public class ComboManager : MonoBehaviour
{
    public Text comboText; // �R���{����\������UI�e�L�X�g
    private int comboCount = 0; // ���݂̃R���{��
    private NotesManager notesManager; // NotesManager�̎Q��

    private void Start()
    {
        notesManager = FindObjectOfType<NotesManager>(); // NotesManager���擾
        UpdateComboText(); // �R���{���\����������
    }

    private void Update()
    {/*
        if (notesManager.CanInputKey() && Input.GetKeyDown(KeyCode.Space))
        {
            IncreaseCombo(); // �n�[�g�ɐG��Ă���Ƃ��ɃX�y�[�X�L�[�������ꂽ�ꍇ�R���{���𑝉�
        }
        else
        {
            ResetCombo();
        }
        /*
                // �n�[�g�ɐG��Ă��Ȃ��A�܂��͓��͂Ɏ��s�����ꍇ
                if (!notesManager.isCombo)
                {
                   ResetCombo(); // �R���{�������Z�b�g
                }
        */
    }

    public void IncreaseCombo()
    {
        comboCount++;
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
