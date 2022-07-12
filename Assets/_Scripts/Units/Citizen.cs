using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour, IUnit
{
    public string Name { get; set; }
    public float Health { get; set; }
    public UnitType Type { get; set; }

    public bool TakesAccomodation => true;

    // Start is called before the first frame update
    void Start()
    {
        SpawnUnit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnUnit()
    {
        Type = UnitType.Citizen;

        IUnit.OnUnitSpawned(Type);
    }

    public void RemoveUnit()
    {
        IUnit.OnUnitRemoved(Type);
        Destroy(this);
    }
}
