using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("Increase Size if needed")]
    [Header("and drag preset prefab to the new Element")]
    public GameObject[] allPresets;
   
    [NonSerialized] public GameObject targetSandwich;   // nonserialized public is public but doesn't show up in editor
    [NonSerialized] public Transform[] positions;       // no need for these to be seen in editor.

    // Start is called before the first frame update
    void Start()
    {
        targetSandwich = GetTargetSandwich();
        positions = GetPositions(targetSandwich.transform.childCount);
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
        Transform lowestPos = transform.GetChild(0);
        Transform highestPos = transform.GetChild(1);
        float stepDifference = (highestPos.position.y - lowestPos.position.y) / (ingredientCount - 1);

        for (int i = 0; i < targetSandwich.transform.childCount; i++)
        {
            GameObject pos = new GameObject(targetSandwich.transform.GetChild(i).name + "Pos");
            pos.transform.position = highestPos.position + new Vector3(0, -i * stepDifference, 0);
            positions[i] = pos.transform;
            positions[i].transform.SetParent(gameObject.transform);
        }
        return positions;
    }

    private void InstantiateTarget()
    {
        for (int i = 0; i < targetSandwich.transform.childCount; i++)
        {  
            GameObject ingredient = Instantiate(targetSandwich.transform.GetChild(i).gameObject, positions[i]);
            ingredient.name = targetSandwich.transform.GetChild(i).name;
            ingredient.transform.SetParent(gameObject.transform);
            ingredient.transform.SetAsLastSibling();
        }
    }
}
