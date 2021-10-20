using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] TMP_Text notion;
    Inventory inventorySc;
    public GameObject test;
    int indexForCount;
    void Start()
    {
        inventorySc = GameObject.FindGameObjectWithTag("inventory").GetComponent<Inventory>();
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
            
            
            if (CheckType(pickUpSc.type))
            {
                //Повтор код
                //проверка на наличие уже подобного зелья в инвентаре
                pickUpSc.countText = pickUpSc.inventory.count[indexForCount].GetComponent<TMP_Text>();
                pickUpSc.countText.text = (pickUpSc.count + 1).ToString();
                pickUpSc.inventory.countInt[indexForCount] += 1;


            }
           
            else
            {
                for (int i = 0; i < pickUpSc.inventory.slots.Length; i++)
                {

                    if (pickUpSc.inventory.isFull[i] == false)
                    {
                        pickUpSc.countText = pickUpSc.inventory.count[i].GetComponent<TMP_Text>();
                        pickUpSc.inventory.isFull[i] = true;
                        pickUpSc.countText.text = (pickUpSc.count + 1).ToString();
                        Instantiate(pickUpSc.itemButton, pickUpSc.inventory.slots[i].transform, false);
                        pickUpSc.craftObj.transform.SetParent(inventorySc.craftInventory[i].transform);
                        pickUpSc.craftObj.transform.position = inventorySc.craftInventory[i].transform.position;
                        inventorySc.types[i] = pickUpSc.type;
                        //new
                        inventorySc.countInt[i] += 1;

                        break;
                    }
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

    public GameObject InstantiateInInventory(GameObject potionButton, int index, PotionType type, int count)
    {
        inventorySc.countInt[index] = count;
        inventorySc.types[index] = type;
        GameObject item = Instantiate(potionButton, inventorySc.slots[index].transform, false);
        return item;
    }

    public void RemoveInInventory(int index)
    {
        
        if(inventorySc.slots[index].transform.childCount > 1)
        {
            inventorySc.types[index] = PotionType.type0;
            Destroy(inventorySc.slots[index].transform.GetChild(1).gameObject);
        }        
    }

    bool CheckType(PotionType type)
    {
        for(int i = 0; i < inventorySc.types.Length; i++)
        {
            if(type.Equals(inventorySc.types[i]))
            {
                indexForCount = i;
                return true;
            }
        }
        return false;
    }
}
