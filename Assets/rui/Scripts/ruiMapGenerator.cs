using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RuiMapGenerator : MonoBehaviour
{
    [SerializeField] TextAsset[] mapText;
    [SerializeField] GameObject[] prefabs;
    float mapSize;
    public int floor = 0;
    Vector2 centerPos;

    public enum MAP_TYPE
    {
        GROUND, // 0 地面
        WALL,   // 1 壁
        PLAYER, // 2 プレイヤー
        ENEMY,  // 3 敵（1体目）
        STAIRS  // 4 階段
    }
    public MAP_TYPE[,] mapTable;
    public MAP_TYPE[,] mapTable2;

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

        _createMap();

        makeAStarMap();
    }

    void Update()
    {

    }

    public void _loadMapData()
    {
        string[] mapLines = mapText[floor].text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);

        int row = mapLines.Length;
        int col = mapLines[0].Split(new char[] { ',' }).Length;
        mapTable = new MAP_TYPE[col, row];
        mapTable2 = new MAP_TYPE[col, row];
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
    public void _createMap()
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
                if (mapTable[x, y] == MAP_TYPE.ENEMY)
                {
                    _map.GetComponent<RuiAttackedEnemy>().enemyCurrentPos = pos;
                }


                _ground.transform.position = ScreenPos(pos);
                _map.transform.position = ScreenPos(pos);

                if (mapTable[x, y] == MAP_TYPE.PLAYER)
                {
                    _map.GetComponent<RuiPlayerManager>().playerCurrentPos = pos;

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
    public void UpdateTilie(Vector2Int _pos, MAP_TYPE mapType)
    {
        mapTable[_pos.x, _pos.y] = mapType;
    }
    public void UpdatePlayerTile(Vector2Int _pos, MAP_TYPE mapType)
    {
        mapTable2[_pos.x, _pos.y] = mapType;
    }

    private void OnGUI()
    {
        string map = "";
        for (int x = 0; x < mapTable.GetLength(1); x++)
        {

            for (int y = 0; y < mapTable.GetLength(0); y++)
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

    class Node
    {
        public bool floor; // 行けないところはfalse
        public int cost;
        public int estimatedCost;
        public int score;
    };
    Node[,] aStarMap;

    public void makeAStarMap()
    {
        int xSize = mapTable.GetLength(1);
        int ySize = mapTable.GetLength(0);
        aStarMap = new Node[ySize, xSize];
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                aStarMap[y, x].floor = (mapTable[y, x] != MAP_TYPE.WALL);
            }
        }
    }

    ///<summary>
    ///AStarで経路を探索して、次に行くべきマスの場所を返す
    /// </summary>
    /// <param name="enemy">移動前の場所（マス）</param>
    /// <param name="player">目的の場所（マス）</param>
    /// <returns>次に行くべき場所（マス）</returns>
    public Vector2Int SearchRoute(Vector2Int enemy, Vector2Int player)
    {
        int xSize = aStarMap.GetLength(1);
        int ySize = aStarMap.GetLength(0);
        // A*のマップを全てクリアする
        const int Max = 100000000; // とてつもなく大きな値
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                aStarMap[y, x].cost = Max;
                aStarMap[y, x].estimatedCost = 0;
                aStarMap[y, x].score = Max;
            }
        }
        //ここで、A*の探索をする
        //目的地から、自分への探索を下ほうが楽なので、
        //目的地に0をいれておく
        aStarMap[player.y, player.x].cost = 0;
        aStarMap[player.y, player.x].estimatedCost = Mathf.Abs(player.y - enemy.y) + Mathf.Abs(player.x - enemy.x);
        aStarMap[player.y, player.x].score = aStarMap[player.y, player.x].cost + aStarMap[player.y, player.x].estimatedCost;
        int minScore = aStarMap[player.y, player.x].score; // 最小スコアを保存しておく

        // ここから経路探索を始める
        bool loop = true;
        while (loop) // 見つかるまでループする
        {
            int nextMinScore = Max;
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    if (aStarMap[y, x].score == minScore) // この場所から、４方向調べる
                    {
                        int[,] dir = {{1, 0},{0, 1},{-1, 0},{0, -1}};
                        for (int d = 0; d < 4; d++)
                        {
                            int nextX = x + dir[d, 0];
                            int nextY = y + dir[d, 1];
                            //ToDo
                            //nextX,nextYが、enemyと同じであれば、経路探索が終わったので、
                            if (nextX == enemy.x && nextY == enemy.y)
                            {
                                loop = false;
                                return new Vector2Int(x, y);
                            }
                            
                            //ToDo:
                            // aStarMap[nextY,nextX]が壁でなくて、scoreがminScoreよりも大きいのであれば、
                            // cost、estimatedCost、scoreを計算して書き込む
                            if (aStarMap[nextY, nextX].floor = false && aStarMap[nextY,nextX].score >= minScore)
                            {
                                aStarMap[nextY, nextX].cost++;
                                //aStarMap[nextY, nextX].estimatedCost
                            }
                            if (nextMinScore > aStarMap[nextY, nextX].score) // 次の最小値を求めておく
                            {
                                nextMinScore = aStarMap[nextY, nextX].score;
                            }
                        }
                    }
                }
            }
            minScore = nextMinScore;
        }
        return enemy;
    }
}
