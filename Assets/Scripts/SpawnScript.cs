using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public Transform passengerNode;
    public GameObject passengerPrefab;

    public List<Flag> flags;

    [HideInInspector]
    public bool done = false;

    public float moveDuration;

    public GameController gameController;

    public GameObject currentPassenger;

    public GaspScript gaspController;

    public FlagDisplayController flagDisplayController;

    private void Start()
    {
        StartCoroutine("InitialSpawn");
    }

    private IEnumerator InitialSpawn()
    {
        yield return new WaitForSeconds(1);
        Spawn();
        done = true;
    }

    public void Spawn()
    {
        currentPassenger = GameObject.Instantiate(passengerPrefab, passengerNode);
        currentPassenger.GetComponentInChildren<TrailRenderer>().enabled = false;
        currentPassenger.GetComponentInChildren<PassengerController>().spawnScript = this;
        currentPassenger.GetComponentInChildren<PassengerController>().gameController = gameController;
        currentPassenger.GetComponentInChildren<PassengerController>().gaspController = gaspController;

        int i = Random.Range(0, flags.Count);
        currentPassenger.GetComponentInChildren<PassengerController>();
        currentPassenger.AddComponent<FlagController>();
        currentPassenger.GetComponentInChildren<FlagController>().flag = flags[i];
        flagDisplayController.flag = flags[i];

        done = true;

        /*currentPassenger = GameObject.Instantiate(passengerPrefab, transform);
        currentPassenger.GetComponentInChildren<TrailRenderer>().enabled = false;
        currentPassenger.GetComponentInChildren<Rigidbody>().useGravity = false;
        StartCoroutine("Move");*/
    }

    IEnumerator Move()
    {
        var startTime = Time.time;
        print("Start time is " + startTime);
        while (Time.time <= startTime + moveDuration)
        {
            currentPassenger.GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
            currentPassenger.GetComponentInChildren<Rigidbody>().angularVelocity = Vector3.zero;

            var t = (Time.time - startTime) / moveDuration;
            print(t + "");
            currentPassenger.transform.position = Vector3.Lerp(transform.position, passengerNode.position, t);
            yield return new WaitForEndOfFrame();
        }
        currentPassenger.GetComponentInChildren<Rigidbody>().useGravity = true;
        currentPassenger.transform.parent = passengerNode;
        done = true;
    }
}
