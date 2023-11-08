using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text timerText;
    private float startTime;
    public float clockStopTime;
    private float clockPausedTime;
    private int clockPauseTimes;
    
    public bool isTimerRunning = true;

    int score;

    private void Start()
    {
        score = 0;
        clockStopTime = 0;
        clockPauseTimes = 0;
        clockPausedTime = 0;
        startTime = Time.time;
    }

    private void Update()
    {
        scoreText.text = "Score: " + score;
        
        if (isTimerRunning)
        {

            float newElapsedTime = Time.time - startTime - (clockPausedTime*clockPauseTimes);
            UpdateTimerText(newElapsedTime);

        }
    }

    public void IncreaseScore()
    {
        score += 1;
    }

    public void DecreaseScore()
    {
        if (score == 0) return;
        else
        {
            score -= 1;
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
        clockPauseTimes++;
        isTimerRunning = false;
        clockPausedTime = stopCooldown;
        yield return new WaitForSeconds(stopCooldown);

        isTimerRunning = true;
        
    }
}