using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTemplate : MonoBehaviour, IResource
{
    public ResourceType ResType { get; set; }
    public bool IsInUse { get; set; }
    
    // done as an courutine to help fix issue of not registered spawn, perhaps all resources should have it
    // or maybe should be fixed in other way. Problems might occur when added Save option
    public virtual IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        CreateResource();
        Debug.Log("Resource type: " + ResType);
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
        IResource.OnCreated(ResType);
    }

    public void UseResource()
    {
        IResource.OnUsed(ResType);
        Destroy(this);
    }
}
