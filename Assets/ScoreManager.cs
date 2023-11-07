using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    int score;

    private void Start()
    {
        score = 0;
    }

    private void Update()
    {
        scoreText.text = "Score: " + score;
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
}
