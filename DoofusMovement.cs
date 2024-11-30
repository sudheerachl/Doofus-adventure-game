using UnityEngine;

public class DoofusMovement : MonoBehaviour
{
    public float speed = 3f; // You can set this in the Inspector or get it from your JSON

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;
        transform.Translate(movement);

        if (transform.position.y < -10f) // Example: Doofus falls below y = -10
        {
            GameOverUI gameOverUI = FindObjectOfType<GameOverUI>();
            if (gameOverUI != null)
            {
                gameOverUI.ShowGameOver();
            }
        }
    }
    
}
