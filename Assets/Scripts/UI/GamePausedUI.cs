 using System;
 using UnityEngine;

public class GamePausedUI : MonoBehaviour
{
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
