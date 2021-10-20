using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Potion : MonoBehaviour
{
    
    public int potionScore;
    public string description;
    public int count;
    
    [SerializeField] TMP_Text countText;
    public string Name;
    public GameObject inventorySprite;
    public PotionType type;

    void Start()
    {
        countText.text = count.ToString();
    }

    
    void Update()
    {
        countText.text = count.ToString();
    }

    
}

public enum PotionType
{
    type0,
    type1,
    type2,
    type3,
    type4,
    type5,
    type6,
    type7,
    type8,
    type9,
    type10,
    type11,
    type12
}
