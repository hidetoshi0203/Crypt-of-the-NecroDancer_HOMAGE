using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public enum DIRECTION
    {
        TOP,
        RIGHT,
        DOWN,
        LEFT
    }

    public DIRECTION direction;
    public Vector2Int playerCurrentPos, playerNextPos;

    int[,] move =
    {
        { 0, -1 },
        { 1, 0 },
        { 0, 1 },
        { -1, 0 }
    };

    MapCreate mapCreate;

    NotesController notesController;
    NotesManager notesManager;
    GameObject leftNotes;
    GameObject rightNotes;

    void Start()
    {
        mapCreate = transform.parent.GetComponent<MapCreate>();
        notesManager = GetComponent<NotesManager>();
        leftNotes = GameObject.Find("notesManager.leftNoteObject");
        rightNotes = GameObject.Find("notesManager.rightNoteObject");
        notesController = leftNotes.GetComponent<NotesController>();//FindObjectOfType<Function>();
        notesController = rightNotes.GetComponent<NotesController>();
    }
    
    void Update()
    {

    }

    public void MoveMent()
    {
        if (notesController.IsTouchingHeart)
        {
            playerNextPos = playerCurrentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);
            if (mapCreate.GetNextMapType(playerNextPos) != MapCreate.MAP_TYPE.WALL)
            {
                transform.localPosition = mapCreate.screenPos(playerNextPos);
                playerCurrentPos = playerNextPos;
            }
        }
    }
}
