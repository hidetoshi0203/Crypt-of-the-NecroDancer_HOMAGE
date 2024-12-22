using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public enum State // �m�[�h�̏��
    {
        None, // �������Ȃ�
        Open, // �m�[�h��Open�ɂ��A�����cost��estimatedCost��score�����߂�
        Closed, // Open�����m�[�h�����
    }

    [SerializeField] private GameObject enemy = null; // �GGameObject
    [SerializeField] private GameObject player = null; // �v���C���[GameObject
    private float enemyPosX; // �G��X���W
    private float enemyPosY; // �G��Y���W

    private float cost = -1f; // ���ۂ̃R�X�g(�G�̈ړ���)
    private float estimatedCost; // ����R�X�g(�G���v���C���[�ɒǂ����܂ł̐���̈ړ���)
    private float score; // (cost��estimatedCost�𑫂�����)

    void Start()
    {
        
    }

    void Update()
    {
        if (enemy == null)
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy");
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void CalculateScore() // cost,estimatedCost,score�����߂�
    {
        float estimatedCostX;
        float estimatedCostY;

        cost++; // �G�̓}�b�v��1�}�X�������̂�+1����

        estimatedCostX = player.transform.position.x - enemy.transform.position.x; // X���W�̐���R�X�g�����߂�
        estimatedCostY = player.transform.position.y - enemy.transform.position.y; // Y���W�̐���R�X�g�����߂�
        estimatedCost = estimatedCostX + estimatedCostY; // ����R�X�g��X���W��Y���W�����ꂼ�ꑫ���ċ��߂�

        score = cost + estimatedCost; // score��cost��estimatedCost�𑫂��ċ��߂�
    }
}
