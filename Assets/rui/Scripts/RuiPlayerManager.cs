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
      { 0, -1 },　//TOPの場合
      { 1, 0 },   //RIGHTの場合
      { 0, 1 },   //DOWNの場合
      { -1, 0 }   //LEFTの場合
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




    //　入力時に_move関数を呼ぶようにする。
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
                    notesManager.playerCanMove = false; //フラグをオフにして音を鳴らせないようにする

                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    direction = DIRECTION.RIGHT;
                    moveType();
                    notesManager.StopTouchSound();
                    notesManager.playerCanMove = false; //フラグをオフにして音を鳴らせないようにする
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    direction = DIRECTION.DOWN;
                    moveType();
                    notesManager.StopTouchSound();
                    notesManager.playerCanMove = false; //フラグをオフにして音を鳴らせないようにする
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    direction = DIRECTION.LEFT;
                    moveType();
                    notesManager.StopTouchSound();
                    notesManager.playerCanMove = false; //フラグをオフにして音を鳴らせないようにする
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

    //移動用の関数
    void moveType()
    {

        if (notesManager != null && notesManager.CanInputKey())
        {
            playerNextPos = playerCurrentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);

            if (mapGenerator.GetEntityMapType(playerNextPos) == MapGenerator.MAP_TYPE.WALL) // 入力先(プレイヤーのnextPos)が壁だった場合
            {
                // 何もしない（後々その場でジャンプするようなアニメーションを入れる）
            }
            else if (mapGenerator.GetEntityMapType(playerNextPos) == MapGenerator.MAP_TYPE.ENEMY) // 敵だった場合
            {
                // 上下左右の入力判定をとりboolをtrueにする
                if (Input.GetKeyDown(KeyCode.W))
                {
                    isPlayerAttack = true; // EnemyManager.csでtrueを受け取り、敵を倒す（MapGenarator.csのMAP_TYPEをENEMYからGROUND書き換える）
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
            else if (mapGenerator.GetEntityMapType(playerNextPos) != MapGenerator.MAP_TYPE.WALL) // 壁以外だった場合
            {

                // 移動する
                //mapGenerator.UpdatePlayerTile(playerCurrentPos, MapGenerator.MAP_TYPE.GROUND); // 自分の座標のMAP_TYPEをGROUNDにする
                //transform.localPosition = mapGenerator.ScreenPos(playerNextPos);          // 移動
                //playerCurrentPos = playerNextPos;
                //mapGenerator.UpdatePlayerTile(playerCurrentPos, MapGenerator.MAP_TYPE.PLAYER); // 自分の座標のMAP_TYPEをPLAYERにする
                //Debug.Log(mapGenerator.GetPlayerNextMapType(playerCurrentPos));
            }
            if (mapGenerator.GetEntityMapType(playerCurrentPos) == MapGenerator.MAP_TYPE.STAIRS)
            {
                Debug.Log("階段の上だよ");
                mapGenerator.floor++;

                mapGenerator._loadMapData();
                mapGenerator._createMap();
            }
        }
    }
}
