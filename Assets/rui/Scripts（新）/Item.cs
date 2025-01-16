using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    RuiMapGenerator ruiMapGenerator = null;
    RuiPlayerManager ruiPlayerManager = null;
    GameObject HPotionObj;
    GameObject SPotionObj;

    public Vector2Int HPotionCurrentPos;
    public Vector2Int SPotionCurrentPos;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (ruiMapGenerator == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("MapChip");
            ruiMapGenerator = inst.GetComponent<RuiMapGenerator>();
        }

        if (ruiPlayerManager == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("PlayerManager");
            ruiPlayerManager = inst.GetComponent<RuiPlayerManager>();
        }

        HPotionObj = GameObject.FindGameObjectWithTag("HealingPotion");
        SPotionObj = GameObject.FindGameObjectWithTag("StrengthPotion");
    }

    public void HealingHP()
    {
        if (ruiPlayerManager.playerHP < 3)
        {
            ruiPlayerManager.playerHP++;
        }
        Destroy(HPotionObj);
        ruiMapGenerator.UpdateTile(HPotionCurrentPos, RuiMapGenerator.MAP_TYPE.GROUND);
        Debug.Log("‰ñ•œƒ|[ƒVƒ‡ƒ“Á‚¦‚½");
    }
}
