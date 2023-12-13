using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionPanelManager : MonoBehaviour
{
    public GameObject optionsPanel;
    public bool panelStatus = false;


    public void ShowOptionsMenu()
    {
        if (!panelStatus)
        {
            optionsPanel.SetActive(true);
            panelStatus = true;
        }
        else
        {
            optionsPanel.SetActive(false);
            panelStatus = false;
        }
        
    }

    public void RestartButton(int levelNum)
    {
        SceneManager.LoadScene($"Level{levelNum}");
    }

    public void QuitButton()
    {
        SceneManager.LoadScene("Menu");
    }

}
