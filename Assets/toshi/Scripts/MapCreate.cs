using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MapCreate : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject playerPrefab;

    private enum mapMode
    {
        floor = 0,
        wall = 1,
        player = 2,
    }

    // ステージ多次元配列
    public int[,] stageArray = new int[10,10]{
        {1,1,1,1,1,1,1,1,1,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,1,1,1,1,1,1,1,1,1}
    };
    public int[,] playerArray = new int[10, 10]
    {
        { 0,0,0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,2,0,0,0,0},
        { 0,0,0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0,0,0},
        { 0,0,0,0,0,0,0,0,0,0}
    };

    private void Start()
    {
        for (int x = 0; x < stageArray.GetLength(0); x++)
        {
            for (int y = 0; y < stageArray.GetLength(1); y++)
            {
                switch (stageArray[x ,y])
                {
                    case 0:
                        Instantiate(floorPrefab);
                        break;
                    case 1:
                        Instantiate(wallPrefab);
                        break;
                }
        }
    }
    void Update()
    {
            int moveX;
            int moveY;
            // Keyによってプレイヤーが相対的に何処に移動するかを指定する
            if (Input.GetKeyDown(KeyCode.A))
            {
                moveX = -1;
                moveY = 0;
                UpdatePlayerPosition(moveX, moveY);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                moveX = 1;
                moveY = 0;
                UpdatePlayerPosition(moveX, moveY);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                moveX = 0;
                moveY = 1;
                UpdatePlayerPosition(moveX, moveY);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                moveX = 0;
                moveY = -1;
                UpdatePlayerPosition(moveX, moveY);
            }
            else
            {
                moveX = 0;
                moveY = 0;
                UpdatePlayerPosition(moveX, moveY);
            }
        }
        void UpdatePlayerPosition(int moveX, int moveY)
        {
            int playerPositionX = 0;
            int playerPositionZ = 0;
            //プレイヤーの現在の一を取得する
            for (int x = 0; x < playerArray.GetLength(0); x++)
            {
                for (int y = 0; y < playerArray.GetLength(1); y++)
                {
                    if (playerArray[x, y] == 2)
                    {
                        playerPositionX = x;
                        playerPositionZ = y;
                    }

                }
            }
            if (stageArray[playerPositionX + moveX, playerPositionZ + moveY] == (int)mapMode.wall)
            {
                Debug.Log("壁");
                return;
            }
            else
            {
                //ここで移動処理書く
            }
        }
    }
}
