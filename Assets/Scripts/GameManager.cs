using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameResumed;
    public static GameManager Instance { get; private set; }
 private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePLaying,
        GameOver,
    }

    private State state;
    private float countdownToStartTimer = 3f;
    private bool isGamePaused = false;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 120f;

    private void Start()
    {
        GameInput.Instance.OnPauseAction += OnPauseAction;
        GameInput.Instance.OnInteractAction += OnInteractAction;
    }

    private void OnInteractAction(object sender, EventArgs e)
    {
        if(state == State.WaitingToStart)
        {
            state = State.CountdownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Awake()
    {
            Instance= this;
            state = State.WaitingToStart;
        
    }
    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
              
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePLaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;
            case State.GamePLaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:

                break;
            default:
                break;
        }
        Debug.Log(state);
    }
    public bool isCountDownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public bool isGamePlaying()
    {
        return state == State.GamePLaying;
    }
    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;    
    }

    
    public bool isGameOver()
    {
        return state == State.GameOver;
    }

    public float getPlayingTimerNormalized()
    {
        return gamePlayingTimer / gamePlayingTimerMax;
    }

    public void TogglePauseGame()
    {    
       isGamePaused = !isGamePaused;
        if(isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameResumed?.Invoke(this, EventArgs.Empty);
        }

  
    }

}
