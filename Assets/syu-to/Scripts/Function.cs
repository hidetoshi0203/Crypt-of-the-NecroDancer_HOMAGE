using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Function : MonoBehaviour
{

    private NoteSpawn noteSpawn; // NoteSpawn�̃C���X�^���X���Q��
    private Note�@note;
    public bool isTouchingHeart; // �n�[�g�ɐڐG���Ă��邩�ǂ���

    void Start()
    {
        noteSpawn = FindObjectOfType<NoteSpawn>(); //NoteSpawn��T���ĎQ�Ƃ��擾
        note = FindObjectOfType<Note>();
    }

}
