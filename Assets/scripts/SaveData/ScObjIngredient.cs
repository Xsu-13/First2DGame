using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Ingredients", order = 1)]
public class ScObjIngredient : ScriptableObject
{
    public int score;
    public int count;
    public string Name;
    public TypeIng type;
}
