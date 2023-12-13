using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpawnManager : MonoBehaviour
{
    public GameObject ling;
    public Transform spawnLocation;
    public DataManager dataManager;

    public int initialOrderInLayer = 0;
    private SortingGroup sortingGroup;

    public void SpawnLing()
    {
        if (dataManager.LingCount() < dataManager.maxLings)
        {
            GameObject Zergling = Instantiate(ling, spawnLocation.position, Quaternion.identity);
            sortingGroup = Zergling.GetComponent<SortingGroup>();
            sortingGroup.sortingOrder = initialOrderInLayer;

            dataManager.AddActiveLing();
        }
        
    }
}