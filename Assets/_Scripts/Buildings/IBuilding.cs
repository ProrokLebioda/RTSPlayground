public delegate void Built(string name);

public interface IBuilding
{

    public static Built OnBuilt;
    string Name { get; set; }
    float Health { get; set; }
    bool IsBuilt { get; set; }
    bool IsOccupied { get; set; }
    int InStockpiles { get; set; }
    int OutStockpiles { get; set; }
    ConstructionCosts ConstructionCost { get; set; }

    public void Build();
}


public struct ConstructionCosts
{
    ConstructionCosts(int _wood, int _stone)
    {
        Wood = _wood;
        Stone = _stone;
    }

    int Wood { get; set; }
    int Stone { get; set; }
}