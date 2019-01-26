using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerController : MonoBehaviour
{
    [HideInInspector]
    public CatapultController catapultController;

    [HideInInspector]
    public SpawnScript spawnScript;

    private Rigidbody childRigidbody;

    private bool timeup = false;
    private bool destroyed = false;

    void Start()
    {
        childRigidbody = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeup && !destroyed && childRigidbody.velocity.magnitude < 1)
        {
            StartCoroutine("Destroy");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Water" || other.tag == "Island")
        {
            StartCoroutine("Destroy");
        }
    }

    private IEnumerator Destroy()
    {
        destroyed = true;

        // TODO: Splash effect and sound
        catapultController.cameraPause = true;

        var trail = GetComponentInChildren<TrailRenderer>();
        trail.transform.parent = null;

        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbody in rigidbodies)
        {
            Destroy(rigidbody);
        }
      
        yield return new WaitForSeconds(1);

        spawnScript.Spawn();

        catapultController.cameraPause = false;
        catapultController.waitingForArrival = false;


        GameObject.Destroy(gameObject.transform.parent.gameObject);

        yield return new WaitForSeconds(5);
        Destroy(this.gameObject.transform.parent.gameObject);
    }

    private IEnumerator Timeup()
    {
        yield return new WaitForSeconds(1);
        timeup = true;
    }

    public void Launch()
    {
        StartCoroutine("Timeup");
    }
}
