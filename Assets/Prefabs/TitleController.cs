using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{

    public Transform followNode;
    private float t = 0;
    public float distanceMin;
    public float distanceMax;
    public float speed;
    public GameObject ocean;
    public Button start;

    // Start is called before the first frame update
    void Start()
    {
        //ocean.GetComponent<Renderer>().material.= new Color(0x00, 0xCC, 0x00, 0xFF);
        start.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime * speed;
        var vel = Vector3.zero;

        float distance = distanceMin + (distanceMax - distanceMin) * (Mathf.Sin(t * 5) / 2 + 1);

        var pos = Camera.main.transform.position;
        var targetPos = new Vector3(
            distance * (Mathf.Cos(t) - Mathf.Sin(t)),
            pos.y,
            distance * (Mathf.Sin(t) + Mathf.Cos(t))
        );
        //Camera.main.transform.position = new Vector3(newPos.x, newPos.y, newPos.z);
        Camera.main.transform.position = targetPos;

        var targetForward = (- Camera.main.transform.position).normalized;
        Camera.main.transform.forward = targetForward;// Quaternion.LookRotation(targetForward);
    }

    void OnClick()
    {
        SceneManager.LoadScene("Main");
    }
}
