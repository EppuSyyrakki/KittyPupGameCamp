﻿using System;
using UnityEngine;

public class Controls : MonoBehaviour
{
    [HideInInspector] public Rigidbody OpponentRB { get; set; }

    public float _moveSpeed = 750;
    public float _force = 500;
    public float _jumpSpeed = 1000;
    public HingeJoint hip;
    public HingeJoint knee;
    public bool invertControl = false;  

    private ParticleSystem blood;
    private PlayerAudio playerAudio;
    private KneeSpeed kneeSpeed;

    [HideInInspector] public FixedJoint ownFixedJoint;  

    // Start is called before the first frame update
    virtual public void Start()
    {
        blood = GetComponent<ParticleSystem>();
        playerAudio = GetComponent<PlayerAudio>();
        kneeSpeed = GetComponentInChildren<KneeSpeed>();
        ownFixedJoint = GetComponent<FixedJoint>();
        FixedJoint fj = ownFixedJoint;
        fj.connectedBody = OpponentRB;
        ownFixedJoint = fj;
    }

    public void MoveLeg(float amount)
    {
        if (hip && knee && !ScoreControl._isOneFall)
        {
            JointMotor jointMotor = hip.motor;

            if (invertControl) jointMotor.targetVelocity = -amount * _moveSpeed;
            else jointMotor.targetVelocity = amount * _moveSpeed;

            hip.motor = jointMotor;
        }

        if (kneeSpeed.hasSpeed && !playerAudio.audioSource.isPlaying) 
            playerAudio.PlayRandom(PlayerAudio.AudioStyle.Contact);

    }

    public void StartJump()
    {
        if (hip && knee && !ScoreControl._isOneFall) knee.useMotor = true;
    }

    public void StopJump()
    {
        if (hip && knee) knee.useMotor = false;
    }

    /**
     * When this player loses call this to dislocate the legs. Work in progress.
     */
    public void Dislocate()
    {
        blood.Play();

        HingeJoint[] joints = GetComponentsInChildren<HingeJoint>();

        foreach (HingeJoint hj in joints)
        {
            hj.transform.parent = null;
            Destroy(hj);
        }            
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!ScoreControl._isOneFall && collision.gameObject.tag == "Ground")
            playerAudio.PlayRandom(PlayerAudio.AudioStyle.Death);            
    }
}
