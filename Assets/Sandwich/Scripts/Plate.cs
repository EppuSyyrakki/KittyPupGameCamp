using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public List<GameObject> sandwich;

    // Start is called before the first frame update
    void Start()
    {
        if (sandwich == null) sandwich = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetSandwich()
    {
        foreach (GameObject obj in sandwich) Destroy(obj);
        sandwich.Clear();
    }
}
