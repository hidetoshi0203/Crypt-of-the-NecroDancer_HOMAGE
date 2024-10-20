using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class ruiEnemyManager : MonoBehaviour
{
    ObjectMove objectMove;
    public GameObject slime;
    private bool isEnemyMoveUp;
    private float enemyMoveTime;

    // Start is called before the first frame update
    void Start()
    {
        objectMove = GetComponent<ObjectMove>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyMoveTime += Time.deltaTime;

        if (enemyMoveTime > 1)
        {
            enemyMoveTime = 0;

            if (slime.name == "Slime" && isEnemyMoveUp)
            {
                objectMove.direction = ObjectMove.DIRECTION.TOP;
                objectMove.MoveMent();
            }
            else if (slime.name == "Slime" && !isEnemyMoveUp)
            {
                objectMove.direction = ObjectMove.DIRECTION.DOWN;
                objectMove.MoveMent();
            }
        }
    }
}
