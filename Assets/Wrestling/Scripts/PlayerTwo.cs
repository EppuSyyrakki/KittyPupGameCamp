using UnityEngine;

public class PlayerTwo : Controls
{
    // Start is called before the first frame update
    override public void Start()
    {
        OpponentRB = GameObject.Find("Player1").GetComponent<Rigidbody>();
        base.Start();     
    }

    // Update is called once per frame
    void Update()
    {
        MoveLeg(-Input.GetAxis("Player2"));

        if (Input.GetButtonDown("Jump2"))
            StartJump();

        if (Input.GetButtonUp("Jump2"))
            StopJump();
    }
}
