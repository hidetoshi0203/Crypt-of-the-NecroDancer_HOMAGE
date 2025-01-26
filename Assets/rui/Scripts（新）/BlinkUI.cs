using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkUI : MonoBehaviour
{
    [SerializeField] float blinkSpeed = 1.0f; // 点滅の速さ
    private float blinkTime; // 点滅の時間
    private Image image;

    RuitoshiPlayer ruiToshiPlayer = null;

    void Start()
    {
        image = this.gameObject.GetComponent<Image>(); // Imageの取得
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
            Debug.Log("5秒前やで");
            image.color = GetImageColorAlpha(image.color); // 関数を読んでreturnで帰ってきたalpha値を読み込んでいる
        }
    }

    Color GetImageColorAlpha(Color color) // UIを点滅させる関数
    {
        blinkTime += Time.deltaTime * blinkSpeed; // 点滅の時間を決めている
        color.a = Mathf.Sin(blinkTime); // colorのalpha値を変更して点滅させている

        return color; // colorのalpha値の変更を返している
    }
}
