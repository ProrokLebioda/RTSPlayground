using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private void OnEnable()
    {
        IBuilding.OnBuilt += FindCollonizer;

        IResource.OnCreated += IncreaseResource;
        IResource.OnUsed += DecreaseResource;

        IUnit.OnUnitSpawned += AddedUnit;
        IUnit.OnUnitRemoved += RemoveUnit;
    }

    private void OnDisable()
    {
        IBuilding.OnBuilt -= FindCollonizer;

        IResource.OnCreated -= IncreaseResource;
        IResource.OnUsed -= DecreaseResource;

        IUnit.OnUnitSpawned -= AddedUnit;
        IUnit.OnUnitRemoved -= RemoveUnit;
    }

    private void FindCollonizer(string name)
    {
        Debug.Log("Go find me someone for " + name);

    }

    private void IncreaseResource(string resourceName)
    {
        Debug.Log("Created " + resourceName + " resource");
        ResourceManager.Instance().Wood++;
    }
    private void DecreaseResource(string name)
    {
        Debug.Log("Used resource" + name);
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
}
