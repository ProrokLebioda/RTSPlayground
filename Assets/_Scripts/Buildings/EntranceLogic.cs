using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntranceLogic : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter");
        if (other.gameObject.tag == "Unit")
        {
            foreach (var meshRenderer in other.gameObject.GetComponentsInChildren<MeshRenderer>())
            {
                meshRenderer.enabled = false;
            }
            //other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            other.gameObject.GetComponent<IUnit>().IsInBuilding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit");
        if (other.gameObject.tag == "Unit")
        {
            foreach (var meshRenderer in other.gameObject.GetComponentsInChildren<MeshRenderer>())
            {
                meshRenderer.enabled = true;
            }
            //other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            other.gameObject.GetComponent<IUnit>().IsInBuilding = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("CollisionEnter");
    }    
}
