using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    private TargetLing targetLing;

    private GameObject currentTarget;

    void Start()
    {
        // Assuming TargetLing script is attached to the same GameObject
        targetLing = GetComponent<TargetLing>();
    }

    void Update()
    {
        if (currentTarget != null)
        {
            if (currentTarget != targetLing.previousObject)
            {
                currentTarget = targetLing.previousObject;
            }
        }
        else
        {
            currentTarget = targetLing.previousObject;
        }
    }

    public void ActivateBuildButton()
    {
        Debug.Log("ActivateBuildButton()");

        if (currentTarget != null)
        {
            SpawnBridge spawnBridge = currentTarget.GetComponent<SpawnBridge>();
            if (spawnBridge != null)
            {
                spawnBridge.BuildButton();
            }
        }
    }

    public void ActivateDigButton()
    {
        Debug.Log("ActivateDigButton()");

        if (currentTarget != null)
        {
            CharacterMovement characterMovement = currentTarget.GetComponent<CharacterMovement>();
            if (characterMovement != null)
            {
                characterMovement.Digging();
            }
        }
    }
}
