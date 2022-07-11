using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour, IResource
{
    // Start is called before the first frame update
    void Start()
    {
        CreateResource();
    }

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

    public void OnDestroy()
    {        
    }
}
