using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagDisplayController : MonoBehaviour
{
    private Vector3 startPosition;
    private float t;
    public new bool enabled = true;

    private void Start()
    {
        startPosition = transform.position;
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled)
        {
            t += Time.deltaTime * 2;
            var newPosition = new Vector3(
                startPosition.x,
                startPosition.y + Mathf.Sin(t),
                startPosition.z);
            transform.position = newPosition;
        } else
        {
            var col = GetComponent<Renderer>().material.color;
            GetComponent<Renderer>().material.color = new Color(col.r, col.g, col.b, 0.3f);
        }
    }
}
