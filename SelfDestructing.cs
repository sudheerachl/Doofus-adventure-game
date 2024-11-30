using UnityEngine;
using TMPro;

public class SelfDestructing : MonoBehaviour
{
    public GameObject prefabToSpawn; 

    private float spawnTimer = 2.5f;
    private float destructionTime;
    private float timer;
    private TextMeshProUGUI timerText;
    private GameObject spawnedPrefab;
    private bool scoreIncremented = false;



    void Start()
    {
        if (prefabToSpawn == null)
        {
            Debug.LogError("Prefab to spawn is not assigned!");
            return; 
        }

        

        timerText = GetComponentInChildren<TextMeshProUGUI>();

        if (timerText == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on a child of this GameObject!");
        }

        // Start the destruction timer with a random value between 4 and 5 seconds
        destructionTime = Random.Range(4f, 5f);
        timer = destructionTime;
    }

    void Update()
    {
        if (spawnedPrefab == null) 
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                SpawnPrefab();
            }
        }

        if (timerText != null)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("F1");

            if (timer <= 0)
            {
                Destroy(gameObject); 
            }
        }

         // Flag to track if score has been incremented

    }

    void SpawnPrefab()
    {
        // Choose a random direction (0: right, 1: left, 2: front, 3: back)
        int randomDirection = Random.Range(0, 4);

        // Calculate spawn offset based on the chosen direction and the cube's scale
        Vector3 spawnOffset;
        switch (randomDirection)
        {
            case 0: // Right
                spawnOffset = transform.right * (transform.localScale.x / 2f + 0.5f);
                break;
            case 1: // Left
                spawnOffset = -transform.right * (transform.localScale.x / 2f + 0.5f);
                break;
            case 2: // Front
                spawnOffset = transform.forward * (transform.localScale.z / 2f + 0.5f);
                break;
            case 3: // Back
                spawnOffset = -transform.forward * (transform.localScale.z / 2f + 0.5f);
                break;
            default:
                spawnOffset = Vector3.zero; 
                break;
        }

        Vector3 spawnPosition = transform.position + spawnOffset;
        spawnedPrefab = Instantiate(prefabToSpawn, spawnPosition, transform.rotation);
    }
    
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !scoreIncremented) 
            {
                FindObjectOfType<ScoreUI>().IncrementScore(); 
                scoreIncremented = true; // Set the flag to true after incrementing
            }
        }
}