using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandController : MonoBehaviour
{
    public Flag flag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherFlag = other.gameObject.GetComponent<FlagController>();
        if (otherFlag != null && otherFlag.name == flag.name)
        {
            Debug.Log("Correct");
        }
    }
}
