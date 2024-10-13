using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private NoteSpawn noteSpawn; // NoteSpawn�̃C���X�^���X���Q��
    private Note note;

    void Start()
    {
        note = FindObjectOfType<Note>(); //NoteSpawn��T���ĎQ�Ƃ��擾
        noteSpawn = FindObjectOfType<NoteSpawn>(); //NoteSpawn��T���ĎQ�Ƃ��擾
    }

    void Update()
    {
        if(note.isTouchingHeart && Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A");
            noteSpawn.PlaySound();
        }

        if (note.isTouchingHeart && Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("D");
            noteSpawn.PlaySound();
        }

        if (note.isTouchingHeart && Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W");
            noteSpawn.PlaySound();
        }

        if (note.isTouchingHeart && Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S");
            noteSpawn.PlaySound();
        }
    }
}
