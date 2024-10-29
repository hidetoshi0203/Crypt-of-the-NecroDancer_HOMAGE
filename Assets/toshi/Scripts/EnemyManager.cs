using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    toshiPlayer ToshiPlayer = null;
    public Vector2Int pos;
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
            Destroy(gameObject);
            mapGenerator.UpdateTilie(pos, MapGenerator.MAP_TYPE.GROUND);
        }
    }
}
