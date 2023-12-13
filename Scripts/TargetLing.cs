using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLing : MonoBehaviour
{
    //private GameObject previousObject; // Store the previously clicked object
    public GameObject previousObject { get; private set; }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Checking for left mouse click
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject currentObject = hit.collider.gameObject;
                // You've now stored the instance of the clicked object in currentObject
                // You can use currentObject for any further operations or storage

                Debug.Log("Clicked on: " + currentObject.name);

                // Check if the clicked object has a child GameObject named "Marker"
                GameObject marker = currentObject.transform.Find("Marker")?.gameObject;
                if (marker != null)
                {
                    marker.SetActive(true);
                    Debug.Log("Activated Marker on: " + currentObject.name);

                }

                // Deactivate the marker of the previously clicked object
                if (previousObject != null && previousObject != currentObject)
                {
                    GameObject prevMarker = previousObject.transform.Find("Marker")?.gameObject;
                    if (prevMarker != null)
                    {
                        prevMarker.SetActive(false);
                        Debug.Log("Deactivated Marker on: " + previousObject.name);
                    }
                }

                // Update the previously clicked object
                previousObject = currentObject;
            }
        }
    }
}

