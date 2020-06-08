using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public enum Type { main, garnish, bread }
    public Type type;
    public string materialName;
}
