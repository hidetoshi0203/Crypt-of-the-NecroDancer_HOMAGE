using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;

public class RuiPlayerManager : MonoBehaviour
{
    public enum DIRECTION
    {
        TOP,
        RIGHT,
        DOWN,
        LEFT
    }

    public DIRECTION direction;
    public Vector2Int currentPos, nextPos;

    int[,] move =
    {
        { 0, -1 },
        { 1, 0 },
        { 0, 1 },
        { -1, 1 }
    };

    MapCreate mapCreate;

    void Start()
    {
        mapCreate = transform.parent.GetComponent<MapCreate>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = DIRECTION.TOP;
            PlayerMove();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            direction = DIRECTION.RIGHT;
            PlayerMove();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            direction = DIRECTION.DOWN;
            PlayerMove();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = DIRECTION.LEFT;
            PlayerMove();
        }
    }

    void PlayerMove()
    {
        nextPos = currentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);
        if (mapCreate.GetNextMapType(nextPos) != MapCreate.MAP_TYPE.WALL)
        {
            transform.localPosition = mapCreate.screenPos(nextPos);
            currentPos = nextPos;
        }
    }
}
