using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private Button backButton;


    private void Awake()
    {
        Instance = this;
        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisuals();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisuals();
        });
        backButton.onClick.AddListener(() =>
        {
            SetActive(false);
        });
    }

    private void Start()
    {
        UpdateVisuals();
        SetActive(false);
        GameManager.Instance.OnGameResumed += GameManager_OnGameResumed;
    }

    private void GameManager_OnGameResumed(object sender, EventArgs e)
    {
        SetActive(false);
    }


    private void UpdateVisuals()
    {
        soundEffectsText.text = "Sound Effects : " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music : " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
    }


    public void SetActive(bool isActive)
    {
     gameObject.SetActive(isActive);   
    }

}
