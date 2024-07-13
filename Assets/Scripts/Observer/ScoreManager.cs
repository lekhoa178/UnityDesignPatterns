using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    TMP_Text scoreText;

    int currentScore = 0;

    private void Awake()
    {
        scoreText = GetComponent<TMP_Text>();
        UpdateScoreText();
    }

    void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = $"Score: {currentScore}";
    }
}