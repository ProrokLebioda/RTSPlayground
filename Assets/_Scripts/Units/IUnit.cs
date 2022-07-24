using UnityEngine;

public delegate void UnitSpawn(UnitType type);
public delegate void UnitRemove(UnitType type);
public interface IUnit
{
    public static UnitSpawn OnUnitSpawned;
    public static UnitRemove OnUnitRemoved;
    string Name { get; set; }
    float Health { get; set; }
    UnitState CurrentUnitState { get; set; }
    UnitType Type { get; }
    bool TakesAccomodation { get; }
    bool IsInBuilding { get; set; }
    Vector3 TargetPosition { get; set; }

    GameObject Workplace { get; set; }

    GameObject carriedResource { get; set; }

    public void SpawnUnit();
    public void RemoveUnit();

    /// <summary>
    /// Basic work loop for unit
    /// </summary>
    public void Work();
    public void ChangeUnitState(UnitState state);

    public void SetTargetPosition(Vector3 targetPosition);

    public void MoveUnitToPosition(Vector3 position);
}
