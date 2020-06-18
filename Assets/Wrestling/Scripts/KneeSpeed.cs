using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class KneeSpeed : MonoBehaviour
{
    public bool hasSpeed = false;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (rb.velocity.x >= 13f)
        {
            hasSpeed = true;
        }
        else hasSpeed = false;
    }
}
