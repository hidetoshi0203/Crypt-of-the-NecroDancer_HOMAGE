using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;
public class toshiPlayer : MonoBehaviour
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
    public bool isAttack;

    MapGenerator mapGenerator;
    NotesManager notesManager = null;
    GameObject leftNotes;
    GameObject rightNotes;
    GameObject function;

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
    }

        //移動用の関数
        void moveType()
    {
        if (notesManager != null && notesManager.CanInputKey())
        {
            playerNextPos = playerCurrentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);

            if (mapGenerator.GetPlayerNextMapType(playerNextPos) == MapGenerator.MAP_TYPE.WALL) // 入力先(プレイヤーのnextPos)が壁だった場合
            {
                // 何もしない（後々その場でジャンプするようなアニメーションを入れる）
            }
            else if (mapGenerator.GetPlayerNextMapType(playerNextPos) == MapGenerator.MAP_TYPE.ENEMY) // 敵だった場合
            {
                // 上下左右の入力判定をとりboolをtrueにする
                if (Input.GetKeyDown(KeyCode.W))
                {
                    isAttack = true; // EnemyManager.csでtrueを受け取り、敵を倒す（MapGenarator.csのMAP_TYPEをENEMYからGROUND書き換える）
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    isAttack = true;
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    isAttack = true;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    isAttack = true;
                }
            }
            else if (mapGenerator.GetPlayerNextMapType(playerNextPos) != MapGenerator.MAP_TYPE.WALL) // 壁以外だった場合
            {
                // 移動する
                mapGenerator.UpdateTilie(playerCurrentPos, MapGenerator.MAP_TYPE.GROUND); // 自分の座標のMAP_TYPEをGROUNDにする
                transform.localPosition = mapGenerator.ScreenPos(playerNextPos);          // 移動
                playerCurrentPos = playerNextPos;
                mapGenerator.UpdateTilie(playerCurrentPos, MapGenerator.MAP_TYPE.PLAYER); // 自分の座標のMAP_TYPEをPLAYERにする
            }
        }
    }
}
