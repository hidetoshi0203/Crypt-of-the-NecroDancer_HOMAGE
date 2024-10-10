using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ruiEnemyEnumes : MonoBehaviour
{
    enum enemy1
    {
        RIGHT_1,
        LEFT_1,
        RIGHT_2,
        LEFT_2
    }

    enemy1 enemy1Move = enemy1.RIGHT_1;

    enum enemy2
    {
        UP_1,
        DOWN_1,
        UP_2,
        DOWN_2
    }

    enemy2 enemy2Move = enemy2.UP_1;

    enum enemy3
    {
        RIGHT_1,
        RIGHT_2,
        UP,
        LEFT,
        DOWN
    }

    enemy3 enemy3Move = enemy3.RIGHT_1;
}
