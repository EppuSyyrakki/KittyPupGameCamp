using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandwich : MonoBehaviour
{
    Stack<GameObject> ingredients;

    public void AddIngredient(GameObject ingredientToAdd)
    {
        ingredients.Push(ingredientToAdd);
    }
}
