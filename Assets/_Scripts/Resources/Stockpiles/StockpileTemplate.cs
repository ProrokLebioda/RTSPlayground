using System;
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
    public GameObject prefabOnStockpile;

    [SerializeField]
    public ResourceType resourceType;
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
        //resourceType = ResourceType.None;
        IsAttachedToBuilding = false;
        CurrentStockpileItemCount = 0;
        if (CurrentStockpileItemCount > 0)
            InstantiateResourcesOnStockpile();
    }

    public virtual void InstantiateResourcesOnStockpile()
    {
        for(int i = 0; i < CurrentStockpileItemCount; i++)
        {
            GameObject go = Instantiate(prefabOnStockpile, this.gameObject.transform.position, Quaternion.Euler(0f, -45f, 0f));
            go.GetComponent<MeshRenderer>().enabled = false;
            resourceInstancesList.Add(go);
        }
    }

    public void ResourcePlaced(GameObject go)
    {
        if (resourceInstancesList.Count < maxStockpileItems)
        {
            resourceInstancesList.Add(go);
            IResource tg = go.GetComponent<IResource>();
            tg.IsInUse = false;
            CurrentStockpileItemCount++;
        }
    }

    public virtual bool ResourcePickedUp(out GameObject go)
    {
        if (resourceInstancesList.Count > 0)
        {
            go = resourceInstancesList[CurrentStockpileItemCount-1];
            resourceInstancesList.RemoveAt(CurrentStockpileItemCount - 1);
            IResource tg = go.GetComponent<IResource>();
            tg.IsInUse = true;
            CurrentStockpileItemCount--;
            return true;

        }
        else 
        {
            //we don't want that...
            go = null;
            return false;
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
