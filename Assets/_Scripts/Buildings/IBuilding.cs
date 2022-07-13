public delegate void Built(string name);

public interface IBuilding
{

    public static Built OnBuilt;
    string Name { get; set; }
    float Health { get; set; }
    bool IsBuilt { get; set; }
    bool IsOccupied { get; set; }

    // Amount of Input Stockpiles
    int InStockpiles { get; set; }

    //Amount of Output Stockpiles
    int OutStockpiles { get; set; }
    ConstructionCosts ConstructionCost { get; set; }

    public void Build();
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