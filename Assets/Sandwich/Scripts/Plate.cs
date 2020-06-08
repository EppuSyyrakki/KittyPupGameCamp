using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    Sandwich mySandwich;

    // Start is called before the first frame update
    void Start()
    {
        mySandwich = new Sandwich(new Stack<Ingredient>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
