using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform cameraTransform;
    public Transform followTransform;

    public float normalSpeed;
    public float fastSpeed;
    public float movementSpeed;
    public float movementTime;
    public float rotationAmount;
    public Vector3 zoomAmount;

    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;

    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;
    public Vector3 rotateStartPosition;
    public Vector3 rotateCurrentPosition;

    public Vector3 screenPosition;
    public Vector3 worldPosition;

    [SerializeField]
    private GameObject building;


    public GameObject wood;

    public enum MouseMode
    {
        NormalMode = 1,
        BuildMode,
        UnitControlMode
    };

    public MouseMode mouseMode;

    private MouseMode MouseControlMode { get => mouseMode; set => mouseMode = value; }

    public static CameraController Instance => instance ? instance : (instance = (new GameObject("CameraController")).AddComponent<CameraController>());
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        MouseControlMode = MouseMode.NormalMode;
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //if (followTransform != null)
        //{
        //    transform.position = followTransform.position;
        //}
        //else
        {
            HandleMouseInput();
            HandleMovementInput();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            followTransform = null;
        }
    }

    void HandleMouseInput()
    {
        MouseCameraZoom();
        MouseCameraPan();
        MouseCameraRotate();

        //if (Input.GetMouseButtonDown(0))
        //{
        //    //Left Mouse Button Pressed
        //    screenPosition = Input.mousePosition;
        //    Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        //    //worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        //    if (Physics.Raycast(ray, out RaycastHit hitData))
        //    {
        //        worldPosition = hitData.point;
        //    }
        //}

        // LMB
        if (Input.GetMouseButtonDown(0))
        {
            switch(MouseControlMode)
            {
                case MouseMode.NormalMode:

                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.tag == "Resource")
                        {
                            if (hit.collider.name.Contains("Wood"))
                            {
                                GameObject go = hit.collider.gameObject;
                                go.SetActive(false);
                                IResource res = go.GetComponent<IResource>();
                                res.UseResource();
                                
                            }
                        }
                        else if (hit.collider.tag == "Unit")
                        {
                            GameObject go = hit.collider.gameObject;
                            go.SetActive(false);
                            IUnit unit = go.GetComponent<IUnit>();
                            unit.Health--;                            
                        }
                        
                    }
                    break;

                case MouseMode.BuildMode:
                    //Build();
                    SetMouseMode(MouseMode.NormalMode);
                    break;

                case MouseMode.UnitControlMode:
                    break;
            }
        }

        //RMB
        if (Input.GetMouseButtonDown(1))
        {
            switch (MouseControlMode)
            {
                case MouseMode.NormalMode:
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        Instantiate(wood, hit.point, Quaternion.Euler(0f, -45f, 0f));
                        print(wood.name + " is spawned");
                    }
                    break;

                case MouseMode.BuildMode:
                    building = null;
                    SetMouseMode(MouseMode.NormalMode);
                    break;

                case MouseMode.UnitControlMode:
                    SetMouseMode(MouseMode.NormalMode);
                    break;
            }
        }


    }

    private void Build()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Instantiate(building, hit.point, Quaternion.Euler(0f, -45f,0f));
            building.GetComponent<IBuilding>()?.Place();
            print(building.name + " is spawned");
            building = null;
        }
    }

    public void SetBuilding(GameObject _b)
    {
        if (_b != null)
        {
            building = _b;
            _b = null;
        }
    }

    public void SetMouseMode(MouseMode newMode) => mouseMode = newMode;

    public MouseMode GetMouseMode() => mouseMode;
    

    private void MouseCameraRotate()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            if (Input.GetMouseButtonDown(1))
            {
                rotateStartPosition = Input.mousePosition;
            }
            if (Input.GetMouseButton(1))
            {
                rotateCurrentPosition = Input.mousePosition;

                Vector3 difference = rotateStartPosition - rotateCurrentPosition;

                rotateStartPosition = rotateCurrentPosition;

                newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
            }
        }
    }

    private void MouseCameraZoom()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }
    }

    private void MouseCameraPan()
    {
        if (Input.GetMouseButtonDown(2))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(2))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);

                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }
    }

    void HandleMovementInput()
    {
        KeyboardCameraAccelerator();
        KeyboardCameraMovement();
        KeyboardCameraRotation();
        KeyboardCameraZoom();

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }

    private void KeyboardCameraZoom()
    {
        if (Input.GetKey(KeyCode.R))
        {
            newZoom += zoomAmount;
        }
        if (Input.GetKey(KeyCode.F))
        {
            newZoom -= zoomAmount;
        }
    }

    private void KeyboardCameraRotation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }
    }

    private void KeyboardCameraMovement()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += transform.forward * movementSpeed;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += transform.forward * -movementSpeed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += transform.right * movementSpeed;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += transform.right * -movementSpeed;
        }
    }

    private void KeyboardCameraAccelerator()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }
    }


}
