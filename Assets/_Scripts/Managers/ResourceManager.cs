using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourceManager : MonoBehaviour
{

    private int logs;
    private int planks;
    private int stones;

    public static ResourceManager instance;

    public int Logs { get => logs; set => logs = value; }
    public int Planks { get => planks; set => planks = value; }
    public int Stones { get => stones; set => stones = value; }

    public static ResourceManager Instance() => instance ? instance : (instance = (new GameObject("ResourceManager")).AddComponent<ResourceManager>());

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Logs = 0;
        Planks = 0;
        Stones = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
         
}
