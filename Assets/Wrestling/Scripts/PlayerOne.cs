using UnityEngine;

public class PlayerOne : Controls
{
    void Awake()
    {
        gameObject.name = "Player1";
    }

    // Start is called before the first frame update
    override public void Start()
    {
        OpponentRB = GameObject.Find("Player2").GetComponent<Rigidbody>();
        base.Start();        
    }

    // Update is called once per frame
    void Update()
    {
        MoveLeg(Input.GetAxis("Player1"));

        if (Input.GetButtonDown("Jump1"))
            StartJump();

        if (Input.GetButtonUp("Jump1"))
            StopJump();
    }
}
