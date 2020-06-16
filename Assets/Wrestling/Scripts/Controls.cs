using System;
using UnityEngine;

public class Controls : MonoBehaviour
{
    [HideInInspector] public Rigidbody OpponentRB { get; set; }

    public float _moveSpeed = 750;
    public float _force = 500;
    public float _jumpSpeed = 1000;
    public HingeJoint hip;
    public HingeJoint knee;

    [HideInInspector] public FixedJoint ownFixedJoint;    

    // Start is called before the first frame update
    virtual public void Start()
    {       
        ownFixedJoint = GetComponent<FixedJoint>();
        FixedJoint fj = ownFixedJoint;
        fj.connectedBody = OpponentRB;
        ownFixedJoint = fj;
    }

    public void MoveLeg(float amount)
    {
        JointMotor jointMotor = hip.motor;
        jointMotor.targetVelocity = amount * _moveSpeed;
        hip.motor = jointMotor;
    }

    public void StartJump()
    {
        Debug.Log("Jump started");
        knee.useMotor = true;
    }

    public void StopJump()
    {
        Debug.Log("Jump stopped");
        knee.useMotor = false;
    }
}
