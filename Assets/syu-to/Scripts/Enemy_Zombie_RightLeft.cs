using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Zombie_RightLeft : MonoBehaviour
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
    MapGenerator mapGenerator;
    NotesManager notesManager = null;
    EnemyManager enemyManager;
    PlayerManager playerManager;
    GameObject leftNotes;
    GameObject rightNotes;
    GameObject function;
    int moveCount = 0;//���������񓮂�����
    public bool isEnemyAttack = false;

    PlayerDamageSound playerDamageSound;

    void Start()
    {
        mapGenerator = transform.parent.GetComponent<MapGenerator>();
        notesManager = GetComponent<NotesManager>();
        enemyManager = GetComponent<EnemyManager>();
        direction = DIRECTION.LEFT;

    }
    // Update is called once per frame
    void Update()
    {

        if (notesManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("NotesManager");
            notesManager = inst.GetComponent<NotesManager>();
        }
        if (playerManager == null)
        {
            GameObject inst = GameObject.Find("PlayerManager");
            playerManager = inst.GetComponent<PlayerManager>();
        }

        if (playerDamageSound == null)
        {
            GameObject inst = GameObject.Find("PlayerDamageSound");
            playerDamageSound = inst.GetComponent<PlayerDamageSound>();
        }

        if (notesManager != null && notesManager.CanInputKey())
        {
            if (moveCount != 1 && notesManager.enemyCanMove)
            {
                moveCount++;
                switch (direction)
                {
                    case DIRECTION.RIGHT:
                        eMoveType();
                        break;
                    case DIRECTION.LEFT:
                        eMoveType();
                        break;
                }
            }
            else { notesManager.enemyCanMove = false; moveCount = 0; }
        }

    }
    void eMoveType()
    {
        if (notesManager != null && notesManager.CanInputKey())
        {
            enemyManager.enemyNextPos = enemyManager.enemyCurrentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);
            if (mapGenerator.GetEntityMapType(enemyManager.enemyNextPos) == MapGenerator.MAP_TYPE.PLAYER)
            {
                // �v���C���[�ɍU������
                isEnemyAttack = true;
                playerDamageSound.DamageSound();
                StartCoroutine(playerManager.Damage());
            }
            else if (mapGenerator.GetEntityMapType(enemyManager.enemyNextPos) >= MapGenerator.MAP_TYPE.ENEMY && mapGenerator.GetEntityMapType(enemyManager.enemyNextPos) <= MapGenerator.MAP_TYPE.ENEMY6)
            {
                changeDirection();
            }
            else if (mapGenerator.GetStageMapType(enemyManager.enemyNextPos) == MapGenerator.MAP_TYPE.GROUND)
            {
                //�ړ�
                mapGenerator.UpdateTile(enemyManager.enemyCurrentPos, MapGenerator.MAP_TYPE.GROUND);
                transform.localPosition = mapGenerator.ScreenPos(enemyManager.enemyNextPos);
                enemyManager.enemyCurrentPos = enemyManager.enemyNextPos;
                mapGenerator.UpdateTile(enemyManager.enemyCurrentPos, MapGenerator.MAP_TYPE.ENEMY2);
            }
            else
            {
                changeDirection();
            }
        }
    }
    void changeDirection()
    {
        if (direction == DIRECTION.RIGHT)
        {
            direction = DIRECTION.LEFT;
        }
        else
        {
            direction = DIRECTION.RIGHT;
        }
        //eMoveType(); // �����]������Ƃ��m�[�c1�񕪑҂��Ȃ��ōs�� 
    }
}
