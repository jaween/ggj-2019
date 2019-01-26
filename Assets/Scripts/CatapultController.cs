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
    public SpawnScript spawner;

    private GameObject currentPassenger;
    private Rigidbody currentPassengerRigidbody;
    private GameObject previousPassenger;
    private float springForce;

    [HideInInspector]
    public bool waitingForArrival = false;

    [HideInInspector]
    public bool cameraPause = false;

    private bool mouseDown = false;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !waitingForArrival && spawner.done)
        {
            mouseDown = true;
            Create();
        }

        if (Input.GetMouseButton(0) && !waitingForArrival && mouseDown)
        {
            Charge();
        }

        if (Input.GetMouseButtonUp(0) && !waitingForArrival && mouseDown)
        {
            mouseDown = false;
            Release();
        }

        if (Input.GetMouseButton(0) && !waitingForArrival)
        {
            cameraController.cameraNode = cameraChargeNode;
            cameraController.followNode = cameraChargeFollowNode;
            cameraController.followTime = 8f;
        } else if (!cameraPause)
        {
            cameraController.cameraNode = currentPassengerRigidbody == null ? cameraChargeNode : cameraLaunchNode;
            cameraController.followNode = currentPassengerRigidbody == null ? cameraChargeFollowNode : currentPassengerRigidbody.transform;
            cameraController.followTime = 8f;
        }

        Rotation();
    }

    private void Rotation()
    {
        if (waitingForArrival)
        {
            return;
        }

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

        spawner.done = false;
        currentPassenger = spawner.currentPassenger;
        currentPassengerRigidbody = currentPassenger.GetComponentInChildren<Rigidbody>();
        spawner.currentPassenger = null;
        /*currentPassenger = GameObject.Instantiate(passengerPrefab, passengerNode);
        currentPassengerRigidbody = currentPassenger.GetComponentInChildren<Rigidbody>();*/
        currentPassenger.GetComponentInChildren<PassengerController>().catapultController = this;

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
        spring.spring = springForce;

        currentPassenger.transform.parent = null;
        currentPassenger.GetComponentInChildren<TrailRenderer>().enabled = true;
        currentPassenger.GetComponentInChildren<PassengerController>().Launch();

        waitingForArrival = true;
    }
}
