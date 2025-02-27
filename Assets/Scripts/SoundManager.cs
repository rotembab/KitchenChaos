using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_SOUND_EFFECT_VOLUME = "SoundEffectsVolume";
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    
    
    public static SoundManager Instance { get; private set; }
    private float volume = 1f;
    private void Awake()
    {
        Instance = this;
        volume =  PlayerPrefs.GetFloat(PLAYER_SOUND_EFFECT_VOLUME, 1f);
    }
    
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
       // Player.Instance.OnPickup += Player_OnPickup;
        BaseCounter.OnAnyObjectDrop += BaseCounter_OnAnyObjectDrop;
        TrashCounter.OnAnyThrowToTrash += TrashCounter_OnAnyThrowToTrash;
    }

    private void TrashCounter_OnAnyThrowToTrash(object sender, EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash,trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectDrop(object sender, EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop,baseCounter.transform.position);
    }

    private void Player_OnPickup(object sender, EventArgs e)
    { 
       // PlaySound(audioClipRefsSO.objectPickup,Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as   CuttingCounter;
        PlaySound(audioClipRefsSO.chop,cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance; 
        PlaySound(audioClipRefsSO.deliveryFailed,deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance; 
        PlaySound(audioClipRefsSO.deliverySuccess,deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip audioClip,Vector3 position,float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip,position,volumeMultiplier * volume);
    }
    
    
    private void PlaySound(AudioClip[] audioClipArray,Vector3 position,float volumeMultiplier = 1f)
    {
        PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)],position,volumeMultiplier * volume);
    }
    
    public void PlayFootstepSound(Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipRefsSO.footstep,position,  volume);
    }


    public void PlayCountdownSound()
    {
        PlaySound(audioClipRefsSO.warning,Vector3.zero);
    }
    
    public void ChangeVolume()
    {
        this.volume +=0.1f;
        if (volume > 1f)
        {
            volume = 0f;
        }
        PlayerPrefs.SetFloat(PLAYER_SOUND_EFFECT_VOLUME,volume);
        PlayerPrefs.Save();
    }
    public float GetVolume()
    {
        return volume;
    }
    

}
