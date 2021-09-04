using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PotionSlot : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    public GameObject potionItem;
    public bool isEmpty = true;
    public Transform another;
    GameObject inventoryOb;
    Inventory inventorySc;
    SlotType anotherSlotTypeSc;
    SlotType mySlotTypeSc;
    PlayerPickUp playerpu;
    public GameObject itemInInventory;

    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }

    }

    public int childCount;

    public void Start()
    {
        inventoryOb = GameObject.FindGameObjectWithTag("inventory");
        inventorySc = inventoryOb.GetComponent<Inventory>();
        mySlotTypeSc = GetComponent<SlotType>();

        playerpu = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPickUp>();
    }
    public void OnDrop(PointerEventData eventData)
    {
       
            if (childCount > 0)
            {
                another = transform.GetChild(0).transform;
                anotherSlotTypeSc = PotionDragDrop.startParent.GetComponent<SlotType>();
                //inventorySc.isFull[anotherSlotTypeSc.slotIndex] = false;
                
                //playerpu.RemoveInInventory();

                if (anotherSlotTypeSc.slotTp == slotType.stock)
                {
                    
                    another.transform.SetParent(another.GetComponent<PotionDragDrop>().homeSlot);
                    another.transform.position = another.GetComponent<PotionDragDrop>().homeSlot.position;
                    
                
                //playerpu.RemoveInInventory(another.GetComponent<PotionSlot>().itemInInventory);
            }
                else
                {
                    another.transform.SetParent(PotionDragDrop.startParent);
                    another.transform.position = PotionDragDrop.startParent.position;
                    playerpu.RemoveInInventory(anotherSlotTypeSc.slotIndex);
                    GameObject ob = playerpu.InstantiateInInventory(another.GetComponent<Potion>().inventorySprite, anotherSlotTypeSc.slotIndex, another.GetComponent<Potion>().type, another.GetComponent<Potion>().count);
                    inventorySc.count[anotherSlotTypeSc.slotIndex].GetComponent<TMP_Text>().text = inventorySc.countInt[anotherSlotTypeSc.slotIndex].ToString();
                }

            }

            PotionDragDrop.itemBeingDrages.transform.SetParent(transform);
            potionItem = transform.GetChild(0).gameObject;
            PotionDragDrop.itemBeingDrages.transform.position = transform.position;

        //inventorySc.isFull[mySlotTypeSc.slotIndex] = true;
            playerpu.RemoveInInventory(mySlotTypeSc.slotIndex);
            itemInInventory = playerpu.InstantiateInInventory(potionItem.GetComponent<Potion>().inventorySprite, mySlotTypeSc.slotIndex, potionItem.GetComponent<Potion>().type, potionItem.GetComponent<Potion>().count);
            inventorySc.count[mySlotTypeSc.slotIndex].GetComponent<TMP_Text>().text = inventorySc.countInt[mySlotTypeSc.slotIndex].ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (childCount > 0 && eventData.button == PointerEventData.InputButton.Right)
        {
            potionItem = transform.GetChild(0).gameObject;
            potionItem.transform.SetParent(potionItem.GetComponent<PotionDragDrop>().parentObj);
            potionItem.transform.position = potionItem.GetComponent<PotionDragDrop>().startPosition;
            inventorySc.count[mySlotTypeSc.slotIndex].GetComponent<TMP_Text>().text = "0";
            //inventorySc.isFull[mySlotTypeSc.slotIndex] = false;
            //itemInInventory
            playerpu.RemoveInInventory(mySlotTypeSc.slotIndex);
        }
    }

    public void Update()
    {
        childCount = transform.childCount;
    }
}
