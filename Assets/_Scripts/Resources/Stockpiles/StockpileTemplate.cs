using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockpileTemplate : MonoBehaviour, IStockpile
{

    // Not in Interface because it's annoying to assign mesh...
    [SerializeField]
    public GameObject[] visualStockpileItems;
    public List<GameObject> resourceInstancesList { get; set; }
    public int maxStockpileItems { get; set; }
    [Range(0, 8), SerializeField]
    private int currentStockpileItemCount;

    public int CurrentStockpileItemCount 
    { 
        get
        {
            return currentStockpileItemCount;
        }
        set 
        {
            currentStockpileItemCount = value;
            SetCorrectObjectVisible();
        }
    }
    public ResourceType resourceType { get; set; }
    public bool IsAttachedToBuilding { get; set; }    

    event IStockpile.ResourcePlace IStockpile.OnResourcePlaced
    {
        add
        {
            throw new System.NotImplementedException();
        }

        remove
        {
            throw new System.NotImplementedException();
        }
    }

    event IStockpile.ResourcePickup IStockpile.OnResourcePickup
    {
        add
        {
            throw new System.NotImplementedException();
        }

        remove
        {
            throw new System.NotImplementedException();
        }
    }

    public virtual void OnEnable()
    {
        //OnResourcePlaced += ResourcePlaced;
        //OnResourcePickup += ResourcePickedUp;
    }

    public virtual void OnDisable()
    {
        //OnResourcePlaced -= ResourcePlaced;
        //OnResourcePickup -= ResourcePickedUp;
    }
    public virtual void Start()
    {
        resourceInstancesList = new List<GameObject>();
        maxStockpileItems = 8;
        resourceType = ResourceType.None;
        IsAttachedToBuilding = false;
        CurrentStockpileItemCount = 0;
    }
    
    public void ResourcePlaced(GameObject go)
    {
        if (resourceInstancesList.Count < maxStockpileItems)
        {
            resourceInstancesList.Add(go);
            CurrentStockpileItemCount++;
        }
    }

    public virtual void ResourcePickedUp(GameObject go)
    {
        if (resourceInstancesList.Count > 0)
        {
            resourceInstancesList.RemoveAt(CurrentStockpileItemCount - 1);
            CurrentStockpileItemCount++;

        }
    }

    public void SetCorrectObjectVisible()
    {
        foreach (GameObject go in visualStockpileItems)
        {
            go.SetActive(false);
        }
        switch (CurrentStockpileItemCount)
        {
            case 0:

                break;

            case 1:
                visualStockpileItems[0].SetActive(true);
                break;

            case 2:
                visualStockpileItems[0].SetActive(true);
                visualStockpileItems[1].SetActive(true);
                break;
            case 3:
                visualStockpileItems[0].SetActive(true);
                visualStockpileItems[1].SetActive(true);
                visualStockpileItems[2].SetActive(true);
                break;

            case 4:
                visualStockpileItems[0].SetActive(true);
                visualStockpileItems[1].SetActive(true);
                visualStockpileItems[2].SetActive(true);
                visualStockpileItems[3].SetActive(true);
                break;

            case 5:
                visualStockpileItems[0].SetActive(true);
                visualStockpileItems[1].SetActive(true);
                visualStockpileItems[2].SetActive(true);
                visualStockpileItems[3].SetActive(true);
                visualStockpileItems[4].SetActive(true);
                break;

            case 6:
                visualStockpileItems[0].SetActive(true);
                visualStockpileItems[1].SetActive(true);
                visualStockpileItems[2].SetActive(true);
                visualStockpileItems[3].SetActive(true);
                visualStockpileItems[4].SetActive(true);
                visualStockpileItems[5].SetActive(true);
                break;

            case 7:
                visualStockpileItems[0].SetActive(true);
                visualStockpileItems[1].SetActive(true);
                visualStockpileItems[2].SetActive(true);
                visualStockpileItems[3].SetActive(true);
                visualStockpileItems[4].SetActive(true);
                visualStockpileItems[5].SetActive(true);
                visualStockpileItems[6].SetActive(true);
                break;

            case 8:
                visualStockpileItems[0].SetActive(true);
                visualStockpileItems[1].SetActive(true);
                visualStockpileItems[2].SetActive(true);
                visualStockpileItems[3].SetActive(true);
                visualStockpileItems[4].SetActive(true);
                visualStockpileItems[5].SetActive(true);
                visualStockpileItems[6].SetActive(true);
                visualStockpileItems[7].SetActive(true);
                break;
        }
    }
}
