using System;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    
    [SerializeField] private StoveCounter stoveCounter; 
    
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStoveCounterStateChanged += StoveCounter_OnStoveCounterStateChanged;
    }

    private void StoveCounter_OnStoveCounterStateChanged(object sender, StoveCounter.StoveCounterEventArgs e)
    {
        if (e.state != StoveCounter.State.Idle)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
