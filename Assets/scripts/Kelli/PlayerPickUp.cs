using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] TMP_Text notion;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("ingredient"))
        {
            ingredientObj ingredientObjSc = collision.GetComponent<ingredientObj>();

            Ingredient ingredientSc = ingredientObjSc.craftObj.GetComponent<Ingredient>();
            notion.text = ingredientSc.Name + " x 1";
            ingredientSc.count += 1;
            Destroy(collision.gameObject);

            Invoke("HideNotion", 2f);
        }
        if(collision.CompareTag("potion"))
        {
            pickUp pickUpSc = collision.GetComponent<pickUp>();
            Potion potionSc = pickUpSc.craftObj.GetComponent<Potion>();
            potionSc.count += 1;
            notion.text = potionSc.Name + " x 1";

            for (int i = 0; i < pickUpSc.inventory.slots.Length; i++)
            {
                if (pickUpSc.inventory.isFull[i] == false)
                {
                    pickUpSc.countText = pickUpSc.inventory.count[i].GetComponent<TMP_Text>();
                    pickUpSc.inventory.isFull[i] = true;
                    pickUpSc.countText.text = (pickUpSc.count + 1).ToString();
                    Instantiate(pickUpSc.itemButton, pickUpSc.inventory.slots[i].transform, false);
                    //types.Add(ingredientScript.type);
                    break;
                }
            }

            Destroy(collision.gameObject);
            Invoke("HideNotion", 2f);
        }
    }

    void HideNotion()
    {
        notion.text = "";
    }
}
