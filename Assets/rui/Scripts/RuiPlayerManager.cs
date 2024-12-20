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

    int[,] move = {
      { 0, -1 },�@//TOP�̏ꍇ
      { 1, 0 },   //RIGHT�̏ꍇ
      { 0, 1 },   //DOWN�̏ꍇ
      { -1, 0 }   //LEFT�̏ꍇ
    };

    public DIRECTION direction;
    public Vector2Int playerCurrentPos, playerNextPos;
    public Vector2Int attackedPlayerPos;
    public bool isPlayerAttack;

    MapGenerator mapGenerator;
    NotesManager notesManager = null;
    RuiEnemyManager ruiEnemyManager = null;
    RuiAttackedEnemy ruiAttackedEnemy = null;

    private void Start()
    {
        mapGenerator = transform.parent.GetComponent<MapGenerator>();
        notesManager = GetComponent<NotesManager>();
        direction = DIRECTION.DOWN;
    }




    //�@���͎���_move�֐����ĂԂ悤�ɂ���B
    private void Update()
    {
        if (notesManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("NotesManager");
            notesManager = inst.GetComponent<NotesManager>();
        }

        if (ruiEnemyManager == null && ruiAttackedEnemy == null)
        {
            GameObject instEnemy = GameObject.FindGameObjectWithTag("Enemy");
            ruiEnemyManager = instEnemy.GetComponent<RuiEnemyManager>();
            ruiAttackedEnemy = instEnemy.GetComponent<RuiAttackedEnemy>();
        }

        if (notesManager != null && notesManager.CanInputKey())
        {
            if (notesManager.playerCanMove)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    direction = DIRECTION.TOP;
                    moveType();
                    notesManager.StopTouchSound();
                    notesManager.playerCanMove = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���

                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    direction = DIRECTION.RIGHT;
                    moveType();
                    notesManager.StopTouchSound();
                    notesManager.playerCanMove = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    direction = DIRECTION.DOWN;
                    moveType();
                    notesManager.StopTouchSound();
                    notesManager.playerCanMove = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    direction = DIRECTION.LEFT;
                    moveType();
                    notesManager.StopTouchSound();
                    notesManager.playerCanMove = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
                }
            }
        }

        if (ruiEnemyManager.isEnemyAttack)
        {
            attackedPlayerPos = ruiAttackedEnemy.enemyCurrentPos;
            if (attackedPlayerPos == playerCurrentPos)
            {
                Destroy(this.gameObject);
            }
        }
    }

    //�ړ��p�̊֐�
    void moveType()
    {

        if (notesManager != null && notesManager.CanInputKey())
        {
            playerNextPos = playerCurrentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);

            if (mapGenerator.GetEntityMapType(playerNextPos) == MapGenerator.MAP_TYPE.WALL) // ���͐�(�v���C���[��nextPos)���ǂ������ꍇ
            {
                // �������Ȃ��i��X���̏�ŃW�����v����悤�ȃA�j���[�V����������j
            }
            else if (mapGenerator.GetEntityMapType(playerNextPos) == MapGenerator.MAP_TYPE.ENEMY) // �G�������ꍇ
            {
                // �㉺���E�̓��͔�����Ƃ�bool��true�ɂ���
                if (Input.GetKeyDown(KeyCode.W))
                {
                    isPlayerAttack = true; // EnemyManager.cs��true���󂯎��A�G��|���iMapGenarator.cs��MAP_TYPE��ENEMY����GROUND����������j
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    isPlayerAttack = true;
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    isPlayerAttack = true;
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    isPlayerAttack = true;
                }
            }
            else if (mapGenerator.GetEntityMapType(playerNextPos) != MapGenerator.MAP_TYPE.WALL) // �ǈȊO�������ꍇ
            {

                // �ړ�����
                //mapGenerator.UpdatePlayerTile(playerCurrentPos, MapGenerator.MAP_TYPE.GROUND); // �����̍��W��MAP_TYPE��GROUND�ɂ���
                //transform.localPosition = mapGenerator.ScreenPos(playerNextPos);          // �ړ�
                //playerCurrentPos = playerNextPos;
                //mapGenerator.UpdatePlayerTile(playerCurrentPos, MapGenerator.MAP_TYPE.PLAYER); // �����̍��W��MAP_TYPE��PLAYER�ɂ���
                //Debug.Log(mapGenerator.GetPlayerNextMapType(playerCurrentPos));
            }
            if (mapGenerator.GetEntityMapType(playerCurrentPos) == MapGenerator.MAP_TYPE.STAIRS)
            {
                Debug.Log("�K�i�̏ゾ��");
                mapGenerator.floor++;

                mapGenerator._loadMapData();
                mapGenerator._createMap();
            }
        }
    }
}
