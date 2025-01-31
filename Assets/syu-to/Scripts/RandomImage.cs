using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RandomImage : MonoBehaviour
{
    public Image displayImage; // �\������Image�R���|�[�l���g
    public Sprite[] images; // �摜���X�g
    public float interval = 3f; // �摜�̐؂�ւ��Ԋu
    public CanvasGroup fadeCanvasGroup; // �t�F�[�h�p��CanvasGroup

    private int lastIndex = -1; // �O��̉摜�̃C���f�b�N�X

    void Start()
    {
        StartCoroutine(ChangeImageRoutine());
    }

    IEnumerator ChangeImageRoutine()
    {
        while (true)
        {
            yield return StartCoroutine(FadeOut()); // �t�F�[�h�A�E�g
            ChangeImage(); // �摜�ύX
            yield return StartCoroutine(FadeIn()); // �t�F�[�h�C��
            yield return new WaitForSeconds(interval); // ���̕ύX�܂őҋ@
        }
    }

    void ChangeImage()
    {
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, images.Length);
        } while (randomIndex == lastIndex); // �O��Ɠ����Ȃ�đI��

        lastIndex = randomIndex; // �I�΂ꂽ�C���f�b�N�X��ۑ�
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
