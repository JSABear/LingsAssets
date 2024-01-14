using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    private TargetLing targetLing;

    private GameObject currentTarget;

    public GameObject targetGameObject;

    void Start()
    {
        targetLing = GetComponent<TargetLing>();
    }

    void Update()
    {
        ToggleGameObjectVisibility(targetGameObject);

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

    public void ToggleGameObjectVisibility(GameObject objectToToggle)
    {
        if (objectToToggle != null)
        {
            // Access the walking direction from TargetLing
            int walkingDirection = targetLing.WalkingDirection;

            // Toggle the visibility of the GameObject based on the walking direction
            if (walkingDirection == 1)
            {
                // Character is facing right, make the GameObject visible
                objectToToggle.SetActive(false);
            }
            else
            {
                // Character is facing left, make the GameObject invisible
                objectToToggle.SetActive(true);
            }
        }
    }

    }
