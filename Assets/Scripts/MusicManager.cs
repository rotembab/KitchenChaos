using System;
using UnityEngine;



public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }   
    private AudioSource AudioSource;
    private float volume = .3f;
    
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";

    private void Awake()
    {
        Instance = this;
        AudioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, .3f);
        AudioSource.volume = volume;
    }

    public void ChangeVolume()
    {
        this.volume +=0.1f;
        if (volume > 1f)
        {
            volume = 0f;
        }
        AudioSource.volume = volume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }
    public float GetVolume()
    {
        return volume;
    }
}
