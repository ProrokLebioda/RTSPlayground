using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronStockpile : StockpileTemplate
{
    public override void Start()
    {
        resourceInstancesList = new List<GameObject>();
        maxStockpileItems = 8;
        resourceType = ResourceType.Iron;
        IsAttachedToBuilding = true;
        CurrentStockpileItemCount = 4;
    }
}
