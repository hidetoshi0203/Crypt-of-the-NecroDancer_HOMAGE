using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyJudgement : MonoBehaviour
{
    public bool isEnemyJudge;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("WTag"))
        {
            isEnemyJudge = true;
        }
        if (collision.gameObject.CompareTag("ATag"))
        {
            isEnemyJudge = true;
        }
        if (collision.gameObject.CompareTag("STag"))
        {
            isEnemyJudge = true;    
        }
        if (collision.gameObject.CompareTag("DTag"))
        {
            isEnemyJudge= true;
        }
    }
}
