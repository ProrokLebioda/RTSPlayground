using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MonoBehaviour, IUnit
{
    public string Name { get; set; }
    public float Health { get; set; }
    public UnitType Type => UnitType.Carrier;

    [SerializeField]
    private GameObject itemCarried;

    public bool TakesAccomodation => true;
    
    public GameObject Workplace { get; set; }

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

    public void SpawnUnit()
    {
        Health = 1;
        Name = "Carrier";
        itemCarried = null;
        Workplace = null;

        IUnit.OnUnitSpawned(Type);
    }

    public void RemoveUnit()
    {
        IUnit.OnUnitRemoved(Type);
        Destroy(this);
    }
}