using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RandomImage : MonoBehaviour
{
    public Image displayImage; // 表示するImageコンポーネント
    public Sprite[] images; // 画像リスト
    public float interval = 3f; // 画像の切り替え間隔
    public CanvasGroup fadeCanvasGroup; // フェード用のCanvasGroup

    private int lastIndex = -1; // 前回の画像のインデックス

    void Start()
    {
        StartCoroutine(ChangeImageRoutine());
    }

    IEnumerator ChangeImageRoutine()
    {
        while (true)
        {
            yield return StartCoroutine(FadeOut()); // フェードアウト
            ChangeImage(); // 画像変更
            yield return StartCoroutine(FadeIn()); // フェードイン
            yield return new WaitForSeconds(interval); // 次の変更まで待機
        }
    }

    void ChangeImage()
    {
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, images.Length);
        } while (randomIndex == lastIndex); // 前回と同じなら再選択

        lastIndex = randomIndex; // 選ばれたインデックスを保存
        displayImage.sprite = images[randomIndex];
    }

    IEnumerator FadeOut()
    {
        float duration = 0.5f;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            fadeCanvasGroup.alpha = 1 - (t / duration);
            yield return null;
        }
        fadeCanvasGroup.alpha = 0;
    }

    IEnumerator FadeIn()
    {
        float duration = 0.5f;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            fadeCanvasGroup.alpha = t / duration;
            yield return null;
        }
        fadeCanvasGroup.alpha = 1;
    }
}
