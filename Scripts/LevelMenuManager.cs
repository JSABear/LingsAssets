using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenuManager : MonoBehaviour
{
    public DataManager dataManager;

    public TextMeshProUGUI[] LevelSurvivedText;
    public TextMeshProUGUI[] LevelTimeText;

    public int LevelCount;

    private int[] survicedLings;
    private int[] maxSurvicedLings;
    private int[] recordTime;

    private void Start()
    {
        
        survicedLings = new int[LevelCount];
        maxSurvicedLings = new int[LevelCount];
        recordTime = new int[LevelCount];

        for (int i = 1; i <= LevelCount; i++)
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
        for (int i = 0; i < LevelCount; i++)
        {
            if (maxSurvicedLings[i] != 0)
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
    }

    public void LevelSelect(int levelNumber)
    {
        SceneManager.LoadScene($"Level{levelNumber}");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void CreateSaveData(int level)
    {
        int time = 60;
        int current = 0;
        int max = 5;
        
        dataManager.MakeDefaultSaveData(level, time, current, max);
    }

}

