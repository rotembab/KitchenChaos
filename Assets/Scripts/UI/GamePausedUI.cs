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
            OptionsUI.Instance.SetActive(true);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameResumed += GameManager_OnGameResumed;
        setVisible(false);
    }

    private void GameManager_OnGameResumed(object sender, EventArgs e)
    {
        setVisible(false);
        
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        setVisible(true);
    }

    private void setVisible(bool visible)
    {
        gameObject.SetActive(visible);
    }
}
