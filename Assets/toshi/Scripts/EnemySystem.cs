using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    List<EnemyManager> enemyManagers = new();
    public void SetEnemyManagers(List<EnemyManager> enemyManagers)
    {
        this.enemyManagers = new(); // ������
        this.enemyManagers = enemyManagers; // ���݂�Map�̓G���i�[
    }

    /// <summary>
    /// �U������ꏊ�ɂ���G�������{�U��
    /// </summary>
    /// <param name="hitPosition">�U������ꏊ</param>
    public void Hit(Vector2 hitPosition)
    {
        foreach(EnemyManager enemyManager in enemyManagers)
        {
            if(enemyManager.enemyCurrentPos != hitPosition) continue; // �U������ꏊ�ɂ��Ȃ��ꍇ�X�L�b�v
            enemyManager.Hit(); // ������U��
        }
    }
}
