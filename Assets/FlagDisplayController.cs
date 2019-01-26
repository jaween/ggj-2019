using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagDisplayController : MonoBehaviour
{
    private Vector3 startPosition;
    private float t;

    private void Start()
    {
        startPosition = transform.position;
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime * 2;
        var newPosition = new Vector3(
            startPosition.x,
            startPosition.y + Mathf.Sin(t),
            startPosition.z);
        transform.position = newPosition;
    }
}
