using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreDisplay;
    private int score;

    private void Awake()
    {
        score = 0;
    }

    public void UpdateScore(int pointValue)
    {
        score += pointValue;
        ScoreDisplay.text ="Score: " + score;
    } 
}
