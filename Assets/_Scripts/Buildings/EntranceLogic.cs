using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntranceLogic : MonoBehaviour
{
    private GameObject parentBuilding;

    private void Start()
    {
        parentBuilding = gameObject.transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter");
        if (other.gameObject.tag == "Unit")
        {
            IUnit unit = other.gameObject.GetComponent<IUnit>();
            if (unit.Workplace.Equals(parentBuilding))
            {
                other.gameObject.GetComponent<IUnit>().IsInBuilding = true;
                foreach (var meshRenderer in other.gameObject.GetComponentsInChildren<MeshRenderer>())
                {
                    meshRenderer.enabled = false;
                }
                //other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit");
        if (other.gameObject.tag == "Unit")
        {
            IUnit unit = other.gameObject.GetComponent<IUnit>();
            if (unit.Workplace.Equals(parentBuilding))
            {
                other.gameObject.GetComponent<IUnit>().IsInBuilding = false;
                foreach (var meshRenderer in other.gameObject.GetComponentsInChildren<MeshRenderer>())
                {
                    meshRenderer.enabled = true;
                }
                //other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("CollisionEnter");
    }    
}
