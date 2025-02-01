using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkUI : MonoBehaviour
{
    [SerializeField] float blinkSpeed = 1.0f; // �_�ł̑���
    private float blinkTime; // �_�ł̎���
    private Image image;

    toshiPlayer ToshiPlayer = null;

    CheckAliveScripts checkAliveScripts;
    private GameObject checkAliveObjs;


    void Start()
    {
        image = this.gameObject.GetComponent<Image>(); // Image�̎擾
        checkAliveObjs = GameObject.Find("CheckAliveObjects");
        checkAliveScripts = checkAliveObjs.GetComponent<CheckAliveScripts>();
    }

    void Update()
    {
        if (ToshiPlayer == null && checkAliveScripts.isAliveToshiPlayerScr)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Player");
            ToshiPlayer = inst.GetComponent<toshiPlayer>();
        }

        if (ToshiPlayer.powerUpTimer >= (ToshiPlayer.powerUpTimerEnd - 5.0f))
        {
            image.color = GetImageColorAlpha(image.color); // �֐���ǂ��return�ŋA���Ă���alpha�l��ǂݍ���ł���

        }
    }

    Color GetImageColorAlpha(Color color) // UI��_�ł�����֐�
    {
        blinkTime += Time.deltaTime * blinkSpeed * 5.0f; // �_�ł̎��Ԃ����߂Ă���
        color.a = Mathf.Sin(blinkTime); // color��alpha�l��ύX���ē_�ł����Ă���

        return color; // color��alpha�l�̕ύX��Ԃ��Ă���
    }
}
