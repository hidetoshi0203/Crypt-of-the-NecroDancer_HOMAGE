using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;
using static UnityEditor.Progress;
public class RuitoshiPlayer : MonoBehaviour
{
    public enum DIRECTION
    {
        TOP,
        RIGHT,
        DOWN,
        LEFT,
    }

    int[,] move = {
      { 0, -1 },　//TOPの場合
      { 1, 0 },   //RIGHTの場合
      { 0, 1 },   //DOWNの場合
      { -1, 0 },  //LEFTの場合
    };

    public DIRECTION direction;
    public Vector2Int playerCurrentPos, playerNextPos;
    public bool isAttack;
    public bool isPlayerMove;
    Camera cam;
    RuiMapGenerator mapGenerator;
    NotesManager notesManager = null;
    ComboManager comboManager = null;
    GameObject leftNotes;
    GameObject rightNotes;
    GameObject function;
    GameObject notesObjets;

    RuiPlayerManager ruiPlayerManager = null;
    Item itemHPotion = null;
    Item itemSPotion = null;

    public float playerAttackPower = 1;
    private void Start()
    {
        mapGenerator = transform.parent.GetComponent<RuiMapGenerator>();
        notesManager = GetComponent<NotesManager>();
        comboManager = GetComponent<ComboManager>();
        direction = DIRECTION.DOWN;
        notesObjets = GameObject.FindGameObjectWithTag("Notes");
        cam = Camera.main;
        cam.transform.position = transform.position + new Vector3(0,0,-1);

        ruiPlayerManager = GetComponent<RuiPlayerManager>();
    }


    private void FixedUpdate()
    {
        cam.transform.position = transform.position + new Vector3(0, 0, -1);
    }

    //　入力時に_move関数を呼ぶようにする。
    private void Update()
    {
        if (ruiPlayerManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("PlayerManager");
            ruiPlayerManager = inst.GetComponent<RuiPlayerManager>();
        }

        if (notesManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("NotesManager");
            notesManager = inst.GetComponent<NotesManager>();
        }
        if (comboManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("ComboManager");
            comboManager = inst.GetComponent <ComboManager>();
        }
        //if (itemHPotion == null)
        //{
        //    GameObject inst = GameObject.FindGameObjectWithTag("HealingPotion");
        //    itemHPotion = inst.GetComponent<Item>();
        //}
        if (itemSPotion == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("StrengthPotion");
            itemSPotion = inst.GetComponent<Item>();
        }
        if (notesManager != null && notesManager.CanInputKey())
        {
            if (notesManager.playerCanMove)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    HandlePlayerMove(DIRECTION.TOP);
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    HandlePlayerMove(DIRECTION.RIGHT);
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    HandlePlayerMove(DIRECTION.DOWN);
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    HandlePlayerMove(DIRECTION.LEFT);
                }
            }
        }
    }

    private void HandlePlayerMove(DIRECTION directionInput)
    {
        direction = directionInput;
        moveType();

        notesManager.StopTouchSound();
    }

    //移動用の関数
    void moveType()
    {
        
        if (notesManager != null && notesManager.CanInputKey())
        {
            playerNextPos = playerCurrentPos + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);

            switch (mapGenerator.GetStageMapType(playerNextPos))
            {
                case RuiMapGenerator.MAP_TYPE.GROUND:
                    Move();
                    break;
                case RuiMapGenerator.MAP_TYPE.HEALINGPOTION:
                    Heal();
                    mapGenerator.GetItem(playerNextPos);
                    Move();
                    break;
                case RuiMapGenerator.MAP_TYPE.STRENGTHPOTION:
                    // プレイヤーの攻撃力を上げる関数
                    mapGenerator.GetItem(playerNextPos);
                    Move();
                    break;
                case RuiMapGenerator.MAP_TYPE.WALL:
                    // 何もしない（後々その場でジャンプするようなアニメーションを入れる）
                    break;
                case RuiMapGenerator.MAP_TYPE.STAIRS:
                    // 次のステージに進む
                    break;
                case RuiMapGenerator.MAP_TYPE.WALL2:
                    // 何もしない（後々その場でジャンプするようなアニメーションを入れる）
                    break;
            }

            switch (mapGenerator.GetEntityMapType(playerNextPos))
            {
                case RuiMapGenerator.MAP_TYPE.PLAYER:
                    // バグ
                    break;
                case RuiMapGenerator.MAP_TYPE.ENEMY:
                    // 攻撃
                    Attack();
                    break;
                case RuiMapGenerator.MAP_TYPE.ENEMY2:
                    // 攻撃

                    break;
            }

            //switch (mapGenerator.GetItemMapType(playerNextPos))
            //{
            //    case RuiMapGenerator.MAP_TYPE.HEALINGPOTION:
            //        Move();
            //        itemHPotion.HealingHP();
            //        break;
            //}

            //if (mapGenerator.GetPlayerNextMapType(playerNextPos) == RuiMapGenerator.MAP_TYPE.WALL && mapGenerator.GetPlayerNextMapType(playerNextPos) == RuiMapGenerator.MAP_TYPE.WALL2) // 入力先(プレイヤーのnextPos)が壁だった場合
            //{
            //    
            //}
            if (mapGenerator.GetEntityMapType(playerNextPos) == RuiMapGenerator.MAP_TYPE.ENEMY) // 敵だった場合
            {
                // 上下左右の入力判定をとりboolをtrueにする
                if (Input.GetKeyDown(KeyCode.W))
                {
                    isAttack = true; // EnemyManager.csでtrueを受け取り、敵を倒す（MapGenarator.csのMAP_TYPEをENEMYからGROUND書き換える）
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    isAttack = true;
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    isAttack = true;
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    isAttack = true;
                }
            }

            if (mapGenerator.GetStageMapType(playerCurrentPos) == RuiMapGenerator.MAP_TYPE.STAIRS)
            {
                Debug.Log("階段の上だよ");
                GameObject parentObject = GameObject.Find("MapChip");

                int childCount = parentObject.transform.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    Transform childTransform = parentObject.transform.GetChild(i);
                    GameObject childObject = childTransform.gameObject;
                    Destroy(childObject);
                }

                mapGenerator.floor++;

                mapGenerator._loadMapData();
                mapGenerator._createMap();
            }
        }
        void Attack()
        {

        }

        void Heal() // プレイヤーが回復する関数
        {
            if (ruiPlayerManager.playerHP < 3) // プレイヤーの体力が減ってたら(3HP未満だったら)
            {
                ruiPlayerManager.playerHP++; // プレイヤー体力(HP)を回復する
            }
        }

        void playerPowerAttackUp()
        {
            playerAttackPower++;
        }

        void Move()
        {
            //Debug.Log(playerCurrentPos);
            //Debug.Log("床だよ");
            // 移動する
            mapGenerator.UpdateTile(playerCurrentPos, RuiMapGenerator.MAP_TYPE.GROUND); // 自分の座標のMAP_TYPEをGROUNDにする
            transform.localPosition = mapGenerator.ScreenPos(playerNextPos);          // 移動
            playerCurrentPos = playerNextPos;
            mapGenerator.UpdateTile(playerCurrentPos, RuiMapGenerator.MAP_TYPE.PLAYER); // 自分の座標のMAP_TYPEをPLAYERにする
        }
    }
}
