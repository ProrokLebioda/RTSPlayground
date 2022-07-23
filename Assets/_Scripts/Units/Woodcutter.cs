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
                    // Go to workplace

                    MoveUnitToPosition(Workplace.transform.position);
                    ChangeUnitState(UnitState.Move);
                    break;

                case UnitState.Move:

                    break;
                case UnitState.Work:
                    
                    break;

                default:
                    break;
            }
        }
    }
    public void SpawnUnit()
    {
        Health = 1;
        Name = "Woodcutter";
        Workplace = null;
        CurrentUnitState = UnitState.Idle;

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
