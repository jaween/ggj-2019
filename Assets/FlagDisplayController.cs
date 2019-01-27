﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagDisplayController : MonoBehaviour
{
    private Vector3 startPosition;
    private float t;
    public new bool enabled = true;
    public Flag flag;

    private void Start()
    {
        startPosition = transform.position;
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Renderer>().material.

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
            gameObject.GetComponent<MeshRenderer>().material.mainTexture = flag.flag;
            //gameObject.GetComponent<MeshRenderer>().materials[0].mainTexture = flag.flag;
            var col = GetComponent<Renderer>().material.color;
            GetComponent<Renderer>().material.color = new Color(col.r, col.g, col.b, 0.3f);
        }
    }
}
