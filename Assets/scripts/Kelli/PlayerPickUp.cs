using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPickUp : MonoBehaviour
{

    Inventory inventorySc;
    //public GameObject test;
    int indexForCount;

    //test
    public GameObject notionPrefab;
    public GameObject notionsParent;
    public GameObject testPotion;

    void Start()
    {
        inventorySc = GameObject.FindGameObjectWithTag("inventory").GetComponent<Inventory>();
    }

    void Update()
    {
        //test
        if(Input.GetKeyDown(KeyCode.G))
        {
            AddPotion(testPotion);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("ingredient"))
        {
            AddIngredient(collision.gameObject);
            /*
            ingredientObj ingredientObjSc = collision.GetComponent<ingredientObj>();
            ingredientObjSc.ingSc.count += 1;
            Ingredient ingredientSc = ingredientObjSc.craftObj.GetComponent<Ingredient>();
            notion.text = ingredientSc.Name + " x 1";
            */
            //ingredientSc.count += 1;
            Destroy(collision.gameObject);


            //Invoke("HideNotion", 2f);
        }

        if(collision.CompareTag("potion"))
        {
          
            
            pickUp pickUpSc = collision.GetComponent<pickUp>();
            Potion potionSc = pickUpSc.craftObj.GetComponent<Potion>();
            //potionSc.count.Value += 1;

            AddPotion(potionSc.gameObject);
            //как то заменить!!
            /*
            pickUpSc.potSc.count += 1;
            notion.text = potionSc.Name + " x 1";
            */
            
            if (CheckType(pickUpSc.type))
            {
                //ѕовтор код
                //проверка на наличие уже подобного зель€ в инвентаре
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
            
        }
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

    public void AddIngredient(GameObject ing)
    {
        GameObject createdNotion = Instantiate(notionPrefab, new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f));
        createdNotion.transform.SetParent(notionsParent.transform);
        TMP_Text notionText = createdNotion.GetComponent<TMP_Text>();


            ingredientObj ingredientObjSc = ing.GetComponent<ingredientObj>();
            ingredientObjSc.ingSc.count += 1;
            notionText.text = ingredientObjSc.ingSc.Name + " x 1";

        Ingredient ingredientSc = ingredientObjSc.craftObj.GetComponent<Ingredient>();

            ingredientSc.count += 1;

    }
    public void AddPotion(GameObject potion)
    {
        GameObject createdNotion = Instantiate(notionPrefab, new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f));
        createdNotion.transform.SetParent(notionsParent.transform);
        TMP_Text notionText = createdNotion.GetComponent<TMP_Text>();

        // pickUp pickUpSc = potion.GetComponent<pickUp>();
        //Potion potionSc = pickUpSc.craftObj.GetComponent<Potion>();
        //potionSc.count.Value += 1;
        Potion potionSc = potion.GetComponent<Potion>();
        potionSc.scPot.count += 1;
        notionText.text = potionSc.scPot.Name + " x 1";

    }
}
