using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class rMapGenerator : MonoBehaviour
{
    [SerializeField] TextAsset mapText;
    [SerializeField] GameObject[] prefabs;
    float mapSize;
    Vector2 centerPos;

    public enum MAP_TYPE
    {
        GROUND, //0
        WALL,   //1
        PLAYER, //2
        ENEMY   //3
    }
    public MAP_TYPE[,] mapTable;

    public MAP_TYPE GetPlayerNextMapType(Vector2Int _pos)
    {
        return mapTable[_pos.x, _pos.y];
    }

    public MAP_TYPE GetEnemyNextMapType(Vector2Int _pos)
    {
        return mapTable[_pos.x, _pos.y];
    }

    void Start()
    {
        _loadMapData();

        //追加　マップ生成関数を呼び出す
        _createMap();

        

    }

    void _loadMapData()
    {
        string[] mapLines = mapText.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);


        int row = mapLines.Length;
        int col = mapLines[0].Split(new char[] { ',' }).Length;
        mapTable = new MAP_TYPE[col, row];

        //追加　行の数だけループ
        for (int y = 0; y < row; y++)
        {
            //1行をカンマ区切りで分割
            string[] mapValues = mapLines[y].Split(new char[] { ',' });
            //列の数だけループ
            for (int x = 0; x < col; x++)
            {
                //mapValuesのx番目をMAP_TYPEにキャストしてmapTable[x,y]番目に代入
                mapTable[x, y] = (MAP_TYPE)int.Parse(mapValues[x]);
            }
        }

    }
    void _createMap()
    {
        mapSize = prefabs[1].GetComponent<SpriteRenderer>().bounds.size.x;



        if (mapTable.GetLength(0) % 2 == 0)
        {
            centerPos.x = mapTable.GetLength(0) / 2 * mapSize - (mapSize / 2);
        }
        else
        {
            centerPos.x = mapTable.GetLength(0) / 2 * mapSize;
        }

        if (mapTable.GetLength(1) % 2 == 0)
        {
            centerPos.y = mapTable.GetLength(1) / 2 * mapSize - (mapSize / 2);
        }
        else
        {
            centerPos.y = mapTable.GetLength(1) / 2 * mapSize;
        }


        for (int y = 0; y < mapTable.GetLength(1); y++)
        {
            for (int x = 0; x < mapTable.GetLength(0); x++)
            {
                Vector2Int pos = new Vector2Int(x, y);

                GameObject _ground = Instantiate(prefabs[(int)MAP_TYPE.GROUND], transform);
                GameObject _map = Instantiate(prefabs[(int)mapTable[x, y]], transform);
                if (mapTable[x,y] == MAP_TYPE.ENEMY)
                {
                    _map.GetComponent<rtoshiEnemy>().eCurrentPos = pos;
                }


                _ground.transform.position = ScreenPos(pos);
                _map.transform.position = ScreenPos(pos);

                if (mapTable[x, y] == MAP_TYPE.PLAYER)
                {
                    _map.GetComponent<rtoshiPlayer>().playerCurrentPos = pos;

                }
            }
        }
    }
    public Vector2 ScreenPos(Vector2Int _pos)
    {
        return new Vector2(
            _pos.x * mapSize - centerPos.x,
            -(_pos.y * mapSize - centerPos.y));

    }

   public void UpdateTilie(Vector2Int _pos,MAP_TYPE mapType)
    {
        mapTable[_pos.x, _pos.y] = mapType;
    }

    private void OnGUI()
    {
        string map = "";
        for(int x = 0; x < mapTable.GetLength(1); x++)
        {
            
            for(int y = 0; y < mapTable.GetLength(0); y++)
            {
                //if (isReach[y, x] == false)
                //{
                //    map += "？";
                //    continue;
                //}
                if (mapTable[y, x] == MAP_TYPE.ENEMY) map += "◆";
                else if (mapTable[y, x] == MAP_TYPE.WALL) map += "■";
                else if (mapTable[y, x] == MAP_TYPE.PLAYER) map += "●";
                else if (mapTable[y, x] == MAP_TYPE.GROUND) map += "□";
            }
            map += "\n";
        }
        GUI.Label(new Rect(50, 50, 300, 300), map);
    }
}
