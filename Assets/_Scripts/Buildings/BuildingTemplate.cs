using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTemplate : MonoBehaviour, IBuilding
{
    public string Name { get; set; }
    public float Health { get; set; }
    public bool IsBuilt { get; set; }
    public bool IsOccupied { get; set; }
    public int InStockpiles { get; set; }
    public int OutStockpiles { get; set; }
    public ConstructionCosts ConstructionCost { get; set; }
    public UnitType WantedUnitType { get; set; }
    public GameObject Entrance { get; set; }
    public float BuildingRadius { get; set; }
    public GameObject OwnStockpileOut { get; set; }

    private void OnEnable()
    {
        
    }

    public virtual void Place()
    {
        IBuilding.OnPlaced(gameObject);
    }

    public virtual void Build()
    {
        IsBuilt = false;
        if (IsBuilt)
            IBuilding.OnBuilt(gameObject);
    }

    public virtual void FinishedBuilding()
    {
        if (IsBuilt)
            IBuilding.OnBuilt(gameObject);
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
