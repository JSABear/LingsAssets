using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenuManager : MonoBehaviour
{
    public DataManager dataManager;

    public TextMeshProUGUI[] LevelSurvivedText;
    public TextMeshProUGUI[] LevelTimeText;

    private int[] survicedLings;
    private int[] maxSurvicedLings;
    private int[] recordTime;

    private void Start()
    {
        survicedLings = new int[4];
        maxSurvicedLings = new int[4];
        recordTime = new int[4];

        for (int i = 1; i <= 4; i++)
        {
            dataManager.LoadFromFile(i);
            survicedLings[i - 1] = dataManager.RecordLings;
            maxSurvicedLings[i - 1] = dataManager.RecordMaxLings;
            recordTime[i - 1] = dataManager.RecordTime;
        }

        ShowRecords();
    }

    private void ShowRecords()
    {
        for (int i = 0; i < 4; i++)
        {
            if (LevelSurvivedText[i] != null)
            {
                // Update the text with the activeLings / maxLings information
                LevelSurvivedText[i].text = $"Survived: {survicedLings[i]} / {maxSurvicedLings[i]}";
            }
            if (LevelTimeText[i] != null)
            {
                // Update the text with the timeOnLevel information
                LevelTimeText[i].text = $"Time: {recordTime[i]}s";
            }
        }
    }

    public void LevelSelect(int levelNumber)
    {
        SceneManager.LoadScene($"Level{levelNumber}");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

