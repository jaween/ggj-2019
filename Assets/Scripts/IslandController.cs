using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandController : MonoBehaviour
{
    public Flag flag;
    public AudioClip successAudio;
    public AudioClip incorrectAudio;
    public ScoreController scoreController;
    public GameController gameController;
    private float lastSuccessTime = 0;

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
            if (otherFlag.flag.name == flag.name && (Time.time - lastSuccessTime) > 3)
            {
                Debug.Log("Correct");
                audioSource.clip = successAudio;
                audioSource.Play();
                scoreController.remaining--;
                lastSuccessTime = Time.time;
            }
            else
            {
                Debug.Log("Incorrect");
            }
        }

        if (scoreController.remaining == 0)
        {
            StartCoroutine("End");
        }
    }

    private IEnumerator End()
    {
        yield return new WaitForSeconds(2);
        gameController.End();
    }
}
