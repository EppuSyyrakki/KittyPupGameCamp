using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private Transform p1;
    private Transform p2;

    // Start is called before the first frame update
    void Start()
    {
        p1 = GameObject.FindGameObjectWithTag("Player1").transform;
        p2 = GameObject.FindGameObjectWithTag("Player2").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookPos = p1.position + (p2.position - p1.position) / 2;
        transform.position = new Vector3(lookPos.x / 2, transform.position.y, transform.position.z);
        transform.LookAt(lookPos);
    }
}
