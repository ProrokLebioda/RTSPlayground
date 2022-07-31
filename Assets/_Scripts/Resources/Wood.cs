using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour, IResource
{
    public ResourceType resourceType { get => ResourceType.Wood; }
    public bool IsInUse { get; set; }

    // done as an courutine to help fix issue of not registered spawn, perhaps all resources should have it
    // or maybe should be fixed in other way. Problems might occur when added Save option
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        CreateResource();
    }

    //private void OnEnable()
    //{
    //    CreateResource();
        
    //}

    // Update is called once per frame
    void Update()
    {       
    }

    public void CreateResource()
    {
        IsInUse = false;
        IResource.OnCreated(resourceType);
    }

    public void UseResource()
    {
        IResource.OnUsed(resourceType);
        Destroy(this);  
    }
        
}
