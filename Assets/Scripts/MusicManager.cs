using System;
using UnityEngine;



public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }   
    private AudioSource AudioSource;
    private float volume = .3f;

    private void Awake()
    {
        Instance = this;
        AudioSource = GetComponent<AudioSource>();
    }

    public void ChangeVolume()
    {
        this.volume +=0.1f;
        if (volume > 1f)
        {
            volume = 0f;
        }
        AudioSource.volume = volume;
    }
    public float GetVolume()
    {
        return volume;
    }
}
