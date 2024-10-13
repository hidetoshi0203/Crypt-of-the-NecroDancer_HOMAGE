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
    Vector2 centerPos; // ���S���W�ϐ�
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

        //�}�b�v�̐����֐�
        createMap();
    }
    MAP_TYPE[,] mapTable;

    void loadMapData()
    {
        mapLines = mapText.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        //MAP_TYPE�̔z�񏉊����̂��߂̒l���擾

        //�s�̐�
        int row = mapLines.Length;
        //��̐�
        int column = mapLines[0].Split(new char[] { ',' }).Length;
        //������
        mapTable = new MAP_TYPE[column, row];
        //�s�̐��������[�v
        for (int y = 0; y < row; y++)
        {
            //1�s���J���}��؂�ŕ���
            string[] mapValues = mapLines[y].Split(new char[] { ',' });
            //��̐��������[�v
            for (int x = 0; x < column; x++)
            {
                //mapValues��x�Ԗڂ�MAP�QTYPE�ɂ��Ⴗ�Ƃ���mapTable[x,y]�Ԗڂɑ��
                mapTable[x, y] = (MAP_TYPE)int.Parse(mapValues[x]);
            }
        }
        
    }

    void createMap()
    {
        //�T�C�Y���擾����
        mapSize = objPrefabs[0].GetComponent<SpriteRenderer>().bounds.size.x;

        //���S���W���擾
        //�c���̐��𔼕��ɂ���mapSize���|���邱�ƂŒ��S�����߂�

        //�񂪋����̏ꍇ
        if (mapTable.GetLength(0) % 2 == 0)
        {
            centerPos.x = mapTable.GetLength(0) / 2* mapSize - (mapSize / 2);   
        }
        else
        {
            centerPos.x = mapTable.GetLength(0) / 2* mapSize;
        }
        //�s�����̏ꍇ
        if (mapTable.GetLength(1) % 2 == 0)
        {
            centerPos.y = mapTable.GetLength(1) / 2* mapSize - (mapSize / 2);
        }
        else
        {
            centerPos.y = mapTable.GetLength(1) / 2* mapSize;
        }
        //mapTable�̍s�̃��[�v
        for (int y = 0;y < mapTable.GetLength(1);y++) 
        {
            //mapTable�̗�̃��[�v
            for (int x = 0;x < mapTable.GetLength(0); x++)
            {
                //���݂̃|�W�V����
                Vector2Int pos = new Vector2Int(x, y);
                // floor��~���l�߂�
                GameObject floor = Instantiate(objPrefabs[(int)MAP_TYPE.FLOOR], transform);
                floor.transform.position = screenPos(pos);
                //objPrefab�̒���mapTable[x,y]�ɂ�������̂𐶐�
                GameObject map = Instantiate(objPrefabs[(int)mapTable[x,y]],transform);
                //���������Q�[���I�u�W�F�N�g�̈ʒu��ݒ�
                map.transform.position = screenPos(pos); 

                //Player�X�N���v�g��currntpos��pos����
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
            //y���}�C�i�X�ɂ��ď㉺���]�𒼂�
            -(_pos.y * mapSize - centerPos.y));
    }
}
