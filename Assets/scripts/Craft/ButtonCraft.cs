using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonCraft : MonoBehaviour
{
    //public Button button;
    public int Sum;
    int score;
    [SerializeField] GameObject createdPotion;
    [SerializeField] GameObject[] sprites;
    

    [SerializeField] Transform[] slots;
    [SerializeField] bool isEmpty;
    [SerializeField] Sprite emptySprite;
    int localIndex;

    [SerializeField] GameObject craftInventory;
    Inventory inventory;

    void Start()
    {
        isEmpty = true;

        if (craftInventory != null)
        {
            inventory = craftInventory.GetComponent<Inventory>();
        }
        else
            Debug.Log("Inventory System does not find");
    }


    void Update()
    {
        
    }
    public void HasChanged()
    {
        if (slots[0].GetComponent<ItemSlot>().SlotIsEmpty || slots[1].GetComponent<ItemSlot>().SlotIsEmpty)
            isEmpty = true;
        else if (slots[0].GetComponent<ItemSlot>().SlotIsEmpty == false || slots[1].GetComponent<ItemSlot>().SlotIsEmpty == false)
            isEmpty = false;

        if (isEmpty)
        {
            Debug.Log("IsEmpty!");
        }
        else
        {
            foreach (Transform slotTransform in slots)
            {
                ItemSlot itemSlotScript = slotTransform.GetComponent<ItemSlot>();
                
                GameObject slot = itemSlotScript.item;
                score = slot.GetComponent<Ingredient>().score;

                Sum += score;
               
            }
            if (isEmpty == false)
            {
                createdPotion.SetActive(true);

                //�������� ����� � ����������� �� ���������� �����
                switch (Sum)
                {
                    case 2:
                        Change(0);
                        break;
                    case 3:
                        Change(1);
                        break;
                    case 4:
                        Change(2);
                        break;
                    case 5:
                        Change(3);
                        break;
                    case 6:
                        Change(4);
                        break;
                    case 7:
                        Change(5);
                        break;
                    case 8:
                        Change(6);
                        break;
                    case 9:
                        Change(7);
                        break;
                    case 10:
                        Change(8);
                        break;
                    case 11:
                        Change(9);
                        break;
                    case 12:
                        Change(10);
                        break;
                    case 13:
                        Change(11);
                        break;


                }
                Sum = 0;

                foreach (Transform slotTransform in slots)
                {
                    slotTransform.GetComponent<ItemSlot>().SlotIsEmpty = true;
                    GameObject slot = slotTransform.transform.GetChild(0).gameObject;
                    slot.transform.SetParent(slot.GetComponent<Ingredient>().startParent);

                    slotTransform.GetComponent<Image>().sprite = emptySprite;
                }

            }
        }
    }

    void Change(int index)
    {       
        createdPotion.GetComponent<Image>().sprite = sprites[index].GetComponent<Image>().sprite;
        this.localIndex = index;
        Invoke("Hide", 2f);
    }

    void Hide()
    {
        Potion potion = sprites[this.localIndex].GetComponent<Potion>();
        createdPotion.GetComponent<Image>().sprite = emptySprite;
        potion.count.Value += 1;

        // ���� ������
        int countType = 0;
        foreach(PotionType type in inventory.types)
        {
            
            if(potion.type == type)
            {
                inventory.countInt[countType] += 1;
                inventory.count[countType].GetComponent<TMP_Text>().text = inventory.countInt[countType].ToString();
            }
            countType += 1;
        }
    }
}


