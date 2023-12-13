using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBridge : MonoBehaviour
{
    public GameObject bridgeSpawnLocation;
    public GameObject bridge;
    public int NumOfBoxes;
    public float spacing = 0.85f;
    Quaternion rotation = Quaternion.Euler(-60, -90, 90);
    Quaternion straight = Quaternion.Euler(-90, 0, 0);

    [SerializeField] 
    private CharacterMovement characterMovement;

    private Coroutine buildCoroutine;

    private void Start()
    {
        
    }

    public void BuildButton()
    {
        Debug.Log("Pre if true loop: " + characterMovement.IsBuilding());
        if (characterMovement.IsBuilding())
        {
            Debug.Log("end building");
            Debug.Log("in if true loop: " + characterMovement.IsBuilding());
            StopBuilding();
            characterMovement.Building();
        }
        else
        {
            Debug.Log("start building");
            Debug.Log("in if false loop: " + characterMovement.IsBuilding());
            Debug.Log("GameObject Active: " + gameObject.activeSelf);
            buildCoroutine = StartCoroutine(BuildBridge());

            characterMovement.Building();
        }
        Debug.Log("-----------------------------------------");
        
    }

    IEnumerator BuildBridge()
    {
        Vector3 spawnLocation = bridgeSpawnLocation.transform.position;

        for (int i = 0; i <= NumOfBoxes; i++)
        {

            Vector3 position = new Vector3(i * spacing, i * spacing * Mathf.Tan(Mathf.Deg2Rad * 30), 0);
            GameObject bridgePart = Instantiate(bridge, spawnLocation + position, rotation, null);
            
            
            yield return new WaitForSeconds(0.18f); // Adjust the time delay between each step
        }
    }

    public void StopBuilding()
    {
        if (buildCoroutine != null)
        {
            StopCoroutine(buildCoroutine);
            //isBuilding = false;
        }
    }
}