using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour, IResource
{
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
        IResource.OnCreated("Wood");
    }

    public void UseResource()
    {
        IResource.OnUsed("Wood");
        Destroy(this);
    }
        
}
