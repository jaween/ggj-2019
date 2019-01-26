using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandController : MonoBehaviour
{
    public Flag flag;
    public AudioClip successAudio;
    public AudioClip incorrectAudio;

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
        var otherFlag = other.gameObject.GetComponentInParent<FlagController>();
        var audioSource = GetComponent<AudioSource>();
        if (otherFlag != null)
        {
            if (otherFlag.flag.name == flag.name)
            {
                Debug.Log("Correct");
                audioSource.clip = successAudio;
                audioSource.Play();
            }
            else
            {
                Debug.Log("Incorrect");
                audioSource.clip = incorrectAudio;
                audioSource.Play();
            }
        }
    }
}
