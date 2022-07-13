using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodcutter : MonoBehaviour, IUnit
{
    public string Name { get; set; }
    public float Health { get; set; }

    public UnitType Type => UnitType.Woodcutter;

    public bool TakesAccomodation => false;

    [SerializeField]
    private GameObject Workplace;



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

    public void RemoveUnit()
    {
        IUnit.OnUnitRemoved(Type);
        Destroy(this);
    }

    public void SpawnUnit()
    {
        Health = 1;
        Name = "Woodcutter";
        Workplace = null;
        
        IUnit.OnUnitSpawned(Type);
    }
}
