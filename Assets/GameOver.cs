using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    PlayerManager playerManager;
    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }
    private void Update()
    {
        if (playerManager.playerHP == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
