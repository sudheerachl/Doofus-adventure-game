using UnityEngine;
using TMPro; // Make sure you have TextMeshPro imported

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverPanel; // Assign the Game Over panel in the Inspector
    public TextMeshProUGUI gameOverText; // Assign the TextMeshProUGUI component within the panel

    void Start()
    {
        if (gameOverPanel == null)
        {
            Debug.LogError("Game Over panel not assigned!");
        }
        else
        {
            gameOverPanel.SetActive(false); // Hide the panel initially
        }

        if (gameOverText == null)
        {
            Debug.LogError("Game Over TextMeshProUGUI not assigned!");
        }
    }

    public void ShowGameOver()
    {
        if (gameOverPanel != null && gameOverText != null)
        {
            gameOverPanel.SetActive(true); // Show the panel
            gameOverText.text = "Game Over"; // Set the text
            Time.timeScale = 0f; // Stop the game
        }
    }
}