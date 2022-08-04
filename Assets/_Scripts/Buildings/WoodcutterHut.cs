using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodcutterHut : BuildingTemplate
{

    
    private void OnEnable()
    {
        
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        ConstructionCost = new ConstructionCosts(2, 1);

        yield return new WaitForSeconds(0.1f);
        Build();
        OwnStockpileOut = transform.Find("WoodStockpile").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void Build()
    {

        // Change when mechanics for building buildings from resources introduced
        IsBuilt = true;
        Name = "Woodcutter's Hut";
        Health = 1;
        IsOccupied = false;
        InStockpiles = 1;
        OutStockpiles = 1;
        Entrance = transform.Find("Entrance").gameObject;
        BuildingRadius = 100.0f;
        WantedUnitType = UnitType.Woodcutter;


        IBuilding.OnBuilt(gameObject);
    }


}
