using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;

public class DataManager : MonoBehaviour
{
    //Ingame values
    public int timeOnLevel;
    public int maxLings;
    public int levelNumber;

    private int activeLings;
    private int deadLings;
    private int survivedLings;
    private int endTime;
    private float startTime;

    //Loaded values
    [HideInInspector]
    public int RecordTime;
    [HideInInspector]
    public int RecordLings;
    [HideInInspector]
    public int RecordMaxLings;

    //UI elements
    public TextMeshProUGUI lingCountText;
    public TextMeshProUGUI timeOnLevelText;
    public TextMeshProUGUI survivedLingsText;
    public TextMeshProUGUI endTimeText;
    public GameObject levelCompletePanel;

    //GameState
    private bool levelOver = false;
    


    private void Start()
    {
        // Check if the current scene is LevelMenu, and if so, do not proceed with the rest of the Start function
        if (SceneManager.GetActiveScene() != null && SceneManager.GetActiveScene().name == "LevelMenu" || SceneManager.GetActiveScene().name == "OptionsMenu")
        {
            return;
        }

        LoadFromFile(levelNumber);
        startTime = Time.time;
        InvokeRepeating(nameof(UpdateTimeOnLevel), 1f, 1f);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene() != null && SceneManager.GetActiveScene().name == "LevelMenu")
        {
            return;
        }
        // Update TextMeshPro texts
        UpdateLingCountText();
        UpdateTimeOnLevelText();

        if (!levelOver && survivedLings + deadLings == maxLings)
        {
            LevelComplete();

            if(RecordLings < survivedLings && RecordTime > endTime || RecordTime == 0 )
            {
                SaveToFile();
            }

            levelOver = true;
        }

    }

    private void UpdateTimeOnLevel()
    {
        timeOnLevel = Mathf.RoundToInt(Time.time - startTime);
    }

    private void OnDestroy()
    {
        CancelInvoke(nameof(UpdateTimeOnLevel));
    }

    public void AddActiveLing() { activeLings++; }

    public void RemoveActiveLing() { activeLings--; }

    public int LingCount() { return activeLings; }

    public void AddDeadLing() { deadLings++; }

    public void AddSurvivedLings() { survivedLings++; }

    public void EndTime()
    {
        endTime = timeOnLevel;
    }

    private void UpdateLingCountText()
    {
        // Check if the TextMeshPro object is assigned
        if (lingCountText != null)
        {
            // Update the text with the activeLings / maxLings information
            lingCountText.text = $"{activeLings} / {maxLings}";
        }
    }

    private void UpdateTimeOnLevelText()
    {
        // Check if the TextMeshPro object is assigned
        if (timeOnLevelText != null)
        {
            // Update the text with the timeOnLevel information
            timeOnLevelText.text = $"Time: {timeOnLevel}s";
        }
    }

    private void LevelComplete()
    {
        CancelInvoke(nameof(UpdateTimeOnLevel));

        EndTime();
        if (survivedLingsText != null)
        {
            survivedLingsText.text = $"Survived: {survivedLings} / {maxLings} ";
        }
        if (endTimeText != null)
        {
            endTimeText.text = $"Time: {endTime}s ";
        }
        levelCompletePanel.SetActive(true);

    }

    public void SaveToFile() 
    {
        SaveData saveData = new SaveData(endTime, survivedLings, maxLings);

        // Convert the object to JSON
        string json = JsonUtility.ToJson(saveData);


        // Specify the file path with the LevelNumber in the filename
        string fileName = $"Level{levelNumber}Data.json";
        string filePath = Path.Combine(Application.dataPath, fileName);

        // Write the JSON data to the file
        File.WriteAllText(filePath, json);

        Debug.Log("Data saved to: " + filePath);
    }

    public void MakeDefaultSaveData(int level, int time, int current, int max)
    {
        SaveData saveData = new SaveData(time, current, max);

        // Convert the object to JSON
        string json = JsonUtility.ToJson(saveData);


        // Specify the file path with the LevelNumber in the filename
        string fileName = $"Level{level}Data.json";
        string filePath = Path.Combine(Application.dataPath, fileName);

        // Write the JSON data to the file
        File.WriteAllText(filePath, json);

        Debug.Log("Data saved to: " + filePath);
    }

    public void LoadFromFile(int loadNumber)
    {
        string fileName = $"Level{loadNumber}Data.json";
        string filePath = Path.Combine(Application.dataPath, fileName);

        // Check if the file exists before attempting to read
        if (File.Exists(filePath))
        {
            // Read the JSON data from the file
            string json = File.ReadAllText(filePath);

            // Convert the JSON data back to a SaveData object
            SaveData loadedData = JsonUtility.FromJson<SaveData>(json);

            // Set the loaded data to the variables
            RecordTime = loadedData.endTime;
            RecordLings = loadedData.survivedLings;
            RecordMaxLings = loadedData.maxLings;

            Debug.Log($"Data loaded from: {filePath}");
        }
        else
        {
            Debug.LogWarning($"File not found: {filePath}");
        }
    }

    [System.Serializable]
    public class SaveData
    {
        public int endTime;
        public int survivedLings;
        public int maxLings;

        public SaveData(int time, int survived, int Lings)
        {
            endTime = time;
            survivedLings = survived;
            maxLings = Lings;
        }
    }
}
