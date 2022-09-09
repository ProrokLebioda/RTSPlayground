using UnityEngine;

public delegate void Built(UnityEngine.GameObject building);
public delegate void Placed(UnityEngine.GameObject building);
public interface IBuilding
{

    public static Built OnBuilt;
    public static Placed OnPlaced;
    string Name { get; set; }
    float Health { get; set; }
    bool IsBuilt { get; set; }
    bool IsOccupied { get; set; }

    // Amount of Input Stockpiles
    int InStockpiles { get; set; }

    GameObject OwnStockpileOut { get; set; }

    //Amount of Output Stockpiles
    int OutStockpiles { get; set; }
    ConstructionCosts ConstructionCost { get; set; }
    GameObject Entrance { get; set; }

    float BuildingRadius { get; set; }

    UnitType WantedUnitType { get; set; }


    public abstract void Place();
    public abstract void Build();
    public abstract void FinishedBuilding();
}


public struct ConstructionCosts
{
    public ConstructionCosts(int _wood, int _stone)
    {
        Wood = _wood;
        Stone = _stone;
    }

    public int Wood { get; set; }
    public int Stone { get; set; }
}