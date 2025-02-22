 using System;
 using UnityEngine;
 using UnityEngine.UI;

 public class GamePausedUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ToggleGamePause();
        });
        mainMenuButton.onClick.AddListener(() =>
        {
           Loader.LoadScene(Loader.Scene.MainMenuScene); 
           
        });
        optionsButton.onClick.AddListener(() =>
        {
            SetVisible(false);
            OptionsUI.Instance.SetActive(true, () =>
            {
                SetVisible(true);
            });
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameResumed += GameManager_OnGameResumed;
        SetVisible(false);
    }

    private void GameManager_OnGameResumed(object sender, EventArgs e)
    {
        SetVisible(false);
        
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        SetVisible(true);
    }

    private void SetVisible(bool visible)
    {
        gameObject.SetActive(visible);
        if (visible)
        {
            resumeButton.Select();
        }
    }
}
