using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ruiEnemyManager : MonoBehaviour
{
    ruiEnemyEnumes enemyEnumes;

    // Start is called before the first frame update
    void Start()
    {
        enemyEnumes = GetComponent<ruiEnemyEnumes>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
