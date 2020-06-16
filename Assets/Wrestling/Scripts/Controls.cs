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
        knee.useMotor = true;
    }

    public void StopJump()
    {
        knee.useMotor = false;
    }

    /**
     * When this player loses call this to dislocate the legs. Work in progress.
     */
    public void Dislocate()
    {
        HingeJoint[] joints = GetComponentsInChildren<HingeJoint>();

        foreach (HingeJoint hj in joints)
        {
            hj.transform.parent = null;
            Destroy(hj);
        }            
    }
}
