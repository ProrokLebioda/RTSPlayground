using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour
{

    RaycastHit hit;
    Vector3 movePoint;
    public GameObject prefab;

    //private void OnEnable()
    //{
    //    MoveAndPlaceBuilding();
    //}

    // Start is called before the first frame update
    void Start()
    {
        MoveAndPlaceBuilding();
    }

    // Update is called once per frame
    void Update()
    {
        MoveAndPlaceBuilding();
    }
    private void MoveAndPlaceBuilding()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 50000.0f, LayerMask.GetMask("Terrain")))
        {
            transform.position = hit.point;

        }

        if (Input.GetMouseButton(0))
        {
            Instantiate(prefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}
