using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public GameObject[] Windows;

    private int activeWindow = 0;

    public bool tutorial = true;

    public OptionsDataManager optionsDataManager;


    void Start()
    {
        if (SceneManager.GetActiveScene() != null && SceneManager.GetActiveScene().name == "OptionsMenu")
        {
            return;
        }
        
        CheckTutorialStatus();
        LoadTutorialStatus();

        GameObject spawner = GameObject.Find("TutorialSpawnerWindow");
        GameObject spawn = GameObject.Find("TutorialSpawnWindow");
        GameObject target = GameObject.Find("TutorialTargetWindow");
        GameObject goal = GameObject.Find("TutorialGoalWindow");
        GameObject dig = GameObject.Find("TutorialDigWindow");
        GameObject build = GameObject.Find("TutorialBuildWindow");
        GameObject start = GameObject.Find("TutorialStartWindow");

        Windows = new GameObject[] { start, goal, spawner, spawn, target, dig, build };

        for (int i = 0; i < Windows.Length; i++)
        {           
            Windows[i].SetActive(false);
        }

        if (tutorial == true)
        {
            start.SetActive(true);
        }
    }

    public void ShowNextWindow()
    {
        GameObject currentWindow = Windows[activeWindow];
        GameObject nextWindow = Windows[activeWindow + 1];

        currentWindow.SetActive(false);
        nextWindow.SetActive(true);

        activeWindow++;

    }

    public void SkipTutorial() 
    {
        for (int i = 0; i < Windows.Length; i++)
        {
            Windows[i].SetActive(false);
        }
        DisableTutorial();
        activeWindow = 0;
    }

    public void StartTutorial()
    {
        Windows[0].SetActive(true);
    }

    public void EnableTutorial()
    {
        tutorial = true;
    }

    public void DisableTutorial()
    {
        tutorial = false;
    }

    public void ToggleTutorial() 
    {
        if(tutorial == true) 
        { 
           tutorial = false; 
        }
        else { tutorial = true; }
    }

    public bool CheckTutorialStatus() 
    {
        return tutorial;
    }

    public void LoadTutorialStatus()
    {
        optionsDataManager.LoadFromFile();
        tutorial = optionsDataManager.tutorial;
    }
}
