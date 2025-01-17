using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    RuiMapGenerator ruiMapGenerator = null;
    RuiPlayerManager ruiPlayerManager = null;

    GameObject HPotionObj;
    GameObject SPotionObj;
    public bool isAliveHPotion = true; // �񕜃|�[�V���������݂��Ă��邩
    public bool isAliveSPotion = true; // �U����UP�|�[�V���������݂��Ă��邩

    public Vector2Int HPotionCurrentPos; // ���݂̉񕜃|�[�V�����̈ʒu
    public Vector2Int SPotionCurrentPos; // ���݂̍U����UP�|�[�V�����̈ʒu
    public Vector2Int takenHPotionPos; // �v���C���[���񕜃|�[�V������������ʒu
    public Vector2Int takenSPotionPos; // �v���C���[���U����UP�|�[�V������������ʒu

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

    public void HealingHP() // �v���C���[��HP���񕜂���֐�
    {
        if (ruiPlayerManager.playerHP < 3) // �v���C���[�̗̑͂������Ă���(3HP������������)
        {
            ruiPlayerManager.playerHP++; // �v���C���[�̗�(HP)���񕜂���
        }
        Destroy(HPotionObj);
        isAliveHPotion = false; // �v���C���[���񕜃|�[�V������������̂ŁA�񕜃|�[�V����������
    }
}
