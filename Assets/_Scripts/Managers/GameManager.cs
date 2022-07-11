using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private void OnEnable()
    {
        IBuilding.OnBuilt += FindCollonizer;
        IResource.OnCreated += IncreaseResource;
        IResource.OnUsed += DecreaseResource;
    }


    private void OnDisable()
    {
        IBuilding.OnBuilt -= FindCollonizer;
        IResource.OnCreated -= IncreaseResource;
        IResource.OnUsed -= DecreaseResource;
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
