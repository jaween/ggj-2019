using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraNode;
    public Transform followNode;

    // Update is called once per frame
    void Update()
    {
        var vel = Vector3.zero;
        var targetPos = cameraNode.transform.position;
        var newPos = Vector3.SmoothDamp(Camera.main.transform.position, targetPos, ref vel, 0.03f);
        Camera.main.transform.position = new Vector3(newPos.x, newPos.y, newPos.z);

        var targetForward = (followNode.transform.position - cameraNode.transform.position).normalized;
        var newForward = Vector3.SmoothDamp(Camera.main.transform.forward, targetForward, ref vel, 0.02f);
        Camera.main.transform.forward = new Vector3(newForward.x, newForward.y, newForward.z);
    }
}
