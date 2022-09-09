using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTemplate : MonoBehaviour, IUnit
{
    public string Name { get; set; }
    public int Health { get; set; }
    public UnitType Type { get; set; }
    public bool TakesAccomodation => true;
    public GameObject Workplace { get; set; }

    public UnitState CurrentUnitState { get; set; }

    public Vector3 TargetPosition { get; set; }
    public UnityEngine.AI.NavMeshAgent MyNavMeshAgent;
    public GameObject CarriedResource { get; set; }
    public bool IsInBuilding { get; set; }
    public bool IsMoving { get; set; }

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

    public virtual void SpawnUnit()
    {
        Health = 1;
        Name = "None";
        Workplace = null;
        CurrentUnitState = UnitState.Idle;
        Type = UnitType.None;

        MyNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        IUnit.OnUnitSpawned(Type);
    }

    public virtual void RemoveUnit()
    {
        IUnit.OnUnitRemoved(Type);
        Destroy(this);
    }

    public virtual void Work()
    {

    }

    public virtual void ChangeUnitState(UnitState state)
    {
        CurrentUnitState = state;
    }

    public virtual void SetTargetPosition(Vector3 targetPosition)
    {
        TargetPosition = targetPosition;
    }

    public virtual void MoveUnitToPosition(Vector3 position)
    {
        MyNavMeshAgent.SetDestination(position);
    }

    public virtual void PickupItemFromStockpile(GameObject stockpileSource)
    {
        IStockpile stockpile = stockpileSource.GetComponent<IStockpile>();
        if (stockpile != null)
        {
            GameObject go = new();
            Debug.Log("Attempt to pickup item from " + stockpileSource.name);
            if (stockpile.ResourcePickedUp(out go))
            {
                Debug.Log("Item picked up");
                CarriedResource = go;
                CarriedResource.transform.position = this.gameObject.transform.position;
                CarriedResource.transform.position += new Vector3(0, 1f, 0); 

                if (!CarriedResource.GetComponent<MeshRenderer>().enabled)
                    CarriedResource.GetComponent<MeshRenderer>().enabled = true;
                CarriedResource.transform.parent = this.gameObject.transform;
            }
            else
            {
                Debug.Log("Item pickup failed");
            }
        }
    }

    public virtual void PickupItem(ResourceType resourceType)
    {
        //pickup item, find closest 
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f);
                
        foreach (var hitCollider in hitColliders)
        {
            IResource tg = hitCollider.gameObject.GetComponent<IResource>();
            if ((tg != null && !tg.IsInUse) && (resourceType == tg.resourceType || resourceType == ResourceType.Any))
            {
                hitCollider.transform.position += new Vector3(0, 1f, 0); 
                hitCollider.transform.parent = this.transform;
                hitCollider.transform.localRotation = Quaternion.identity;
                tg.IsInUse = true;
                CarriedResource = hitCollider.gameObject;
                return;
            }
        }
    }

    /// <summary>
    /// Place object on Stockpile
    /// </summary>
    /// <param name="destinationObject"></param>
    public virtual void PlaceItem(GameObject destinationObject)
    {
        if (CarriedResource)
        {
            IStockpile stockpile = destinationObject.GetComponent<IStockpile>();
            
            stockpile?.ResourcePlaced(CarriedResource);
            CarriedResource.GetComponent<MeshRenderer>().enabled = false;
            CarriedResource.transform.parent = null;
            CarriedResource = null;
        }
    }

    /// <summary>
    /// Place on position, in case there is no valid object to place
    /// </summary>
    /// <param name="destinationPosition"></param>
    public virtual void PlaceItem(Vector3 destinationPosition)
    {
        CarriedResource.transform.parent = null;
        CarriedResource = null;
    }

    public virtual void GoToWorkplace()
    {
        SetTargetPosition(Workplace.GetComponent<IBuilding>().Entrance.transform.position);
        ChangeUnitState(UnitState.Move);
        IsMoving = true;
        MyNavMeshAgent.isStopped = false;
    }
}

