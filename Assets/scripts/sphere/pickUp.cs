using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class pickUp : MonoBehaviour
{
    public Inventory inventory;
    public GameObject craftObj;
    public GameObject itemButton;
    public TMP_Text countText;
    public int count = 0;
    List<Ingredient.Type> types = new List<Ingredient.Type>();

    
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("inventory").GetComponent<Inventory>();
        
    }

}
