using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    RuiMapGenerator ruiMapGenerator = null;
    RuiPlayerManager ruiPlayerManager;

    // Start is called before the first frame update
    void Start()
    {
        ruiPlayerManager = GetComponent<RuiPlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ruiMapGenerator == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("MapChip");
            ruiMapGenerator = inst.GetComponent<RuiMapGenerator>();
        }
    }

    public void HealingHP()
    {
        ruiPlayerManager.playerHP++;
        Debug.Log("‰ñ•œ‚µ‚½");
    }

    public void PlayerPowerUp()
    {
        return;
    }
}
