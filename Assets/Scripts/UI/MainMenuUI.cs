using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    [SerializeField] private Button quitBtn;

    private void Awake()
    {
        playBtn.onClick.AddListener(() =>
        {
           Loader.LoadScene(Loader.Scene.GameScene);
        });
        quitBtn.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        Time.timeScale = 1f;
    }


}
