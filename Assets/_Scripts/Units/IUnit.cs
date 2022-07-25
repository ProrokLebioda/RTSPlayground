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
    UnitType Type { get; set; }
    bool TakesAccomodation { get; }
    bool IsInBuilding { get; set; }
    Vector3 TargetPosition { get; set; }

    GameObject Workplace { get; set; }

    GameObject CarriedResource { get; set; }

    public abstract void SpawnUnit();
    public abstract void RemoveUnit();

    /// <summary>
    /// Basic work loop for unit
    /// </summary>
    public abstract void Work();
    public abstract void ChangeUnitState(UnitState state);

    public abstract void SetTargetPosition(Vector3 targetPosition);

    public abstract void MoveUnitToPosition(Vector3 position);
}
