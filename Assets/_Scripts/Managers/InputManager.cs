using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private GameObject buildingsMenuUI;
    [SerializeField]
    private GameObject unitsMenuUI;
    [SerializeField]
    private GameObject resourcesMenuUI;
    [SerializeField]
    private GameObject statisticsMenuUI;

    [SerializeField]
    private GameObject resourceBuildingsPanel;
    [SerializeField]
    private GameObject productionBuildingsPanel;

    public GameObject building { get; private set; }

    private void Start()
    {
        buildingsMenuUI.SetActive(false);
        unitsMenuUI.SetActive(false);
        resourcesMenuUI.SetActive(false);
        statisticsMenuUI.SetActive(false);
    }


    public void OnBuildingsMenuUIClick()
    {
        buildingsMenuUI.SetActive(true);
        unitsMenuUI.SetActive(false);
        resourcesMenuUI.SetActive(false);
        statisticsMenuUI.SetActive(false);
    }

    public void OnUnitsMenuUIClick()
    {
        buildingsMenuUI.SetActive(false);
        unitsMenuUI.SetActive(true);
        resourcesMenuUI.SetActive(false);
        statisticsMenuUI.SetActive(false);
    }

    public void OnResourcesMenuUIClick()
    {
        buildingsMenuUI.SetActive(false);
        unitsMenuUI.SetActive(false);
        resourcesMenuUI.SetActive(true);
        statisticsMenuUI.SetActive(false);
    }

    public void OnStatisticsMenuUIClick()
    {
        buildingsMenuUI.SetActive(false);
        unitsMenuUI.SetActive(false);
        resourcesMenuUI.SetActive(false);
        statisticsMenuUI.SetActive(true);
    }

    public void OnResourceBuildingsButtonClick()
    {
        resourceBuildingsPanel.SetActive(true);
        productionBuildingsPanel.SetActive(false);
    }

    public void OnProductionBuildingsButtonClick()
    {
        resourceBuildingsPanel.SetActive(false);
        productionBuildingsPanel.SetActive(true);
    }

    public void OnWoodcutterHutButtonClick()
    {
        // Place code responsible for building a Woodcutter's Hut
        CameraController.Instance.SetMouseMode(CameraController.MouseMode.BuildMode);
        building = Resources.Load<GameObject>("_Prefabs/Buildings/Resources/WoodcutterHut");
        Instantiate(building, transform.position, Quaternion.Euler(0f, -45f, 0f));

        //CameraController.Instance.SetBuilding(building/*.GetComponent<Blueprint>().gameObject*/);
    }

    public void OnStonecutterHutButtonClick()
    {
        // Place code responsible for building a Woodcutter's Hut
        CameraController.Instance.SetMouseMode(CameraController.MouseMode.BuildMode);
        building = Resources.Load<GameObject>("_Prefabs/Buildings/Resources/StonecutterHut");
        Instantiate(building, transform.position, Quaternion.Euler(0f, -45f, 0f));

        //CameraController.Instance.SetBuilding(building/*.GetComponent<Blueprint>().gameObject*/);
    }

    public void OnTreePlanterHouseButtonClick()
    {
        // Place code responsible for building a Simple House
        CameraController.Instance.SetMouseMode(CameraController.MouseMode.BuildMode);
        building = Resources.Load<GameObject>("_Prefabs/Buildings/Resources/TreePlanter");
        Instantiate(building, transform.position, Quaternion.Euler(0f, -45f, 0f));
        //CameraController.Instance.SetBuilding(building);
    }

    public void OnSimpleHouseButtonClick()
    {
        // Place code responsible for building a Simple House
        CameraController.Instance.SetMouseMode(CameraController.MouseMode.BuildMode);
        building = Resources.Load<GameObject>("_Prefabs/Buildings/Homes/SimpleHome");
        Instantiate(building, transform.position, Quaternion.Euler(0f, -45f, 0f));
        //CameraController.Instance.SetBuilding(building);
    }

}
