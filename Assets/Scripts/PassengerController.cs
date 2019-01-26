using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerController : MonoBehaviour
{
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Water")
        {
            StartCoroutine("Destroy");
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject.transform.parent.gameObject);
    }
}
