using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Vector2Int myPosition;
    public bool aliveItemScript;
    //RuiMapGenerator ruiMapGenerator = null;
    //RuitoshiPlayer ruiToshiPlayer = null;
    //RuiPlayerManager ruiPlayerManager = null;

    //GameObject HPotionObj;
    //GameObject SPotionObj;
    //public bool isAliveHPotion = true; // �񕜃|�[�V���������݂��Ă��邩
    //public bool isAliveSPotion = true; // �U����UP�|�[�V���������݂��Ă��邩

    //public Vector2Int HPotionCurrentPos; // ���݂̉񕜃|�[�V�����̈ʒu
    //public Vector2Int SPotionCurrentPos; // ���݂̍U����UP�|�[�V�����̈ʒu
    //public Vector2Int takenHPotionPos; // �v���C���[���񕜃|�[�V������������ʒu
    //public Vector2Int takenSPotionPos; // �v���C���[���U����UP�|�[�V������������ʒu

    //// Update is called once per frame
    //void Update()
    //{
    //    Debug.Log(takenHPotionPos);
    //    if (ruiMapGenerator == null)
    //    {
    //        GameObject inst = GameObject.FindGameObjectWithTag("MapChip");
    //        ruiMapGenerator = inst.GetComponent<RuiMapGenerator>();
    //    }

    //    if (ruiPlayerManager == null)
    //    {
    //        GameObject inst = GameObject.FindGameObjectWithTag("PlayerManager");
    //        ruiPlayerManager = inst.GetComponent<RuiPlayerManager>();
    //    }

    //    if (ruiToshiPlayer == null)
    //    {
    //        GameObject inst = GameObject.FindGameObjectWithTag("Player");
    //        ruiToshiPlayer = inst.GetComponent<RuitoshiPlayer>();
    //    }
    //    //HPotionObj = GameObject.FindGameObjectWithTag("HealingPotion");
    //    //SPotionObj = GameObject.FindGameObjectWithTag("StrengthPotion");
    //}

    //public void HealingHP() // �v���C���[��HP���񕜂���֐�
    //{
    //    if (ruiPlayerManager.playerHP < 3) // �v���C���[�̗̑͂������Ă���(3HP������������)
    //    {
    //        ruiPlayerManager.playerHP++; // �v���C���[�̗�(HP)���񕜂���
    //    }
    //    //takenHPotionPos = ruiToshiPlayer.playerNextPos;
    //    //if (takenHPotionPos == HPotionCurrentPos)
    //    //{
    //    //    Debug.Log("aaa");
    //    //    if (ruiPlayerManager.playerHP < 3) // �v���C���[�̗̑͂������Ă���(3HP������������)
    //    //    {
    //    //        ruiPlayerManager.playerHP++; // �v���C���[�̗�(HP)���񕜂���
    //    //    }
    //    //    Destroy(HPotionObj);
    //    //    isAliveHPotion = false; // �v���C���[���񕜃|�[�V������������̂ŁA�񕜃|�[�V����������
    //    //}
    //}
}
