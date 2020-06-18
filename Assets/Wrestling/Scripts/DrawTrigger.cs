using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wrestling;

public class DrawTrigger : MonoBehaviour
{
    UIMaster uiMaster;
    private void Start()
    {
        uiMaster = GameObject.Find("Canvas In Game").GetComponent<UIMaster>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2")
        {
            Debug.Log("Round ends in draw");
            // uiMaster.TriggerDrawTextOrWhatever();
        }
    }
}
