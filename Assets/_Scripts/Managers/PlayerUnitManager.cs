using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitManager : MonoBehaviour
{
    [SerializeField]
    private int m_PlayerIndex;

    [SerializeField]
    private int AvailableAccomodation { get; set; }

    [SerializeField]
    private int UsedAccomodation { set; get; }

    [SerializeField]
    public Dictionary<UnitType, int> UnitsInfoDictionary = new Dictionary<UnitType, int>();

    
    private void OnEnable()
    {
        IUnit.OnUnitSpawned += AddToUnitsInfoDictionary;
        IUnit.OnUnitRemoved += RemoveFromUnitsInfoDictionary;
    }

    private void OnDisable()
    {
        IUnit.OnUnitSpawned -= AddToUnitsInfoDictionary;
        IUnit.OnUnitRemoved -= RemoveFromUnitsInfoDictionary;
    }

    private void RemoveFromUnitsInfoDictionary(UnitType type)
    {
        if (UnitsInfoDictionary.TryGetValue(type, out int value))
        {
            UnitsInfoDictionary[type]--;
        }
    }

    private void AddToUnitsInfoDictionary(UnitType type)
    {
        // If key already exists, increase amount
        if (!UnitsInfoDictionary.TryAdd(type, 1))
        {
            UnitsInfoDictionary[type]++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AvailableAccomodation = 20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool FindFreeLaborerForBuilding(GameObject building)
    {
        
        UnitType type = building.GetComponent<IBuilding>().WantedUnitType;
        if (UnitsInfoDictionary.TryGetValue(type, out int value))
        {
            Debug.Log("Found " + type.ToString());
            var gos = UnityEngine.GameObject.FindGameObjectsWithTag("Unit");
            //UnityEngine.Object[] allObjects = UnityEngine.Object.FindObjectsOfType(typeof(IUnit));
            
            foreach (GameObject go in gos)
            {
                if (go.GetComponent<IUnit>().Type == type)
                {
                    if (!go.GetComponent<IUnit>().Workplace)
                    {
                        go.GetComponent<IUnit>().Workplace = building;
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
