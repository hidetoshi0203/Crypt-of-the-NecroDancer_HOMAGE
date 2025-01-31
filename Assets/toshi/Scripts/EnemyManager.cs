using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    toshiPlayer ToshiPlayer = null;
    public Vector2Int enemyCurrentPos , enemyNextPos;       // ���̍��W
    public Vector2Int attackedEnemyPos; // �v���C���[����U�����ꂽ�G�I�u�W�F�N�g�̍��W
    MapGenerator mapGenerator;
    private int hp = 0;
    public List<GameObject> HeartObjList;
    //[SerializeField] GameObject HeartObj;
    //[SerializeField] GameObject Spawner;
    //[SerializeField] GameObject Spawner1;

    CheckAliveScripts checkAliveScripts;
    private GameObject checkAliveObjs;

    void Start()
    {
        mapGenerator = transform.parent.GetComponent<MapGenerator>();
        hp = HeartObjList.Count;
        //Instantiate(HeartObj, Spawner.transform.position, Quaternion.identity);
        //Instantiate(HeartObj, Spawner1.transform.position, Quaternion.identity);
        checkAliveObjs = GameObject.Find("CheckAliveObjects");
        checkAliveScripts = checkAliveObjs.GetComponent<CheckAliveScripts>();
    }
    private void Update()
    {
        if (ToshiPlayer == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Player");
            ToshiPlayer = inst.GetComponent<toshiPlayer>();
        }
    
        //if (ToshiPlayer.isAttack)
        //{
        //    attackedEnemyPos = ToshiPlayer.playerNextPos;�@// �v���C���[��nextPos��������
        //    if (attackedEnemyPos == enemyCurrentPos) // �v���C���[����U�����ꂽ���W�ƓG�̍��W���ׂ�
        //    { 
        //        Destroy(gameObject); // �G�̃I�u�W�F�N�g��Destroy����
        //        mapGenerator.UpdateTile(enemyCurrentPos, MapGenerator.MAP_TYPE.GROUND); // MAP_TYAPE�̍U�����ꂽENEMY��GROUND�ɂ�����
        //    }
        //}
    }
    public void Hit()
    {
        attackedEnemyPos = ToshiPlayer.playerNextPos; // �v���C���[��nextPos��������
        if (attackedEnemyPos == enemyCurrentPos) // �v���C���[����U�����ꂽ���W�ƓG�̍��W���ׂ�
        {
            hp--;
            if(hp <= 0)
            {
                Destroy(gameObject); // �G�̃I�u�W�F�N�g��Destroy����
                mapGenerator.UpdateTile(enemyCurrentPos, MapGenerator.MAP_TYPE.GROUND); // MAP_TYAPE�̍U�����ꂽENEMY��GROUND�ɂ�����
                checkAliveScripts.isAliveEnemyManagerScr = false;
            }
          HeartObjList[hp].SetActive(false);
        }
    }
}
