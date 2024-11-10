using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class ruiEnemyManager : MonoBehaviour
{
    toshiPlayer ToshiPlayer = null;
    NotesManager notesManager = null;
    ObjectMove objectMove;
    private bool isEnemyMove = false;
    private float enemyMoveTime;

    public Vector2Int pos;

    // Start is called before the first frame update
    void Start()
    {
        objectMove = GetComponent<ObjectMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ToshiPlayer == null)
        {
            GameObject instPScript = GameObject.FindGameObjectWithTag("Player");
            ToshiPlayer = instPScript.GetComponent<toshiPlayer>();
        }

        if(notesManager == null)
        {
            GameObject instNotesM = GameObject.FindGameObjectWithTag("NotesManager");
            notesManager = instNotesM.GetComponent<NotesManager>();
        }

        enemyMoveTime += Time.deltaTime;

        if (enemyMoveTime > 1)
        {
            isEnemyMove = true;
            enemyMoveTime = 0;
        }

        if (notesManager != null && notesManager.CanInputKey())
        {
            notesManager.enemyCanMove = true;

            if (!ToshiPlayer.isAttack && isEnemyMove && notesManager.enemyCanMove)
            {
                objectMove.direction = ObjectMove.DIRECTION.TOP;
                objectMove.MoveMent();
            }

            if (ToshiPlayer.isAttack)
            {
                Destroy(gameObject);
                //mapGenerator.UpdateTilie(pos, MapGenerator.MAP_TYPE.GROUND);
            }
        }
    }
}
