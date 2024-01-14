using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class OptionsDataManager : MonoBehaviour
{
    public bool tutorial;
    public float effectsVolume;
    public float volume;

    public Tutorial tutorialState;

    public AudioOptionsManager audioOptionsManager;


    // Start is called before the first frame update
    private void Start()
    {
        string fileName = "Options.json";
        string filePath = Path.Combine(Application.dataPath, fileName);

        if (!File.Exists(filePath))
        {
            MakeDefaultSaveData(true, 0.5f, 0.5f);
        }

        if (SceneManager.GetActiveScene() != null && SceneManager.GetActiveScene().name == "OptionsMenu")
        {
            GameObject tutorialObject = GameObject.Find("GameManager");
            tutorialState = tutorialObject.GetComponent<Tutorial>();
            tutorialState.LoadTutorialStatus();
            return;
        }

        if (SceneManager.GetActiveScene() != null && SceneManager.GetActiveScene().name != "Menu")
        {
            GameObject tutorialWindow = GameObject.Find("TutorialWindow");
            tutorialState = tutorialWindow.GetComponent<Tutorial>();
        }
        
    }



    public void SaveToFile()
    {
        (float musicVolume, float soundEffectsVolume) = audioOptionsManager.ReturnVolume();

        volume = musicVolume;
        effectsVolume = soundEffectsVolume;
        tutorial = tutorialState.CheckTutorialStatus();

        SaveData saveData = new SaveData(tutorial, effectsVolume, volume);

        // Convert the object to JSON
        string json = JsonUtility.ToJson(saveData);


        // Specify the file path with the LevelNumber in the filename
        string fileName = "Options.json";
        string filePath = Path.Combine(Application.dataPath, fileName);

        // Write the JSON data to the file
        File.WriteAllText(filePath, json);

        Debug.Log("Options saved to: " + filePath + "with status: " + tutorial);
    }

    public void MakeDefaultSaveData(bool tutorial, float effectVolume, float volume)
    {
        SaveData saveData = new SaveData(tutorial, effectVolume, volume);

        // Convert the object to JSON
        string json = JsonUtility.ToJson(saveData);


        // Specify the file path with the LevelNumber in the filename
        string fileName = "Options.json";
        string filePath = Path.Combine(Application.dataPath, fileName);

        // Write the JSON data to the file
        File.WriteAllText(filePath, json);

        Debug.Log("Options saved to: " + filePath);
    }

    public void LoadFromFile()
    {
        string fileName = $"Options.json";
        string filePath = Path.Combine(Application.dataPath, fileName);

        // Check if the file exists before attempting to read
        if (File.Exists(filePath))
        {
            // Read the JSON data from the file
            string json = File.ReadAllText(filePath);

            // Convert the JSON data back to a SaveData object
            SaveData loadedData = JsonUtility.FromJson<SaveData>(json);

            // Set the loaded data to the variables
            tutorial = loadedData.tutorial;
            effectsVolume = loadedData.effectVolume;
            volume = loadedData.volume;

            Debug.Log($"Options loaded from: {filePath}");
        }
        else
        {
            Debug.LogWarning($"File not found: {filePath}");
        }
    }

    public (float, float) LoadVolume()
    {
        return (volume, effectsVolume);
    }

    public bool TutorialStatus() {  return tutorial; }


    [System.Serializable]
    public class SaveData
    {
        public bool tutorial;
        public float effectVolume;
        public float volume;

        public SaveData(bool tState, float eV, float V)
        {
            tutorial = tState;
            effectVolume = eV;
            volume = V;
        }
    }
}
