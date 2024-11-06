using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    toshiPlayer ToshiPlayer = null;
    public Vector2Int enemyCurrentPos;       // ���̍��W
    public Vector2Int attackedPos; // �v���C���[����U�����ꂽ�G�I�u�W�F�N�g�̍��W
    MapGenerator mapGenerator;
    void Start()
    {
        mapGenerator = transform.parent.GetComponent<MapGenerator>();
    }
    private void Update()
    {
        if (ToshiPlayer == null) 
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Player");
            ToshiPlayer = inst.GetComponent<toshiPlayer>();
        }
    
        if (ToshiPlayer.isAttack)
        {
            attackedPos = ToshiPlayer.playerNextPos;�@// �v���C���[��nextPos��������
            if (attackedPos == enemyCurrentPos) // �v���C���[����U�����ꂽ���W�ƓG�̍��W���ׂ�
            { 
                Destroy(gameObject); // �G�̃I�u�W�F�N�g��Destroy����
                mapGenerator.UpdateTilie(enemyCurrentPos, MapGenerator.MAP_TYPE.GROUND); // MAP_TYAPE�̍U�����ꂽENEMY��GROUND�ɂ�����
            }

        }
    }
}
