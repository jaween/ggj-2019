using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaspScript : MonoBehaviour
{

    public List<AudioClip> gasps;
    public void Play()
    {
        var index = Random.Range(0, gasps.Count);
        GetComponent<AudioSource>().clip = gasps[index];
        GetComponent<AudioSource>().Play();
    }
}
