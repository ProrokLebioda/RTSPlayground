using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stonecutter : UnitTemplate
{
    bool isCuttingRockFormation;

    private GameObject rockFormationRef;
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

    // Change to cut rockFormation
    private IEnumerator CutRockFormation()
    {
        yield return new WaitForSeconds(1f);
        //Debug.Log("Rock formation cut");

        Collider[] hitColliders = Physics.OverlapSphere(transform.position + new Vector3(0, 0, 1), 2);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<RockFormation>(out RockFormation rockFormation))
            {
                rockFormation.Damage(1);
            }
        }
    }

    //Change to HasRockFormationInRange
    private bool HasRockFormationInRange(Vector3 center, float radius, out Vector3 foundRockFormationPosition)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foundRockFormationPosition = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        float closestRockFormationDistance = float.MaxValue;
        GameObject rockFormationGO = null;
        foreach (var hitCollider in hitColliders)
        {
            var tg = hitCollider.gameObject.GetComponent<RockFormation>();
            if (tg)
            {
                float currentRockFormationDistance = Vector3.Distance(Workplace.transform.position, hitCollider.transform.position);
                if (currentRockFormationDistance < closestRockFormationDistance && !tg.IsInUse)
                {
                    closestRockFormationDistance = currentRockFormationDistance;
                    foundRockFormationPosition = hitCollider.transform.position;
                    rockFormationGO = hitCollider.gameObject;
                }
            }
        }

        if (closestRockFormationDistance < float.MaxValue)
        {
            Debug.Log("Found rock formation. Position: " + foundRockFormationPosition.ToString());
            rockFormationRef = rockFormationGO;
            rockFormationRef.GetComponent<RockFormation>().IsInUse = true;
            return true;
        }

        return false;
    }

    public override void SpawnUnit()
    {
        Health = 1;
        Name = UnitType.Stonecutter.ToString();
        Workplace = null;
        CurrentUnitState = UnitState.Idle;
        IsInBuilding = false;
        IsMoving = false;
        Type = UnitType.Stonecutter;
        MyNavMeshAgent = GetComponent<NavMeshAgent>();

        isCuttingRockFormation = false;

        IUnit.OnUnitSpawned(Type);
    }

    public override void Work()
    {
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
                    else if (!CarriedResource && HasRockFormationInRange(Workplace.transform.position, Workplace.GetComponent<IBuilding>().BuildingRadius, out Vector3 rockFormationPosition))
                    {
                        if (Workplace.GetComponent<IBuilding>().OwnStockpileOut.GetComponent<StockpileTemplate>().CurrentStockpileItemCount < 8)
                        {
                            SetTargetPosition(rockFormationPosition);
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

                    if (!IsMoving && !isCuttingRockFormation)
                    {
                        isCuttingRockFormation = true;

                        StartCoroutine(CutRockFormation());


                    }
                    else if (!IsMoving && isCuttingRockFormation)
                    {
                        ///
                        // This part needs work. Logic behind RockFormation is different.
                        ///
                        if (!rockFormationRef)
                        {
                            // Tree was cut
                            //ChangeUnitState(UnitState.Idle);

                            //pickup stone
                            PickupItem(ResourceType.Stone);
                            ChangeUnitState(UnitState.Idle);
                            isCuttingRockFormation = false;
                        }
                        else
                        {
                            StartCoroutine(CutRockFormation());
                        }
                        //when tree cut
                        //

                    }
                    else if (!IsInBuilding)
                    {
                        float distanceToTargetTree = MyNavMeshAgent.remainingDistance;
                        if (distanceToTargetTree <= 0.3f)
                        {
                            //Debug.Log(gameObject.name + " reached rock formation");
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
}
