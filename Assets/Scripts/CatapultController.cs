using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultController : MonoBehaviour
{
    public SpringJoint spring;
    public GameObject passengerPrefab;
    public GameObject neck;
    public Transform passengerNode;
    public Transform cameraChargeNode;
    public Transform cameraChargeFollowNode;
    public Transform cameraLaunchNode;
    public float turnSpeed;
    public float chargeSpeed;
    public CameraController cameraController;

    private GameObject currentPassenger;
    private Rigidbody currentPassengerRigidbody;
    private GameObject previousPassenger;
    private float springForce;

    void Start()
    {
        cameraController.cameraNode = cameraChargeNode;
        cameraController.followNode = cameraChargeFollowNode;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Create();
        }

        if (Input.GetMouseButton(0))
        {
            Charge();
        }

        if (Input.GetMouseButtonUp(0))
        {
            Release();
        }

        if (Input.GetMouseButton(0))
        {
            cameraController.cameraNode = cameraChargeNode;
            cameraController.followNode = cameraChargeFollowNode;
        } else
        {
            cameraController.cameraNode = cameraChargeNode;
            cameraController.followNode = currentPassengerRigidbody.transform;
        }

        Rotation();
    }

    private void Rotation()
    {
        var axisAmount = 0;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            axisAmount = 1;
        }
        if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            axisAmount = -1;
        }
        //var axisAmount = Input.GetAxis("Horizontal");
        var horizontal = axisAmount * turnSpeed * Time.deltaTime;
        var rotation = Quaternion.Euler(new Vector3(0, horizontal, 0));
        transform.localRotation *= rotation;
    }

    public void Create()
    {
        springForce = spring.spring;
        spring.GetComponent<SpringJoint>().spring = 0;

        spring.GetComponent<Rigidbody>().velocity = Vector3.zero;
        spring.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        currentPassenger = GameObject.Instantiate(passengerPrefab, passengerNode);
        currentPassengerRigidbody = currentPassenger.GetComponentInChildren<Rigidbody>();

        currentPassenger.GetComponentInChildren<TrailRenderer>().enabled = false;

        // Unparent any non-thrown passenger from the catapult
        if (previousPassenger != null)
        {
            previousPassenger.transform.parent = null;
            previousPassenger = currentPassenger;
        }
    }

    public void Charge()
    {
        var xRot = neck.transform.localRotation.eulerAngles.x + chargeSpeed * Time.deltaTime;
        xRot = Mathf.Clamp(xRot, -180, 55);
        neck.transform.localRotation = Quaternion.Euler(xRot, 0, 0);
    }

    private Vector3 CalculatePosition(Vector3 initialPosition, Vector3 launchVelocity, float elapsedTime)
    {
        return Physics.gravity * elapsedTime * elapsedTime * 0.5f +
                   launchVelocity * elapsedTime + initialPosition;
    }

    public void Release()
    {
        GetComponent<LineRenderer>().enabled = false;
        spring.spring = springForce;

        currentPassenger.transform.parent = null;
        currentPassenger.GetComponentInChildren<TrailRenderer>().enabled = true;
    }
}
