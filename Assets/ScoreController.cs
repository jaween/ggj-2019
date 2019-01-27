using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public int remaining = 7;
    private Text text;
    
    void Start()
    {
        text = GetComponent<Text>();
    }

    
    void Update()
    {
        text.text = remaining + " passengers to go";
    }
}
