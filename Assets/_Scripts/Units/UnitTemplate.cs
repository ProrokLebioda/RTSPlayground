using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTemplate : MonoBehaviour, IUnit
{
    public string Name { get; set; }
    public float Health { get; set; }
    public UnitType Type { get; set; }
    public bool TakesAccomodation => true;
    public GameObject Workplace { get; set; }

    public UnitState CurrentUnitState { get; set; }

    public Vector3 TargetPosition { get; set; }
    public UnityEngine.AI.NavMeshAgent MyNavMeshAgent;
    public GameObject CarriedResource { get; set; }
    public bool IsInBuilding { get; set; }
    public bool IsMoving { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        SpawnUnit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
            RemoveUnit();
    }

    public virtual void SpawnUnit()
    {
        Health = 1;
        Name = "None";
        Workplace = null;
        CurrentUnitState = UnitState.Idle;
        Type = UnitType.None;

        MyNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        IUnit.OnUnitSpawned(Type);
    }

    public virtual void RemoveUnit()
    {
        IUnit.OnUnitRemoved(Type);
        Destroy(this);
    }

    public virtual void Work()
    {

    }

    public virtual void ChangeUnitState(UnitState state)
    {
        CurrentUnitState = state;
    }

    public virtual void SetTargetPosition(Vector3 targetPosition)
    {
        TargetPosition = targetPosition;
    }

    public virtual void MoveUnitToPosition(Vector3 position)
    {
        MyNavMeshAgent.SetDestination(position);
    }
}

