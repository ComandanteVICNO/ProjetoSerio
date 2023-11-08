using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class StopClock : MonoBehaviour
{
    public ScoreManager scoreManager;
    public float stopTime;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }
    private void OnDestroy()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.clockStopTime = stopTime;
        scoreManager.Invoke("StopClockActivate", 0f);
    }

    
}
