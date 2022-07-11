using UnityEngine;
using TMPro;


public class ResourceManager : MonoBehaviour
{
    public TMP_Text woodCount;
    private int wood;
    private int planks;
    private int stones;

    public static ResourceManager instance;

    public int Wood { get => wood;
        set 
        {
            wood = value;
            woodCount.text = wood.ToString();
        }
    }
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
        Wood = 0;
        Planks = 0;
        Stones = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
         
}
