using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    public HingeJoint p1Hip;
    public HingeJoint p1Knee;

    public HingeJoint p2Hip;
    public HingeJoint p2Knee;

    public float moveSpeed;
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float p1x = Input.GetAxis("Player1");
        float p2x = Input.GetAxis("Player2");      

        MoveLeg(p1Hip, p1x);
        MoveLeg(p2Hip, p2x);
    }

    private void MoveLeg(HingeJoint hj, float amount)
    {       
        JointMotor jointMotor = hj.motor;
        jointMotor.force = Mathf.Abs(amount) * force;
        jointMotor.targetVelocity = amount * moveSpeed;
        hj.motor = jointMotor;
    }

}
