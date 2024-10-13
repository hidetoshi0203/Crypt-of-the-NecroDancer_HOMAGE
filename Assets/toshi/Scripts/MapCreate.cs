using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MapCreate : MonoBehaviour
{
    [SerializeField] TextAsset mapText;
    [SerializeField] GameObject[] objPrefabs;
    private string[] mapLines;
    float mapSize;
    Vector2 centerPos; // 中心座標変数
    public enum MAP_TYPE
    {
        FLOOR, //0
        WALL,  //1
        PLAYER, //2
        ENEMY//3
    }

    public MAP_TYPE GetNextMapType(Vector2Int _pos)
    {
        return mapTable[_pos.x, _pos.y];
    }
    private void Start()
    {
        loadMapData();

        //マップの生成関数
        createMap();
    }
    MAP_TYPE[,] mapTable;

    void loadMapData()
    {
        mapLines = mapText.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        //MAP_TYPEの配列初期化のための値を取得

        //行の数
        int row = mapLines.Length;
        //列の数
        int column = mapLines[0].Split(new char[] { ',' }).Length;
        //初期化
        mapTable = new MAP_TYPE[column, row];
        //行の数だけループ
        for (int y = 0; y < row; y++)
        {
            //1行をカンマ区切りで分割
            string[] mapValues = mapLines[y].Split(new char[] { ',' });
            //列の数だけループ
            for (int x = 0; x < column; x++)
            {
                //mapValuesのx番目をMAP＿TYPEにきゃすとしてmapTable[x,y]番目に代入
                mapTable[x, y] = (MAP_TYPE)int.Parse(mapValues[x]);
            }
        }
        
    }

    void createMap()
    {
        //サイズを取得する
        mapSize = objPrefabs[0].GetComponent<SpriteRenderer>().bounds.size.x;

        //中心座標を取得
        //縦横の数を半分にしてmapSizeを掛けることで中心を求める

        //列が偶数の場合
        if (mapTable.GetLength(0) % 2 == 0)
        {
            centerPos.x = mapTable.GetLength(0) / 2* mapSize - (mapSize / 2);   
        }
        else
        {
            centerPos.x = mapTable.GetLength(0) / 2* mapSize;
        }
        //行偶数の場合
        if (mapTable.GetLength(1) % 2 == 0)
        {
            centerPos.y = mapTable.GetLength(1) / 2* mapSize - (mapSize / 2);
        }
        else
        {
            centerPos.y = mapTable.GetLength(1) / 2* mapSize;
        }
        //mapTableの行のループ
        for (int y = 0;y < mapTable.GetLength(1);y++) 
        {
            //mapTableの列のループ
            for (int x = 0;x < mapTable.GetLength(0); x++)
            {
                //現在のポジション
                Vector2Int pos = new Vector2Int(x, y);
                // floorを敷き詰める
                GameObject floor = Instantiate(objPrefabs[(int)MAP_TYPE.FLOOR], transform);
                floor.transform.position = screenPos(pos);
                //objPrefabの中のmapTable[x,y]にあたるものを生成
                GameObject map = Instantiate(objPrefabs[(int)mapTable[x,y]],transform);
                //生成したゲームオブジェクトの位置を設定
                map.transform.position = screenPos(pos); 

                //Playerスクリプトのcurrntposにposを代入
                if (mapTable[x,y] == MAP_TYPE.PLAYER)
                {
                    map.GetComponent<RuiPlayerManager>().currentPos = pos;
                }
            }
        }
    }

    public Vector2 screenPos(Vector2Int _pos)
    {
        return new Vector2(
            _pos.x * mapSize - centerPos.x,
            //yをマイナスにして上下反転を直す
            -(_pos.y * mapSize - centerPos.y));
    }
}
