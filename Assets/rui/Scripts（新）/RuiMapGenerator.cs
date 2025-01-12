using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RuiMapGenerator : MonoBehaviour
{
    [SerializeField] TextAsset[] mapText;
    [SerializeField] GameObject[] prefabs;
    float mapSize;
    public int floor = 0;
    //Vector2 centerPos;

    public enum MAP_TYPE
    {
        GROUND, // 0 �n��
        WALL,   // 1 ��
        PLAYER, // 2 �v���C���[
        ENEMY,  // 3 �G�i1�̖ځj
        STAIRS,  // 4 �K�i
        WALL2, // 5 �� ()
        ENEMY2, // 6 �G2
        HEALINGPOTION, // 8 �񕜃|�[�V����
        STRENGTHPOTION, // 9 �U����UP�|�[�V����
    }
    public MAP_TYPE[,] mapTable;
    public MAP_TYPE[,] mapTable2;

    public MAP_TYPE GetEntityMapType(Vector2Int _pos)
    {
        return mapTable2[_pos.x, _pos.y];
    }

    public MAP_TYPE GetStageMapType(Vector2Int _pos)
    {
        return mapTable[_pos.x, _pos.y];
    }

    public MAP_TYPE GetEnemyNextMapType(Vector2Int _pos)
    {
        return mapTable[_pos.x, _pos.y];
    }

    public MAP_TYPE GetMapType(Vector2Int _pos)
    {
        return mapTable[_pos.x, _pos.y];
    }

    private void Awake()
    {
        _loadMapData();

        _createMap();
    }

    void Start()
    {
        makeAStarMap();
    }

    [SerializeField] Vector2Int enemy = new Vector2Int(0, 0);
    [SerializeField] Vector2Int player = new Vector2Int(5, 4);

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            enemy = SearchRoute(enemy, player);
        }
    }

    public void _loadMapData()
    {
        string[] mapLines = mapText[floor].text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);

        int row = mapLines.Length;
        int col = mapLines[0].Split(new char[] { ',' }).Length;
        mapTable = new MAP_TYPE[col, row];
        mapTable2 = new MAP_TYPE[col, row];
        //�ǉ��@�s�̐��������[�v
        for (int y = 0; y < row; y++)
        {
            //1�s���J���}��؂�ŕ���
            string[] mapValues = mapLines[y].Split(new char[] { ',' });
            //��̐��������[�v
            for (int x = 0; x < col; x++)
            {
                //mapValues��x�Ԗڂ�MAP_TYPE�ɃL���X�g����mapTable[x,y]�Ԗڂɑ��
                mapTable[x, y] = (MAP_TYPE)int.Parse(mapValues[x]);

            }
        }
    }
    public void _createMap()
    {
        mapSize = prefabs[1].GetComponent<SpriteRenderer>().bounds.size.x;

        /*if (mapTable.GetLength(0) % 2 == 0)
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
        }*/


        for (int y = 0; y < mapTable.GetLength(1); y++)
        {
            for (int x = 0; x < mapTable.GetLength(0); x++)
            {
                Vector2Int pos = new Vector2Int(x, y);

                GameObject _ground = Instantiate(prefabs[(int)MAP_TYPE.GROUND], transform);
                GameObject _map = Instantiate(prefabs[(int)mapTable[x, y]], transform);
                if (mapTable[x, y] == MAP_TYPE.ENEMY)
                {
                    _map.GetComponent<RuiEnemyManager>().enemyCurrentPos = pos;
                    mapTable[x, y] = MAP_TYPE.GROUND;
                    mapTable2[x, y] = MAP_TYPE.ENEMY;
                }
                if (mapTable[x, y] == MAP_TYPE.ENEMY2)
                {
                    _map.GetComponent<RuiEnemyManager>().enemyCurrentPos = pos;
                    mapTable[x, y] = MAP_TYPE.GROUND;
                    mapTable2[x, y] = MAP_TYPE.ENEMY;
                }

                _ground.transform.position = ScreenPos(pos);
                _map.transform.position = ScreenPos(pos);

                if (mapTable[x, y] == MAP_TYPE.PLAYER)
                {
                    _map.GetComponent<RuitoshiPlayer>().playerCurrentPos = pos;
                    mapTable[x, y] = MAP_TYPE.GROUND;
                    mapTable2[x, y] = MAP_TYPE.PLAYER;

                }
            }
        }
    }

    public Vector2 ScreenPos(Vector2Int _pos)
    {
        return new Vector2(
            _pos.x * mapSize /*- centerPos.x*/,
            -(_pos.y * mapSize /*- centerPos.y*/));

    }
    public void UpdateMapTile(Vector2Int _pos, MAP_TYPE mapType) // �}�b�v�̏��
    {
        mapTable[_pos.x, _pos.y] = mapType;
    }
    public void UpdateTile(Vector2Int _pos, MAP_TYPE mapType)
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
                //    map += "�H";
                //    continue;
                //}
                if (mapTable[y, x] == MAP_TYPE.PLAYER) map += "��";
                else if (mapTable2[y, x] == MAP_TYPE.PLAYER) map += "��";
                else if (mapTable[y, x] == MAP_TYPE.ENEMY) map += "��";
                else if (mapTable2[y, x] == MAP_TYPE.ENEMY) map += "��";
                else if (mapTable[y, x] == MAP_TYPE.WALL) map += "��";
                else if (mapTable[y, x] == MAP_TYPE.GROUND) map += "��";
                else map += "��";
            }
            map += "\n";
        }
        GUI.Label(new Rect(50, 50, 300, 300), map);
    }

    public struct Node
    {
        public bool floor; // �s���Ȃ��Ƃ����false
        public int cost; // �g�p��������
        public int estimatedCost; // goal�܂ł̕���
        public int score; // �X�^�[�g����S�[���܂ł̍ŒZ����

        public Node(bool floor, int cost, int estimatedCost, int score)
        {
            this.floor = floor;
            this.cost = cost;
            this.estimatedCost = estimatedCost;
            this.score = score;
        }
    };
    public Node[,] aStarMap;

    public void makeAStarMap()
    {
        int xSize = mapTable.GetLength(0);
        int ySize = mapTable.GetLength(1);
        //Debug.Log(xSize);
        //Debug.Log(ySize);
        aStarMap = new Node[xSize, ySize];
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                aStarMap[x, y] = new Node(false, 0,0,0);
                //Debug.Log(aStarMap[x, y].floor);
                aStarMap[x, y].floor = (mapTable[x, y] != MAP_TYPE.WALL);
            }
        }
       
        
    }

    public int nextX;
    public int nextY;

    ///<summary>
    ///AStar�Ōo�H��T�����āA���ɍs���ׂ��}�X�̏ꏊ��Ԃ�
    /// </summary>
    /// <param name="enemy">�ړ��O�̏ꏊ�i�}�X�j</param>
    /// <param name="player">�ړI�̏ꏊ�i�}�X�j</param>
    /// <returns>���ɍs���ׂ��ꏊ�i�}�X�j</returns>
    public Vector2Int SearchRoute(Vector2Int enemy, Vector2Int player)
    {
        int xSize = aStarMap.GetLength(0);
        int ySize = aStarMap.GetLength(1);
        // A*�̃}�b�v��S�ăN���A����
        const int Max = 100000000; // �ƂĂ��Ȃ��傫�Ȓl
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                aStarMap[x, y].cost = Max;
                aStarMap[x, y].estimatedCost = 0;
                aStarMap[x, y].score = Max;
            }
        }
        //�����ŁAA*�̒T��������
        //�ړI�n����A�����ւ̒T�������ق����y�Ȃ̂ŁA
        //�ړI�n��0������Ă���
        aStarMap[player.x, player.y].cost = 0;
        aStarMap[player.x, player.y].estimatedCost = Mathf.Abs(player.y - enemy.y) + Mathf.Abs(player.x - enemy.x);
        aStarMap[player.x, player.y].score = aStarMap[player.x, player.y].cost + aStarMap[player.x, player.y].estimatedCost;
        int minScore = aStarMap[player.x, player.y].score; // �ŏ��X�R�A��ۑ����Ă���

        // ��������o�H�T�����n�߂�
        bool loop = true;
        while (loop) // ������܂Ń��[�v����
        {
            int nextMinScore = Max;
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    if (aStarMap[x, y].score == minScore) // ���̏ꏊ����A�S�������ׂ�
                    {
                        int[,] dir = {{1, 0},{0, 1},{-1, 0},{0, -1}};
                        for (int d = 0; d < 4; d++)
                        {
                            nextX = x + dir[d, 0];
                            nextY = y + dir[d, 1];
                            //ToDo
                            //nextX,nextY���Aenemy�Ɠ����ł���΁A�o�H�T�����I������̂ŁA�ŒZ�o�H��x��y��Ԃ�
                            if (nextX == enemy.x && nextY == enemy.y)
                            {
                                loop = false;
                                
                                return new Vector2Int(x, y);
                            }

                            if (nextX < 0 || nextY < 0 || nextX >= xSize || nextY >= ySize) continue;
                            //Debug.Log(nextX + "," + nextY);
                            //ToDo:
                            // aStarMap[nextY,nextX]���ǂłȂ��āAscore��minScore�����傫���̂ł���΁A
                            // cost�AestimatedCost�Ascore���v�Z���ď�������
                            if (aStarMap[nextX, nextY].floor == true && aStarMap[nextX,nextY].score >= minScore)
                            {
                                if (aStarMap[nextX, nextY].cost > aStarMap[x, y].cost)
                                {
                                    aStarMap[nextX, nextY].cost = aStarMap[x, y].cost + 1;
                                    aStarMap[nextX, nextY].estimatedCost = Mathf.Abs(enemy.y - nextY) + Mathf.Abs(enemy.x - nextX);
                                    aStarMap[nextX, nextY].score = aStarMap[nextX, nextY].cost + aStarMap[nextX, nextY].estimatedCost;
                                }
                            }
                            if (nextMinScore > aStarMap[nextX, nextY].score) // ���̍ŏ��l�����߂Ă���
                            {
                                nextMinScore = aStarMap[nextX, nextY].score;
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
