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

    private void OnEnable()
    {
        Build();
    }


    public void Build()
    {

        for (int i = 0; i < 1000000; i++)
        {

        }
        IsBuilt = true;
        if (IsBuilt)
            IBuilding.OnBuilt("Woodcutter Hut");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
