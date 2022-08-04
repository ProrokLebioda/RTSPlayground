using System;
using System.Collections.Generic;
using UnityEngine;

public class WoodStockpile : StockpileTemplate
{
    public override void Start()
    {
        resourceInstancesList = new List<GameObject>();
        maxStockpileItems = 8;
        resourceType = ResourceType.Wood;
        IsAttachedToBuilding = true;
        CurrentStockpileItemCount = 0;
    }
}
