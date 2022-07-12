public delegate void UnitSpawn(UnitType type);
public delegate void UnitRemove(UnitType type);
public interface IUnit
{
    public static UnitSpawn OnUnitSpawned;
    public static UnitRemove OnUnitRemoved;
    string Name { get; set; }
    float Health { get; set; }
    UnitType Type { get; set; }
    bool TakesAccomodation { get; }

    public void SpawnUnit();
    public void RemoveUnit();
}
