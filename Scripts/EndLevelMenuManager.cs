using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelMenuManager : MonoBehaviour
{
    public DataManager dataManager;

    public void ChangeLevel()
    {
        SceneManager.LoadScene($"Level{dataManager.levelNumber+1}");
    }

    public void LevelMenu()
    {
        SceneManager.LoadScene("LevelMenu");
    }

    public void FinalLevel()
    {
        SceneManager.LoadScene("Menu");
    }
}
