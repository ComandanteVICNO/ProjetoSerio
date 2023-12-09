using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [Header("References")]
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text timerText;
    public TMP_Text gameoverScoreText;
    public GameObject gameUI;
    public GameObject gameOverUI;
    public VibrationManager vibrationManager;

    [Header("TimeValues")]
    private float startTime;
    public float clockStopTime;
    private float clockPausedTime;
    private int clockPauseTimes;
    public float maxTime;
    public float newElapsedTime;

    [Header("Score Values")]
    int highScore;
    int score;
    int failed;

    [Header("Bool Checks")]
    public bool isTimerRunning = true;
    public bool isGameOver;
    public bool isGamemodeTimed;
    public bool setTimed;

    private void Awake()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        int gamemode = PlayerPrefs.GetInt("Gamemode", 0);
        if(gamemode == 0)
        {
            isGamemodeTimed = false;
        }
        else
        {
            isGamemodeTimed = true;
        }
        
    }
    private void Start()
    {
        vibrationManager = FindAnyObjectByType<VibrationManager>();
        isGameOver = false;
        score = 0;
        failed = 0;
        gameUI.SetActive(true);
        gameOverUI.SetActive(false);
        highScoreText.text = "Highscore: " + highScore; 

    }

    private void Update()
    {
        scoreText.text = "Score: " + score;
        if (isGamemodeTimed)
        {
            ClockCountdown();
        }

        if(setTimed)
        {
            PlayerPrefs.SetInt("Gamemode", 1);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt("Gamemode", 0);
            PlayerPrefs.Save();
        }
    }
    #region ScoreHandling
    public void IncreaseScore()
    {
        score += 1;
        vibrationManager.Vibrate(vibrationManager.correctVibrationTime);
    }

    public void DecreaseScore()
    {
        vibrationManager.WrongVibrate();
        if (isGamemodeTimed)
        {
            if (score == 0) return;
            else
            {
                score -= 1;
            }
        }
        else
        {
            failed += 1;
            if(failed > 3)
            {
                GameOverUI();
            }
            timerText.text = "Fails: " + failed;
        }
        
    }
    #endregion
    #region Clock
    private void ClockCountdown()
    {
        if (isTimerRunning)
        {
            newElapsedTime = maxTime -= Time.deltaTime;
            UpdateTimerText(newElapsedTime);
            if (newElapsedTime <= 0)
            {
                GameOverUI();
            }
        }
    }

    private void UpdateTimerText(float elapsedTime)
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);

        timerText.text = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }

    public void StopClockActivate()
    {
        StartCoroutine(PauseClock(clockStopTime));
    }

    public IEnumerator PauseClock(float stopCooldown)
    {
        isTimerRunning = false;
        yield return new WaitForSeconds(stopCooldown);
        isTimerRunning = true;
        
    }
    #endregion
    #region UIHandling
    void GameOverUI()
    {
        isGameOver = true;
        gameUI.SetActive(false);
        gameOverUI.SetActive(true);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
        Time.timeScale = 0f;
        

    }

    public void RestartGame()
    {
        
        SceneManager.LoadScene("GameScene");
        
    }
    #endregion
}