using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodcutterHut : MonoBehaviour, IBuilding
{
    public string Name { get; set; }
    public float Health { get; set; }
    public bool IsBuilt { get; set; }
    public bool IsOccupied { get; set; }
    public int InStockpiles { get; set; }
    public int OutStockpiles { get; set; }
    public ConstructionCosts ConstructionCost { get; set; }

    public UnitType WantedUnitType => UnitType.Woodcutter;

    private void OnEnable()
    {
        
    }


    public void Build()
    {

        // Change when mechanics for building buildings from resources introduced
        IsBuilt = true;
        Name = "Woodcutter's Hut";
        Health = 1;
        IsOccupied = false;
        InStockpiles = 1;
        OutStockpiles = 1;
        
        IBuilding.OnBuilt(gameObject);
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        ConstructionCost = new ConstructionCosts(2, 1);

        yield return new WaitForSeconds(0.1f);
        Build();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
