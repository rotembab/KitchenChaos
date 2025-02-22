using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event EventHandler OnGameStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameResumed;
    
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State state;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 60f;
    private bool isGamePaused = false;

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }


    private void Start()
    {
       GameInput.Instance.OnPauseAction += GameInputOnPauseAction;
       GameInput.Instance.OnInteractAction += GameInputOnInteractAction;
    }

    private void GameInputOnInteractAction(object sender, EventArgs e)
    {
        if (state == State.WaitingToStart)
        {
            state = State.CountdownToStart;
            OnGameStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInputOnPauseAction(object sender, EventArgs e)
    {
        ToggleGamePause();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                // waitingToStartTimer -= Time.deltaTime;
                // if (waitingToStartTimer < 0f)
                // {
                //     state = State.CountdownToStart;
                //     OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                // }
                break;
            case State.CountdownToStart: 
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }


    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }
    
    
    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }
    
    public bool IsGameOverActive()
    {
        return state == State.GameOver;
    }


    public float GetGamePlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    public void ToggleGamePause()
    {
        isGamePaused = !isGamePaused;
        Time.timeScale = isGamePaused ? 0 : 1;
        if (isGamePaused)
        {
            OnGamePaused?.Invoke(this, EventArgs.Empty);
            
        }
        else
        {
            OnGameResumed?.Invoke(this, EventArgs.Empty);
        }
    }
    
}
