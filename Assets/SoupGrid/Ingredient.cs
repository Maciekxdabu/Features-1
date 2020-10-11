using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Ingredient", menuName ="Scriptable Objects/Ingredient")]
public class Ingredient : ScriptableObject
{
    public Sprite sprite;
    public int value;
}
