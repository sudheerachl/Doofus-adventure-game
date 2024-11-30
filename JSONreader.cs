using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

// JSON Data Structures
[System.Serializable]
public class PlayerData
{
    public float speed;
}

[System.Serializable]
public class PulpitData
{
    public float min_pulpit_destroy_time;
    public float max_pulpit_destroy_time;
    public float pulpit_spawn_time;
}

[System.Serializable]
public class GameData
{
    public PlayerData player_data;
    public PulpitData pulpit_data;
}

// JSON Reader Script
public class JSONreader : MonoBehaviour
{
    void Start()
    {
        string filePath = Path.Combine(Application.dataPath, "JSONtext.json");

        try
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                GameData gameData = JsonUtility.FromJson<GameData>(jsonString); 

                GameObject cube = GameObject.Find("Cube"); 

                if (cube != null) 
                {
                    SelfDestructingCube selfDestructingCube = cube.GetComponent<SelfDestructingCube>();

                    if (selfDestructingCube != null)
                    {
                        selfDestructingCube.destructionTime = gameData.pulpit_data.min_pulpit_destroy_time; 
                    }
                    else
                    {
                        // Handle missing SelfDestructingCube component (e.g., use a default value)
                        // You could also disable the Cube or take other appropriate action
                    }
                }
                else 
                {
                    // Handle missing Cube GameObject
                    // You could spawn a new Cube, disable other game elements, etc.
                }
            }
            else
            {
                throw new FileNotFoundException("JSON file not found at: " + filePath);
            }
        }
        catch (FileNotFoundException e)
        {
            // Handle the exception, perhaps by displaying an error message to the user or quitting the game
            Debug.LogError(e.Message); 
            // You could also use Application.Quit() here if the JSON file is critical
        }
        catch (System.Exception e)
        {
            // Handle other potential exceptions (e.g., JSON parsing errors)
            Debug.LogError("An error occurred while reading the JSON file: " + e.Message);
        }
    }
}

// Self-Destructing Cube Script
public class SelfDestructingCube : MonoBehaviour
{
    public float destructionTime = 5f; // Default value in case JSON reading fails
    private float timer;
    private TextMeshPro textMeshPro;

    void Start()
    {
        timer = destructionTime;
        textMeshPro = GetComponentInChildren<TextMeshPro>(); 

        if (textMeshPro == null)
        {
            // Handle missing TextMeshPro component
            // You could disable the timer display or take other action
        }
    }

    void Update()
    {
        if (textMeshPro != null) 
        {
            timer -= Time.deltaTime;
            textMeshPro.text = timer.ToString("F1"); 

            if (timer <= 0)
            {
                Destroy(gameObject); 
            }
        }
    }
}