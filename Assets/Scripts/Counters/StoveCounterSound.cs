using System;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    
    [SerializeField] private StoveCounter stoveCounter; 
    
    private AudioSource audioSource;
    private float warningTimerMax = 0.2f;
    private float warningSoundTimer;
    private bool playWarningSound;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStoveCounterStateChanged += StoveCounter_OnStoveCounterStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.ProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = 0.5f;
        playWarningSound = stoveCounter.IsFried() &&  e.progressNormalized >= burnShowProgressAmount;

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


    private void Update()
    {
        if (playWarningSound)
        {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer < 0f)
            {
                warningSoundTimer = warningTimerMax;
                
                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }
}
