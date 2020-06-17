using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("CameraTarget").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 targetInWorld = transform.TransformPoint(target.position);
        // transform.position = new Vector3(targetInWorld.x, targetInWorld.y + 2, transform.position.z);
        transform.LookAt(target);
    }
}
