using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandwich : MonoBehaviour
{
    Stack<GameObject> ingredients;

    void Start()
    {
        ingredients = new Stack<GameObject>();
    }

    public void AddIngredient(GameObject ingredientToAdd)
    {
        ingredients.Push(ingredientToAdd);
    }
}
