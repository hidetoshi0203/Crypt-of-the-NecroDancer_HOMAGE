using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSound : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] AudioClip PlayerAttack;

    public void AttackSound()
    {
        audioSource.PlayOneShot(PlayerAttack);
    }
}