using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class ruiEnemyManager : MonoBehaviour
{
    public GameObject slime;
    bool isEnemyMove = false;
    private float enemyMoveTime;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        enemyMoveTime += Time.deltaTime;

        if (enemyMoveTime > 1)
        {
            isEnemyMove = false;
            enemyMoveTime = 0;
        }

        if (slime.name == "Slime")
        {

        }
    }
}
