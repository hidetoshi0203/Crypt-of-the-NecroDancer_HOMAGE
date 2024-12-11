using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuiAttackedEnemy : MonoBehaviour
{
    RuiPlayerManager ruiPlayerManager = null;
    public Vector2Int enemyCurrentPos, enemyNextPos;       // ���̍��W
    public Vector2Int attackedEnemyPos; // �v���C���[����U�����ꂽ�G�I�u�W�F�N�g�̍��W
    MapGenerator mapGenerator;
    void Start()
    {
        mapGenerator = transform.parent.GetComponent<MapGenerator>();
    }
    private void Update()
    {
        if (ruiPlayerManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Player");
            ruiPlayerManager = inst.GetComponent<RuiPlayerManager>();
        }

        if (ruiPlayerManager.isPlayerAttack)
        {
            attackedEnemyPos = ruiPlayerManager.playerNextPos;�@// �v���C���[��nextPos��������
            if (attackedEnemyPos == enemyCurrentPos) // �v���C���[����U�����ꂽ���W�ƓG�̍��W���ׂ�
            {
                Destroy(gameObject); // �G�̃I�u�W�F�N�g��Destroy����
                //mapGenerator.UpdateTilie(enemyCurrentPos, MapGenerator.MAP_TYPE.GROUND); // MAP_TYAPE�̍U�����ꂽENEMY��GROUND�ɂ�����
            }

        }
    }
}
