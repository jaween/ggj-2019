using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public CatapultController catapult;
    public CameraController cameraController;
    public Transform cameraStartNode;
    public Transform cameraStartFollow;
    private bool gameStarted = false;

    void Start()
    {
        catapult.enabled = false;
        StartCoroutine("Startup");
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted)
        {
            cameraController.cameraNode = cameraStartNode;
            cameraController.followNode = cameraStartFollow;
            cameraController.followTime = 3f;
        }
    }

    private IEnumerator Startup()
    {
        yield return new WaitForSeconds(2f);
        catapult.enabled = true;
        gameStarted = true;
    }
}
