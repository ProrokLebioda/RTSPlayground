using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private void OnEnable()
    {
        IBuilding.OnBuilt += FindCollonizer;
    }
    private void OnDisable()
    {
        IBuilding.OnBuilt -= FindCollonizer;
    }

    private void FindCollonizer(string name)
    {
        Debug.Log("Go find me someone for " + name);
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
