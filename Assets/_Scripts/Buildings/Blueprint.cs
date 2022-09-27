using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour
{

    RaycastHit hit;
    Vector3 movePoint;
    //public GameObject prefab;
    bool isPlaced = false;
    public Material blueprintMaterial;
    private List<Material> originalMaterials = new();

    private void OnEnable()
    {
        GameObject Parent = this.gameObject;
        //Instantiates a gameobject at (0, 0, 0) with default rotation

        Transform[] Children = Parent.GetComponentsInChildren<Transform>(); //Creates an array of all transforms within its children

        
        foreach (Transform child in Children) //Anything that you want applied to all children in the parent object goes in here
        {
            MeshRenderer mr = child.gameObject.GetComponent<MeshRenderer>();
            if (mr)
            {
                originalMaterials.Add(mr.material);
                mr.material = blueprintMaterial; //Adds a mesh renderer to all children within the parent object
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //MoveAndPlaceBuilding();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaced)
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
            isPlaced = true;            
            StartCoroutine(PlaceBuildingRemoveBlueprint());
        }
        else if (Input.GetMouseButton(1))
        {
            Destroy(this.gameObject);
            CameraController.Instance.SetMouseMode(CameraController.MouseMode.NormalMode);
        }
    }

    //IEnumerator PlaceBuildingRemoveBlueprint()
    //{
    //    this.gameObject.GetComponent<IBuilding>().Place();
    //    yield return new WaitForSeconds(3f);
    //    this.gameObject.GetComponent<IBuilding>().IsBuilt=true;
    //    this.gameObject.GetComponent<IBuilding>().FinishedBuilding();

    //    // Restore materials
    //    GameObject Parent = this.gameObject;
    //    Transform[] Children = Parent.GetComponentsInChildren<Transform>(); //Creates an array of all transforms within its children

    //    foreach (Transform child in Children) //Anything that you want applied to all children in the parent object goes in here
    //    {
    //        MeshRenderer mr = child.gameObject.GetComponent<MeshRenderer>();
    //        if (mr)
    //        {
    //            mr.material = originalMaterials.ToArray()[0];
    //            originalMaterials.RemoveAt(0);
    //        }
    //    }
    //    originalMaterials.Clear();
    //    Destroy(this);// removes just script Blueprint
    //}

    IEnumerator PlaceBuildingRemoveBlueprint()
    {
        IBuilding building = this.gameObject.GetComponent<IBuilding>();
        building.Place();
        yield return new WaitForSeconds(3f);


        this.gameObject.GetComponent<IBuilding>().IsBuilt = true;
        this.gameObject.GetComponent<IBuilding>().FinishedBuilding();

        // Restore materials
        GameObject Parent = this.gameObject;
        Transform[] Children = Parent.GetComponentsInChildren<Transform>(); //Creates an array of all transforms within its children

        foreach (Transform child in Children) //Anything that you want applied to all children in the parent object goes in here
        {
            MeshRenderer mr = child.gameObject.GetComponent<MeshRenderer>();
            if (mr)
            {
                mr.material = originalMaterials.ToArray()[0];
                originalMaterials.RemoveAt(0);
            }
        }
        originalMaterials.Clear();
        Destroy(this);// removes just script Blueprint
    }

}
