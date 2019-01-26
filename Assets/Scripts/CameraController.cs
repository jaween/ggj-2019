using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraNode;
    public Transform followNode;
    public float followTime;

    // Update is called once per frame
    void Update()
    {
        var vel = Vector3.zero;
        var targetPos = cameraNode.transform.position;
        var newPos = Vector3.SmoothDamp(Camera.main.transform.position, targetPos, ref vel, followTime);
        //Camera.main.transform.position = new Vector3(newPos.x, newPos.y, newPos.z);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPos, Time.deltaTime * followTime);

        var targetForward = (followNode.transform.position - cameraNode.transform.position).normalized;
        var newForward = Vector3.SmoothDamp(Camera.main.transform.forward, targetForward, ref vel, followTime);
        var targetRotation = Quaternion.LookRotation(targetForward, Vector3.up);
        Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, targetRotation, Time.deltaTime * followTime);// Quaternion.LookRotation(targetForward, Vector3.up);
    }
}
