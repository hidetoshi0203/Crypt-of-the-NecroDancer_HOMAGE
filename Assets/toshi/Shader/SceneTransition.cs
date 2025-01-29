using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] Material screenTranstionMaterial;
    [SerializeField] float transitontionTime = 1.0f;
    [SerializeField] string propertyName = "_Progress";
    public UnityEvent OnTransitionDone;

    private void Start()
    {
        StartCoroutine(TransitionCoroutine());
    }

    IEnumerator TransitionCoroutine()
    {
        float currentTime = 0f;
        while (currentTime < transitontionTime)
        {
            currentTime += Time.deltaTime;
            screenTranstionMaterial.SetFloat(propertyName, 
                Mathf.Clamp01(currentTime / transitontionTime));
            yield return null;
        }
        OnTransitionDone?.Invoke();
    }
}
