using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerController : MonoBehaviour
{
    [HideInInspector]
    public CatapultController catapultController;

    [HideInInspector]
    public SpawnScript spawnScript;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

        yield return new WaitForSeconds(5);
        Destroy(this.gameObject.transform.parent.gameObject);
    }
}
