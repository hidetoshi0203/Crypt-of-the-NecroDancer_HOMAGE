using System;
using UnityEngine;

public class EnemySlimeMoveComponent : MonoBehaviour
{
    private enum DIRECTION
    {
        TOP,
        RIGHT,
        DOWN,
        LEFT
    }

    Vector2Int[] move = {
      new Vector2Int(  0, -1 ),�@//TOP�̏ꍇ
      new Vector2Int(  1,  0 ),   //RIGHT�̏ꍇ
      new Vector2Int(  0,  1 ),   //DOWN�̏ꍇ
      new Vector2Int( -1,  0 )   //LEFT�̏ꍇ
    };
    [SerializeField] private DIRECTION[] moveDirections;
    [SerializeField] private int currentDirectionIndex = 0;
    private DIRECTION direction;
    MapGenerator mapGenerator;
    NotesManager notesManager = null;
    EnemyManager enemyManager;
    PlayerManager playerManager;
    int moveCount = 0;//���������񓮂�����
    private bool isEnemyTurn = false;

    PlayerDamageSound playerDamageSound;

    void Start()
    {
        mapGenerator = transform.parent.GetComponent<MapGenerator>();
        enemyManager = GetComponent<EnemyManager>();
        if (moveDirections.Length == 0) return;
        direction = moveDirections[currentDirectionIndex];

    }

    void Update()
    {
        if(moveDirections.Length == 0) return;

        // playerManager�̎擾
        if (playerManager == null)
        {
            GameObject inst = GameObject.Find("PlayerManager");
            playerManager = inst.GetComponent<PlayerManager>();
        }
        if(notesManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("NotesManager");
            notesManager = inst.GetComponent<NotesManager>();
        }

        if (playerDamageSound == null)
        {
            GameObject inst = GameObject.Find("PlayerDamageSound");
            playerDamageSound = inst.GetComponent<PlayerDamageSound>();
        }
        if (notesManager.enemyCanMove)
        {
            if (!isEnemyTurn) return; // ���s���ɂ���
            else
            {
                // �^�[���̏I��
                isEnemyTurn = false;

                // 2�^�[���Ɉ��s���ɂ���
                moveCount = (moveCount + 1) % 2; 
                if (moveCount == 0) return; 
                
                // �ړ�
                eMoveType();
            }
        }
        else
        {
            isEnemyTurn = true;
        }

    }
    void eMoveType()
    {
        enemyManager.enemyNextPos = enemyManager.enemyCurrentPos + move[(int)direction];
        if (mapGenerator.GetEntityMapType(enemyManager.enemyNextPos) == MapGenerator.MAP_TYPE.PLAYER)
        {
            Debug.Log("�U���G�l�~�[��");
            playerDamageSound.DamageSound();
            // �v���C���[�ɍU������
            playerManager.Hit();
        }
        else if (mapGenerator.GetStageMapType(enemyManager.enemyNextPos) != MapGenerator.MAP_TYPE.WALL ||
                        mapGenerator.GetStageMapType(enemyManager.enemyNextPos) != MapGenerator.MAP_TYPE.WALL2)
        {
            //�ړ�
            mapGenerator.UpdateTile(enemyManager.enemyCurrentPos, MapGenerator.MAP_TYPE.GROUND);
            transform.localPosition = mapGenerator.ScreenPos(enemyManager.enemyNextPos);
            enemyManager.enemyCurrentPos = enemyManager.enemyNextPos;
            mapGenerator.UpdateTile(enemyManager.enemyCurrentPos, MapGenerator.MAP_TYPE.ENEMY);
            ChangeMoveDirection();
        }
    }

    
    private void ChangeMoveDirection()
    {
        currentDirectionIndex = (currentDirectionIndex + 1) % moveDirections.Length;
        direction = moveDirections[currentDirectionIndex];
    }

}
