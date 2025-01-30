using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBGM : MonoBehaviour
{
    public AudioClip[] bgmClips;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayRandomBGM();
    }

    void PlayRandomBGM()
    {
        if (bgmClips.Length == 0) return;

        int randomIndex = Random.Range(0, bgmClips.Length);
        audioSource.clip = bgmClips[randomIndex];
        audioSource.Play();
    }
}