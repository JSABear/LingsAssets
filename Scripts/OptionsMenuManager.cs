using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuManager : MonoBehaviour
{

    public OptionsDataManager optionsDataManager;


    public void CloseOptions()
    {
        optionsDataManager.SaveToFile();

        SceneManager.LoadScene("Menu");
    }
}
