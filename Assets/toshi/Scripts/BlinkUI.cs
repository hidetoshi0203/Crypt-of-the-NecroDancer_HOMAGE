using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkUI : MonoBehaviour
{
    [SerializeField] float blinkSpeed = 1.0f; // 点滅の速さ
    private float blinkTime; // 点滅の時間
    private Image image;

    toshiPlayer ToshiPlayer = null;

    CheckAliveScripts checkAliveScripts;
    private GameObject checkAliveObjs;


    void Start()
    {
        image = this.gameObject.GetComponent<Image>(); // Imageの取得
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
            image.color = GetImageColorAlpha(image.color); // 関数を読んでreturnで帰ってきたalpha値を読み込んでいる

        }
    }

    Color GetImageColorAlpha(Color color) // UIを点滅させる関数
    {
        blinkTime += Time.deltaTime * blinkSpeed * 5.0f; // 点滅の時間を決めている
        color.a = Mathf.Sin(blinkTime); // colorのalpha値を変更して点滅させている

        return color; // colorのalpha値の変更を返している
    }
}
