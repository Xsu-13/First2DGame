using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Poitions", order = 1)]
public class ScObjPotion : ScriptableObject
{

    public int potionScore;
    public string description;
    public int count;

    public PotionType type;
    public string Name;

}

