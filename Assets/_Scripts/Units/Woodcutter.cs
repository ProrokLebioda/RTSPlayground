using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Woodcutter : UnitTemplate
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

        if (Workplace)
        {
            switch (CurrentUnitState)
            {
                case UnitState.Idle:
                    IsMoving = false;
                    // Go to workplace, this state is usually when first converted to unit type
                    if (!IsInBuilding)
                    {
                        SetTargetPosition(Workplace.GetComponent<IBuilding>().Entrance.transform.position);
                        ChangeUnitState(UnitState.Move);
                        IsMoving = true;
                    }
                    else if (!CarriedResource && HasTreesInRange(Workplace.transform.position, Workplace.GetComponent<IBuilding>().BuildingRadius,out Vector3 treePosition))
                    {
                        SetTargetPosition(treePosition);
                        ChangeUnitState(UnitState.Work);
                        IsMoving = true;

                    }
                    break;

                case UnitState.Move:
                    if (IsInBuilding)
                    {
                        ChangeUnitState(UnitState.Idle);
                        IsMoving = true;
                        //GetComponent<CapsuleCollider>().enabled = true;
                        //GetComponent<NavMeshAgent>().enabled = true;
                        
                        //MoveUnitToPosition(TargetPosition);
                        
                    }
                    else
                    {
                        MoveUnitToPosition(TargetPosition);
                        IsMoving = true;
                    }

                    break;
                case UnitState.Work:
                    MoveUnitToPosition(TargetPosition);
                    
                    if (!IsMoving)
                    {
                        Debug.Log(gameObject.name + " works now");
                        StartCoroutine(CutDownTree());
                        
                    }
                    else if (!IsInBuilding)
                    { 
                        float distanceToTargetTree = MyNavMeshAgent.remainingDistance;
                        if (distanceToTargetTree <= 0.3f)
                        {
                            Debug.Log(gameObject.name + " reached tree");
                            IsMoving = false;
                            MyNavMeshAgent.isStopped = true;
                        }
                    }
                    break;

                default:
                    break;
            }
        }
    }

    private IEnumerator CutDownTree()
    {
        yield return new WaitForSeconds(1f);        
        Debug.Log("Tree cut");
        ChangeUnitState(UnitState.Idle);
        MyNavMeshAgent.isStopped = false;
    }

    private bool HasTreesInRange(Vector3 center, float radius, out Vector3 foundTreePosition)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foundTreePosition = new Vector3(float.MinValue, float.MinValue, float.MinValue);

        foreach (var hitCollider in hitColliders)
        {
            var tg = hitCollider.gameObject.GetComponent<TreeGrowing>();
            if (tg)
            {
                foundTreePosition = hitCollider.transform.position;
                Debug.Log("Found tree. Position: " + foundTreePosition.ToString());
                return true;
            }
        }
        
        return false;
    }

    public override void SpawnUnit()
    {
        Health = 1;
        Name = "Woodcutter";
        Workplace = null;
        CurrentUnitState = UnitState.Idle;
        IsInBuilding = false;
        IsMoving = false;
        Type = UnitType.Woodcutter;
        MyNavMeshAgent = GetComponent<NavMeshAgent>();

        IUnit.OnUnitSpawned(Type);
    }

    public override void Work()
    {

    }    
}
