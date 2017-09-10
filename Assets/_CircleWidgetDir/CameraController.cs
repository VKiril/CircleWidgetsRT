using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float speed = 2.0f;

    public float minY = -45.0f;
    public float maxY = 45.0f;

    public float sensX = 100.0f;
    public float sensY = 100.0f;

    public float zoomSpeed = 50.0f;

    public float minAngle = 60.0f;
    public float maxAngle = 120.0f;

    const string RIGHT_W_TAG_NAME = "RightWidget";
    const string LEFT_W_TAG_NAME  = "LeftWidget";

    private int rotationMultiplier = 1;

    protected Vector3 rot;

    protected float rotationY = 0.0f;
    protected float rotationX = 0.0f;

    protected bool acceptableAngle = false;
    protected bool isCameraInWidgetArea = false;
    protected CircleWidgetController controller;

    GameObject camParent;

    void Start()
    {
        camParent = new GameObject("MainCamera");
        camParent.transform.position = this.transform.position;
        this.transform.parent = camParent.transform;
        Input.gyro.enabled = true;

        rot = transform.localEulerAngles;
    }

	// Update is called once per frame
	void Update () {
        CameraDeltaRotation();
        InputAxis();
        Scroll();
        MoveMouse();

        transform.Rotate(0, -Input.gyro.rotationRateUnbiased.y, 0);
        this.transform.Rotate(-Input.gyro.rotationRateUnbiased.x, 0, 0);
    }

    protected void ShowWidget()
    {
        if (controller != null)
        {
            if (acceptableAngle && isCameraInWidgetArea)
            {
                controller.SetIsWidgetShown(true);
            }
            else
            {
                controller.SetIsWidgetShown(false);
                controller.SetIsPlayingSecondary(false);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        
        isCameraInWidgetArea = true;
        controller = other.GetComponent<CircleWidgetController>();
        string tag = other.gameObject.tag;
        
        rotationMultiplier = 1;
        if (tag.ToString() == RIGHT_W_TAG_NAME)
        {
            rotationMultiplier = -1;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        isCameraInWidgetArea = false;
        rotationMultiplier = 0;
    }
    
    protected void CameraDeltaRotation()
    {
        Vector3 currentRotation = transform.localEulerAngles;
        float diffX = Mathf.DeltaAngle(currentRotation.y, rot.y);

        float localMinAngle = minAngle * rotationMultiplier;
        float localMaxAngle = maxAngle * rotationMultiplier;

        if (rotationMultiplier == 1)
        {
            if (diffX > localMinAngle && diffX < localMaxAngle)
            {
                acceptableAngle = true;
            }
            else
            {
                acceptableAngle = false;
            }
        }
        else if (rotationMultiplier == -1)
        {
            if (diffX < localMinAngle && diffX > localMaxAngle)
            {
                acceptableAngle = true;
            }
            else
            {
                acceptableAngle = false;
            }
        }
        
        ShowWidget();
    }



    protected void MoveMouse()
    {
        if (Input.GetMouseButton(0))
        {
            rotationX += Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
            rotationY += Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
    }

    protected void Scroll()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(0, scroll * zoomSpeed, scroll * zoomSpeed, Space.World);
    }

    protected void InputAxis()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed);
        }
    }
}
