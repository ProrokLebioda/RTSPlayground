using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHome : BuildingTemplate
{
    private void OnEnable()
    {
        Build();
    }


    public override void Build()
    {

        for (int i = 0; i < 1000000; i++)
        { 

        }
        IsBuilt = true;
        if (IsBuilt)
            IBuilding.OnBuilt(gameObject);
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
