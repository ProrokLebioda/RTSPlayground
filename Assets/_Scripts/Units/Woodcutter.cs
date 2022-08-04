using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Woodcutter : UnitTemplate
{
    bool isCuttingTree;

    private GameObject treeRef;
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
                        GoToWorkplace();
                    }
                    else if (!CarriedResource && HasTreesInRange(Workplace.transform.position, Workplace.GetComponent<IBuilding>().BuildingRadius,out Vector3 treePosition))
                    {
                        if (Workplace.GetComponent<IBuilding>().OwnStockpileOut.GetComponent<WoodStockpile>().CurrentStockpileItemCount < 8)
                        {
                            SetTargetPosition(treePosition);
                            ChangeUnitState(UnitState.Work);
                            IsMoving = true;
                            MyNavMeshAgent.isStopped = false;
                        }
                    }
                    else if (CarriedResource)
                    {
                        IResource resource = CarriedResource.GetComponent<IResource>();
                        if (resource != null)
                        {
                            //resource.UseResource();
                            Workplace.GetComponent<IBuilding>().OwnStockpileOut.GetComponent<IStockpile>().ResourcePlaced(CarriedResource);
                            CarriedResource.transform.parent = null;
                            CarriedResource = null;
                        }
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

                    if (!IsMoving && !isCuttingTree)
                    {
                        isCuttingTree = true;

                        StartCoroutine(CutDownTree());


                    }
                    else if (!IsMoving && isCuttingTree)
                    {
                        if (!treeRef)
                        {
                            // Tree was cut
                            //ChangeUnitState(UnitState.Idle);

                            //pickup tree
                            PickupItem(ResourceType.Wood);
                            ChangeUnitState(UnitState.Idle);
                            isCuttingTree = false;
                        }
                        else
                        {
                            StartCoroutine(CutDownTree());
                        }
                        //when tree cut
                        //

                    }
                    else if (!IsInBuilding)
                    { 
                        float distanceToTargetTree = MyNavMeshAgent.remainingDistance;
                        if (distanceToTargetTree <= 0.3f)
                        {
                            //Debug.Log(gameObject.name + " reached tree");
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
        //Debug.Log("Tree cut");
               
        Collider[] hitColliders = Physics.OverlapSphere(transform.position+new Vector3(0,0,1), 2);
        foreach (var hitCollider in hitColliders)
        {            
            if (hitCollider.TryGetComponent<TreeGrowing>(out TreeGrowing tree))
            {
                tree.Damage(1);
            }
        }            
    }

    private bool HasTreesInRange(Vector3 center, float radius, out Vector3 foundTreePosition)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foundTreePosition = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        float closestTreeDistance = float.MaxValue;
        float currentTreeDistance = float.MaxValue;
        GameObject treeGO = null;
        foreach (var hitCollider in hitColliders)
        {
            var tg = hitCollider.gameObject.GetComponent<TreeGrowing>();
            if (tg)
            {
                currentTreeDistance = Vector3.Distance(Workplace.transform.position, hitCollider.transform.position);
                if (currentTreeDistance < closestTreeDistance && !tg.IsInUse)
                {
                    closestTreeDistance = currentTreeDistance;
                    foundTreePosition = hitCollider.transform.position;
                    treeGO = hitCollider.gameObject;
                }
            }
        }

        if (closestTreeDistance < float.MaxValue)
        { 
            Debug.Log("Found tree. Position: " + foundTreePosition.ToString());
            treeRef = treeGO;
            treeRef.GetComponent<TreeGrowing>().IsInUse = true;
            return true;
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

        isCuttingTree = false;

        IUnit.OnUnitSpawned(Type);
    }

    public override void Work()
    {

    }    
}
