using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private NoteSpawn noteSpawn; // NoteSpawnのインスタンスを参照
    private Note note;

    void Start()
    {
        note = FindObjectOfType<Note>(); //NoteSpawnを探して参照を取得
        noteSpawn = FindObjectOfType<NoteSpawn>(); //NoteSpawnを探して参照を取得
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
