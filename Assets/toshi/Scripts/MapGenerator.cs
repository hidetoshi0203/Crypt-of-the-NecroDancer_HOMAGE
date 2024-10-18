using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
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
    MAP_TYPE[,] mapTable;

    public MAP_TYPE GetNextMapType(Vector2Int _pos)
    {
        return mapTable[_pos.x, _pos.y];
    }

    void Start()
    {
        _loadMapData();

        //�ǉ��@�}�b�v�����֐����Ăяo��
        _createMap();
    }

    void _loadMapData()
    {
        string[] mapLines = mapText.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);


        int row = mapLines.Length;
        int col = mapLines[0].Split(new char[] { ',' }).Length;
        mapTable = new MAP_TYPE[col, row];

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

                //�ȉ�2�s�C���@�������Ɏ��g��transform��n��
                GameObject _ground = Instantiate(prefabs[(int)MAP_TYPE.GROUND], transform);
                GameObject _map = Instantiate(prefabs[(int)mapTable[x, y]], transform);


                _ground.transform.position = ScreenPos(pos);
                _map.transform.position = ScreenPos(pos);

                if (mapTable[x, y] == MAP_TYPE.PLAYER)
                {
                    _map.GetComponent<toshiPlayer>().currentPos = pos;

                }
            }
        }
    }
    //�C���@public�ɂ��Ă���
    public Vector2 ScreenPos(Vector2Int _pos)
    {
        return new Vector2(
            _pos.x * mapSize - centerPos.x,
            -(_pos.y * mapSize - centerPos.y));

    }
}
