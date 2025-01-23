using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageSound : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] AudioClip PlayerDamage;

    public void DamageSound()
    {
        audioSource.PlayOneShot(PlayerDamage);
    }
}
