using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class UnitMovement : MonoBehaviour
{   
    NavMeshAgent myNavMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetDestinationToWorldPosition(Vector3 worldPosition)
    {
        myNavMeshAgent.SetDestination(worldPosition);
    }

    private void SetDestinationToMousePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //If mouse not over UI element
            if (!EventSystem.current.IsPointerOverGameObject())
                myNavMeshAgent.SetDestination(hit.point);
        }
    }

}
