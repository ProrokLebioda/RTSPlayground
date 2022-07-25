using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Carrier : UnitTemplate
{
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

    public override void SpawnUnit()
    {
        Health = 1;
        Name = "Carrier";
        CarriedResource = null;
        Workplace = null;
        CurrentUnitState = UnitState.Idle;
        Type = UnitType.Carrier;
        MyNavMeshAgent = GetComponent<NavMeshAgent>();

        IUnit.OnUnitSpawned(Type);
    }
}
