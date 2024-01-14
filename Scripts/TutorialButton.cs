using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public Image imageToChangeColor; // Reference to the image you want to change color
    public Tutorial tutorialScript; // Reference to the Tutorial script
    public Color falseColor = Color.red; // Color when tutorial is true
    public Color trueColor = Color.green; // Color when tutorial is false

    void Start()
    {
        // Find and reference the Tutorial script in the scene
        tutorialScript = FindObjectOfType<Tutorial>();

        Debug.Log(tutorialScript.tutorial);

        if (tutorialScript == null)
        {
            Debug.LogError("Tutorial script not found in the scene.");
            return;
        }
    }

    private void Update()
    {
        if(tutorialScript.tutorial == true) 
        {
            imageToChangeColor.color = trueColor;
        }
        else
        {
            imageToChangeColor.color = falseColor;
        }
    }

    void ChangeColor()
    {
        // Check if the image reference is not null
        if (imageToChangeColor != null)
        {
            // Change the color based on the tutorial boolean
            imageToChangeColor.color = tutorialScript.tutorial ? falseColor : trueColor;
        }
        else
        {
            Debug.LogError("Image reference is null. Assign an Image component to 'imageToChangeColor'.");
        }
    }


}

