using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandwich : MonoBehaviour
{
    Stack<Ingredient> ingredients;

    public Sandwich(Stack<Ingredient> ingredients)
    {
        this.ingredients = ingredients;
    }

    public void AddIngredient(Ingredient ingredientToAdd)
    {
        ingredients.Push(ingredientToAdd);
    }
}
