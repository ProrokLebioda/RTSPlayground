using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Carrier : UnitTemplate
{

    public GameObject StartObject;
    public GameObject EndObject;


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

        Work();
    }

    public override void SpawnUnit()
    {
        Health = 1;
        Name = "Carrier";
        CarriedResource = null;
        Workplace = null;
        CurrentUnitState = UnitState.Idle;
        Type = UnitType.Carrier;
        MyNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        IUnit.OnUnitSpawned(Type);
    }

    public void SetStartDestination(GameObject startObject)
    {
        StartObject = startObject;
    }

    public void SetEndDestination(GameObject endObject)
    {
        EndObject = endObject;
    }

    public override void Work()
    {
        if (!CarriedResource && StartObject)
        {
            // go to start object
            MoveUnitToPosition(StartObject.transform.position);
            if (Vector3.Distance(this.gameObject.transform.position, StartObject.transform.position) < 3.1f)
            {
                //PickupItem(ResourceType.Any);
                PickupItemFromStockpile(StartObject);
            }
        }
        else if (CarriedResource && EndObject)
        {
            // carry to destination
            MoveUnitToPosition(EndObject.transform.position);
            if (Vector3.Distance(this.gameObject.transform.position, EndObject.transform.position) < 3.1f)
            {
                PlaceItem(EndObject);
            }
        }
    }
}
