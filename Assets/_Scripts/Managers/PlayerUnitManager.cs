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
    Dictionary<UnitType, int> UnitsInfoDictionary = new Dictionary<UnitType, int>();

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
}
