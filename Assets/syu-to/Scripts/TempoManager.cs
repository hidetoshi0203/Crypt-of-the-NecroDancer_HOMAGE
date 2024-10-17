using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TempoManager : MonoBehaviour
{
    public const float MINUTES = 60f;
    [SerializeField] private float startDelay = 1.5f;
    public float StartDelay { get { return startDelay; } }
    [SerializeField] private int bpm = 120;
    private float tempo = 1f;
    public float Tempo { get { return tempo; } private set { tempo = value; } }

    private void Awake()
    {
        Tempo = MINUTES / bpm;
    }
}
