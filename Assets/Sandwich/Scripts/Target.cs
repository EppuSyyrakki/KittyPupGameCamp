using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Sandwich targetSandwich;    

    // Start is called before the first frame update
    void Start()
    {
        targetSandwich = new Sandwich(new Stack<GameObject>()); // add some kind of randomizer or take from preset targets
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
