using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] TextAsset[] mapText;
    [SerializeField] GameObject[] prefabs;
    float mapSize;
    public int floor = 0;
    //Vector2 centerPos;
    

    public enum MAP_TYPE
    {   /*
        GROUND, // 0 �n��
        WALL,   // 1 ��
        PLAYER, // 2 �v���C���[
        ENEMY,  // 3 �G�i1�̖ځj
        STAIRS, // 4 �K�i
        WALL2,   // 5 �ǁi�j
        ENEMY2,  // 6 �G2
        ENEMY3  // 7 �G3
        */

        GROUND,     // 0 �n��
        WALL,       // 1 ��
        WALL2,      // 2 �ǁi�j
        PLAYER,     // 3 �v���C���[
        STAIRS,     // 4 �K�i
        ENEMY,      // 5 �G(�X���C���㉺)
        ENEMY_2,    // 6 �G(�X���C�����E)
        ENEMY2,     // 7 �G2(�]���r���E)
        ENEMY2_1,   // 8 �G2(�]���r�㉺)
        ENEMY3      // 9 �G3(�P���^�E���X)
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
        //Debug.Log(_pos);
        return mapTable2[_pos.x, _pos.y];
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



        //if (mapTable.GetLength(0) % 2 == 0)
        //{
        //    centerPos.x = mapTable.GetLength(0) / 2 * mapSize - (mapSize / 2);
        //}
        //else
        //{
        //    centerPos.x = mapTable.GetLength(0) / 2 * mapSize;
        //}

        //if (mapTable.GetLength(1) % 2 == 0)
        //{
        //    centerPos.y = mapTable.GetLength(1) / 2 * mapSize - (mapSize / 2);
        //}
        //else
        //{
        //    centerPos.y = mapTable.GetLength(1) / 2 * mapSize;
        //}


        for (int y = 0; y < mapTable.GetLength(1); y++)
        {
            for (int x = 0; x < mapTable.GetLength(0); x++)
            {
                Vector2Int pos = new Vector2Int(x, y);

                GameObject _ground = Instantiate(prefabs[(int)MAP_TYPE.GROUND], transform);
                GameObject _map = Instantiate(prefabs[(int)mapTable[x, y]], transform);
                if (mapTable[x, y] == MAP_TYPE.ENEMY)
                {
                    _map.GetComponent<EnemyManager>().enemyCurrentPos = pos;
                    mapTable[x, y] = MAP_TYPE.GROUND;
                    mapTable2[x, y] = MAP_TYPE.ENEMY;
                }

                if (mapTable[x, y] == MAP_TYPE.ENEMY_2)
                {
                    _map.GetComponent<EnemyManager>().enemyCurrentPos = pos;
                    mapTable[x, y] = MAP_TYPE.GROUND;
                    mapTable2[x, y] = MAP_TYPE.ENEMY;
                }

                if (mapTable[x, y] == MAP_TYPE.ENEMY2)
                {
                    _map.GetComponent<EnemyManager>().enemyCurrentPos = pos;
                    mapTable[x, y] = MAP_TYPE.GROUND;
                    mapTable2[x, y] = MAP_TYPE.ENEMY;
                }

                if (mapTable[x, y] == MAP_TYPE.ENEMY2_1)
                {
                    _map.GetComponent<EnemyManager>().enemyCurrentPos = pos;
                    mapTable[x, y] = MAP_TYPE.GROUND;
                    mapTable2[x, y] = MAP_TYPE.ENEMY;
                }

                if (mapTable[x, y] == MAP_TYPE.ENEMY3)
                {
                    _map.GetComponent<EnemyManager>().enemyCurrentPos = pos;
                    mapTable[x, y] = MAP_TYPE.GROUND;
                    mapTable2[x, y] = MAP_TYPE.ENEMY;
                }


                _ground.transform.position = ScreenPos(pos);
                _map.transform.position = ScreenPos(pos);

                if (mapTable[x, y] == MAP_TYPE.PLAYER)
                {
                    _map.GetComponent<toshiPlayer>().playerCurrentPos = pos;
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
            -(_pos.y * mapSize /*- centerPos.y)*/));

    }
   public void UpdateMapTile(Vector2Int _pos,MAP_TYPE mapType) // �}�b�v�̏��
    {
        mapTable[_pos.x, _pos.y] = mapType;
    }
    public void UpdateTile(Vector2Int _pos, MAP_TYPE mapType) // �v���C���[�A�G�l�~�[�̏��i�ړ��ȂǂŎg���j
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
                if (mapTable2[y, x] == MAP_TYPE.PLAYER) map += "��";
                else if (mapTable2[y, x] == MAP_TYPE.ENEMY) map += "��";
                else if (mapTable[y, x] == MAP_TYPE.ENEMY) map += "��";
                else if (mapTable[y, x] == MAP_TYPE.WALL) map += "��";
                else if (mapTable[y, x] == MAP_TYPE.PLAYER) map += "��";
                else if (mapTable[y, x] == MAP_TYPE.GROUND) map += "��";
                else map += "��";
            }
            map += "\n";
        }
        GUI.Label(new Rect(50, 50, 300, 300), map);
    }
}
