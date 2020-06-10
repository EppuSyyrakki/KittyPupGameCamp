using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("Increase SIZE if needed and drag preset to the new row")]
    public GameObject[] allPresets;
   
    [NonSerialized] public GameObject targetSandwich;   // nonserialized public is public but doesn't show up in editor
    [NonSerialized] public Transform[] positions;       // no need for these to be seen in editor.

    // Start is called before the first frame update
    void Start()
    {
        targetSandwich = GetTargetSandwich();
        positions = GetPositions(targetSandwich.transform.childCount);  // create new emptys according to ingredient count of preset
        InstantiateTarget();       
    }

    private GameObject GetTargetSandwich()
    {
        int randomIndex = UnityEngine.Random.Range(0, allPresets.Length);   // select random prefab from array
        GameObject selected = allPresets[randomIndex];
        
        for (int i = 0; i < selected.transform.childCount; i++)        
            selected.transform.GetChild(i).position = Vector3.zero; // reset prefab positions, easier to make prefabs
        
        return selected;
    }

    private Transform[] GetPositions(int ingredientCount)
    {
        Transform[] positions = new Transform[targetSandwich.transform.childCount];
        Transform lowestPos = transform.GetChild(0);        // divide the space between highest and lowest positions to accommodate
        Transform highestPos = transform.GetChild(1);       // all ingredients (more ingredients, the closer together they are)
        float stepDifference = (highestPos.position.y - lowestPos.position.y) / (ingredientCount - 1);  

        for (int i = 0; i < targetSandwich.transform.childCount; i++)
        {
            GameObject pos = new GameObject(targetSandwich.transform.GetChild(i).name + "Pos");
            pos.transform.position = highestPos.position + new Vector3(0, -i * stepDifference, 0);  // choose the correct position
            positions[i] = pos.transform;
            positions[i].transform.SetParent(gameObject.transform); // change the parent to be the this, not the highest/lowest
        }
        return positions;
    }

    private void InstantiateTarget()
    {
        for (int i = 0; i < targetSandwich.transform.childCount; i++)
        {  
            GameObject ingredient = Instantiate(targetSandwich.transform.GetChild(i).gameObject, positions[i]);
            Destroy(ingredient.GetComponent<ItemTimer>());  // destroy timer to not move object
            ingredient.name = targetSandwich.transform.GetChild(i).name;    // get the normal name instead of (Clone)
            ingredient.transform.SetParent(gameObject.transform);   // parent this, not position object
            ingredient.transform.SetAsLastSibling();    // set the hierarchy order to same as on screen
        }
    }
}
