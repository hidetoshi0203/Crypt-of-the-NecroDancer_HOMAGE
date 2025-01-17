using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    List<EnemyManager> enemyManagers = new();
    public void SetEnemyManagers(List<EnemyManager> enemyManagers)
    {
        this.enemyManagers = new(); // ‰Šú‰»
        this.enemyManagers = enemyManagers; // Œ»İ‚ÌMap‚Ì“G‚ğŠi”[
    }

    /// <summary>
    /// UŒ‚‚·‚éêŠ‚É‚¢‚é“G‚ğŒŸõ{UŒ‚
    /// </summary>
    /// <param name="hitPosition">UŒ‚‚·‚éêŠ</param>
    public void Hit(Vector2 hitPosition)
    {
        foreach(EnemyManager enemyManager in enemyManagers)
        {
            if(enemyManager.enemyCurrentPos != hitPosition) continue; // UŒ‚‚·‚éêŠ‚É‚¢‚È‚¢ê‡ƒXƒLƒbƒv
            enemyManager.Hit(); // ‚¢‚½‚çUŒ‚
        }
    }
}
