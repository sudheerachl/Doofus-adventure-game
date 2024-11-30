using UnityEngine;
using TMPro; // Make sure you have TextMeshPro imported

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("Score TextMeshProUGUI not assigned!");
        }
        else
        {
            UpdateScoreText();
        }
    }

    public void IncrementScore()
    {
        score++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}