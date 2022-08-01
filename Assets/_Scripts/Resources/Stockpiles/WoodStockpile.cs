using System;
using System.Collections.Generic;
using UnityEngine;

public delegate void ResourcePlace(GameObject go);
public delegate void ResourcePickup(GameObject go);

public class WoodStockpile : MonoBehaviour
{
    public GameObject[] woodlogs;
    public List<GameObject> woodlogsList = new List<GameObject>();
    private int maxStockpileItems = 8;

    [Range(0, 8), SerializeField]
    private int currentStockpileItemCount;

    public int CurrentStockpileItemCount
    {
        get { return currentStockpileItemCount; }
        set 
        {
            currentStockpileItemCount = value;
            SetCorrectObjectVisible();
        }
    }

    public ResourceType resourceType => ResourceType.Wood;

    public static ResourcePlace OnResourcePlaced;
    public static ResourcePickup OnResourcePickup;

    public bool IsAttachedToBuilding = true;
    // Start is called before the first frame update

    private void OnEnable()
    {
        OnResourcePlaced += ResourcePlaced;
        OnResourcePickup += ResourcePickedUp;
    }


    private void OnDisable()
    {
        OnResourcePlaced -= ResourcePlaced;
        OnResourcePickup -= ResourcePickedUp;
    }
    private void ResourcePickedUp(GameObject go)
    {
        if (woodlogsList.Count > 0)
        {
            woodlogsList.RemoveAt(CurrentStockpileItemCount - 1);
            CurrentStockpileItemCount++;
            
        }
    }

    private void ResourcePlaced(GameObject go)
    {
        if(woodlogsList.Count < 8)
        {
            woodlogsList.Add(go);
            CurrentStockpileItemCount++;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    private void SetCorrectObjectVisible()
    {
        foreach (GameObject go in woodlogs)
        {
            go.SetActive(false);
        }
        switch (CurrentStockpileItemCount)
        {
            case 0:

                break;

            case 1:
                woodlogs[0].SetActive(true);
                break;

            case 2:
                woodlogs[0].SetActive(true);
                woodlogs[1].SetActive(true);
                break;
            case 3:
                woodlogs[0].SetActive(true);
                woodlogs[1].SetActive(true);
                woodlogs[2].SetActive(true);
                break;

            case 4:
                woodlogs[0].SetActive(true);
                woodlogs[1].SetActive(true);
                woodlogs[2].SetActive(true);
                woodlogs[3].SetActive(true);
                break;

            case 5:
                woodlogs[0].SetActive(true);
                woodlogs[1].SetActive(true);
                woodlogs[2].SetActive(true);
                woodlogs[3].SetActive(true);
                woodlogs[4].SetActive(true);
                break;

            case 6:
                woodlogs[0].SetActive(true);
                woodlogs[1].SetActive(true);
                woodlogs[2].SetActive(true);
                woodlogs[3].SetActive(true);
                woodlogs[4].SetActive(true);
                woodlogs[5].SetActive(true);
                break;

            case 7:
                woodlogs[0].SetActive(true);
                woodlogs[1].SetActive(true);
                woodlogs[2].SetActive(true);
                woodlogs[3].SetActive(true);
                woodlogs[4].SetActive(true);
                woodlogs[5].SetActive(true);
                woodlogs[6].SetActive(true);
                break;

            case 8:
                woodlogs[0].SetActive(true);
                woodlogs[1].SetActive(true);
                woodlogs[2].SetActive(true);
                woodlogs[3].SetActive(true);
                woodlogs[4].SetActive(true);
                woodlogs[5].SetActive(true);
                woodlogs[6].SetActive(true);
                woodlogs[7].SetActive(true);
                break;
        }
    }
}
