using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkUI : MonoBehaviour
{
    [SerializeField] float blinkSpeed = 1.0f; // �_�ł̑���
    private float blinkTime; // �_�ł̎���
    private Image image;

    RuitoshiPlayer ruiToshiPlayer = null;

    void Start()
    {
        image = this.gameObject.GetComponent<Image>(); // Image�̎擾
    }

    void Update()
    {
        if (ruiToshiPlayer == null)
        {
            GameObject inst = GameObject.FindGameObjectWithTag("Player");
            ruiToshiPlayer = inst.GetComponent<RuitoshiPlayer>();
        }

        if (ruiToshiPlayer.powerUpTimer >= (ruiToshiPlayer.powerUpTimerEnd - 5.0f))
        {
            Debug.Log("5�b�O���");
            image.color = GetImageColorAlpha(image.color); // �֐���ǂ��return�ŋA���Ă���alpha�l��ǂݍ���ł���
        }
    }

    Color GetImageColorAlpha(Color color) // UI��_�ł�����֐�
    {
        blinkTime += Time.deltaTime * blinkSpeed; // �_�ł̎��Ԃ����߂Ă���
        color.a = Mathf.Sin(blinkTime); // color��alpha�l��ύX���ē_�ł����Ă���

        return color; // color��alpha�l�̕ύX��Ԃ��Ă���
    }
}
