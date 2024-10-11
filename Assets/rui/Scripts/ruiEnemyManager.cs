using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class ruiEnemyManager : MonoBehaviour
{
    [SerializeField]
    ruiEnemyEnums enemyEnums;
    public GameObject slime;

    // Start is called before the first frame update
    void Start()
    {
        enemyEnums = GetComponent<ruiEnemyEnums>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slime.name == "Slime")
        {
            enemyEnums.EnemyMoveUp();
        }
    }
}
