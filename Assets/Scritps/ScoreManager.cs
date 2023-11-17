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

    [Header("Bool Checks")]
    public bool isTimerRunning = true;

    int score;
    int highScore;
    private void Awake()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
    private void Start()
    {
        vibrationManager = FindAnyObjectByType<VibrationManager>();
        score = 0;
        gameUI.SetActive(true);
        gameOverUI.SetActive(false);
        highScoreText.text = "Highscore: " + highScore; 

    }

    private void Update()
    {
        scoreText.text = "Score: " + score;
        ClockCountdown();


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
        if (score == 0) return;
        else
        {
            
            score -= 1;
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