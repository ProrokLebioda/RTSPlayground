using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerUnitsManager;

    private void OnEnable()
    {
        IBuilding.OnBuilt += BuildingCreated;

        IResource.OnCreated += IncreaseResource;
        IResource.OnUsed += DecreaseResource;

        IUnit.OnUnitSpawned += AddedUnit;
        IUnit.OnUnitRemoved += RemoveUnit;
    }

    private void OnDisable()
    {
        IBuilding.OnBuilt -= BuildingCreated;

        IResource.OnCreated -= IncreaseResource;
        IResource.OnUsed -= DecreaseResource;

        IUnit.OnUnitSpawned -= AddedUnit;
        IUnit.OnUnitRemoved -= RemoveUnit;
    }

    private void BuildingCreated(GameObject building)
    {
        Debug.Log("Go find me someone for " + building.name);
        FindFreeLaborerForBuilding(building);
    }

    private void IncreaseResource(ResourceType resType)
    {
        Debug.Log("Created " + resType.ToString() + " resource");
        ResourceManager.Instance().Wood++;
    }
    private void DecreaseResource(ResourceType resType)
    {
        Debug.Log("Used resource" + resType.ToString());
        ResourceManager.Instance().Wood--;
    }

    private void AddedUnit(UnitType type)
    {
        Debug.Log("Unit spawned: " + type.ToString());
    }
    private void RemoveUnit(UnitType type)
    {
        Debug.Log("Unit removed: " + type.ToString());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool FindFreeLaborerForBuilding(GameObject building)
    {
        return playerUnitsManager.GetComponent<PlayerUnitManager>().FindFreeLaborerForBuilding(building); ;
    }
}
