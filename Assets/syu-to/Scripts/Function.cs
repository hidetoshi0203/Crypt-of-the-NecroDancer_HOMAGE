using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Function : MonoBehaviour
{

    private NoteSpawn noteSpawn; // NoteSpawnのインスタンスを参照
    private Note　note;
    public bool isTouchingHeart; // ハートに接触しているかどうか

    void Start()
    {
        noteSpawn = FindObjectOfType<NoteSpawn>(); //NoteSpawnを探して参照を取得
        note = FindObjectOfType<Note>();
    }

}
