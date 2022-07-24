using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Woodcutter : MonoBehaviour, IUnit
{
    public string Name { get; set; }
    public float Health { get; set; }
    public UnitState CurrentUnitState { get; set; }

    public UnitType Type => UnitType.Woodcutter;

    public bool TakesAccomodation => false;

    [SerializeField]
    public GameObject Workplace { get; set; }

    public Vector3 TargetPosition { get; set; }
    public NavMeshAgent MyNavMeshAgent;
    public GameObject carriedResource { get; set; }
    public bool IsInBuilding { get; set; }

    ///


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

        if (Workplace)
        {
            switch (CurrentUnitState)
            {
                case UnitState.Idle:
                    // Go to workplace, this state is usually when first converted to unit type
                    if (!IsInBuilding)
                    {
                        SetTargetPosition(Workplace.GetComponent<IBuilding>().Entrance.transform.position);
                        ChangeUnitState(UnitState.Move);
                    }
                    else if (HasTreesInRange(Workplace.transform.position, Workplace.GetComponent<IBuilding>().BuildingRadius,out Vector3 treePosition))
                    {
                        SetTargetPosition(treePosition);
                        ChangeUnitState(UnitState.Work);
                        
                    }
                    break;

                case UnitState.Move:
                    if (IsInBuilding)
                    {
                        ChangeUnitState(UnitState.Idle);
                        //GetComponent<CapsuleCollider>().enabled = true;
                        //GetComponent<NavMeshAgent>().enabled = true;
                        
                        //MoveUnitToPosition(TargetPosition);
                        
                    }
                    else
                    {
                        MoveUnitToPosition(TargetPosition);
                    }

                    break;
                case UnitState.Work:
                    MoveUnitToPosition(TargetPosition);
                    break;

                default:
                    break;
            }
        }
    }

    private bool HasTreesInRange(Vector3 center, float radius, out Vector3 foundTreePosition)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            foundTreePosition = hitCollider.transform.position;
            Debug.Log("Found tree. Position: " + foundTreePosition.ToString());
            return true;
        }
        foundTreePosition = new Vector3();
        return false;
    }

    public void SpawnUnit()
    {
        Health = 1;
        Name = "Woodcutter";
        Workplace = null;
        CurrentUnitState = UnitState.Idle;
        IsInBuilding = false;
        MyNavMeshAgent = GetComponent<NavMeshAgent>();

        IUnit.OnUnitSpawned(Type);
    }

    public void RemoveUnit()
    {
        IUnit.OnUnitRemoved(Type);
        Destroy(this);
    }


    public void Work()
    {

    }

    public void ChangeUnitState(UnitState state)
    {
        CurrentUnitState = state;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        TargetPosition = targetPosition;
    }

    public void MoveUnitToPosition(Vector3 position)
    {
        MyNavMeshAgent.SetDestination(position);
    }
}
