using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class pickUp : MonoBehaviour
{
    private Inventory inventory;
    public GameObject itemButton;
    [SerializeField] TMP_Text countText;
    int count = 0;
    Potion ingredientScript;
    List<Ingredient.Type> types = new List<Ingredient.Type>();

    
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i=0; i< inventory.slots.Length; i++)
            {
                //if()
                //{

                //}
                if(inventory.isFull[i] == false)
                {
                    countText = inventory.count[i].GetComponent<TMP_Text>();
                    inventory.isFull[i] = true;
                    countText.text = (count + 1).ToString();
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    //types.Add(ingredientScript.type);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
