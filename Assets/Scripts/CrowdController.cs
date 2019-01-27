using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Jump()
    {
        while (true)
        {
            foreach (Transform child in transform)
            {
                bool jump = (Random.value > 0.1f);
                if (jump)
                {
                    var rigidbody = child.GetComponent<Rigidbody>();
                    rigidbody.velocity += Vector3.up * 3;
                }
                var t = Random.Range(0.1f, 0.2f);
                yield return new WaitForSeconds(t);
            }
        }
    }
}
