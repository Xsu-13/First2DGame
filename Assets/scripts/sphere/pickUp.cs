using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class pickUp : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] GameObject craftObj;
    public GameObject itemButton;
    [SerializeField] TMP_Text countText;
    [SerializeField] TMP_Text notion;
    bool pickPotion = false;
    int count = 0;
    Potion ingredientScript;
    List<Ingredient.Type> types = new List<Ingredient.Type>();

    
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        
    }

    private void Update()
    {
        //Не работает
        if(pickPotion)
        {
            Invoke("HideNotion", 2f);
            pickPotion = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Potion potionSc = craftObj.GetComponent<Potion>();
            potionSc.count += 1;
            notion.text = potionSc.Name + " x 1";
            pickPotion = true;

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
                    break;
                }
            }

            Destroy(gameObject);
            
        }
    }

    void HideNotion()
    {
        notion.text = "";
    }
}
