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

    void Start()
    {
        mapCreate = transform.parent.GetComponent<MapCreate>();
    }

    void Update()
    {

    }

    public void MoveMent()
    {
        playerNextPos = playerCurrentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);
        if (mapCreate.GetNextMapType(playerNextPos) != MapCreate.MAP_TYPE.WALL)
        {
            transform.localPosition = mapCreate.screenPos(playerNextPos);
            playerCurrentPos = playerNextPos;
        }
    }
}