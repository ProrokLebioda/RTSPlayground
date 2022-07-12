using UnityEngine;

public class PlayerUnitManager : MonoBehaviour
{
    [SerializeField]
    private int m_PlayerIndex;

    [SerializeField]
    private int AvailableAccomodation { get; set; }

    [SerializeField]
    private int UsedAccomodation { set; get; }
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
