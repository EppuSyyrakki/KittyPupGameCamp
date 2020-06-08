using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject[] allPresets;
   
    [NonSerialized] public GameObject targetSandwich;
    [NonSerialized] public Transform[] positions;

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
            GameObject pos = new GameObject("targetPos " + i);
            pos.transform.position = highestPos.position + new Vector3(0, -i * stepDifference, 0);
            positions[i] = pos.transform;
        }
        return positions;
    }

    private void InstantiateTarget()
    {
        for (int i = 0; i < targetSandwich.transform.childCount; i++)
        {  
            GameObject ingredient = Instantiate(targetSandwich.transform.GetChild(i).gameObject, positions[i]);
            ingredient.transform.SetParent(gameObject.transform);
        }
    }
}
