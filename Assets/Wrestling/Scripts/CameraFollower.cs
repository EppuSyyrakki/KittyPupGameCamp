using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollower : MonoBehaviour
{
    private Transform target;
    private int sceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex != 0) target = GameObject.Find("CameraTarget").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneIndex != 0) 
        { 
            transform.position = new Vector3(target.position.x / 2, transform.position.y, transform.position.z);
            transform.LookAt(target);
        }
    }
}
