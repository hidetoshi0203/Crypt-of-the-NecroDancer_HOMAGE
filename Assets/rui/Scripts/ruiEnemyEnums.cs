using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ruiEnemyEnums : MonoBehaviour
{
    public void EnemyMoveUp()
    {
        transform.position += new Vector3(0, 1, 0);
        Debug.Log("a");
    }
    public void EnemyMoveDown()
    {
        transform.position += new Vector3(0, -1, 0);
    }
    public void EnemyMoveRight()
    {
        transform.position += new Vector3(1, 0, 0);
    }
    public void EnemyMoveLeft()
    {
        transform.position += new Vector3(-1, 0, 0);
    }

}
