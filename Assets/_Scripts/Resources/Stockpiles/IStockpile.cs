using System;
using System.Collections.Generic;
using UnityEngine;


public interface IStockpile
{
    public delegate void ResourcePlace(object source, EventArgs Args);
    public delegate void ResourcePickup(object source, EventArgs Args);

    public event ResourcePlace OnResourcePlaced;
    public event ResourcePickup OnResourcePickup;

    public List<GameObject> resourceInstancesList { get; set; }
    public int maxStockpileItems { get; set; }

    public int CurrentStockpileItemCount { get; set; }

    public ResourceType resourceType { get; set; }
    public bool IsAttachedToBuilding { get; set; }

    public abstract void ResourcePickedUp(GameObject go);
    public abstract void ResourcePlaced(GameObject go);
    public abstract void SetCorrectObjectVisible();

}
