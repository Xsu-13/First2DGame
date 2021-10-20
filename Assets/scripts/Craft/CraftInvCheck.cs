using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftInvCheck : MonoBehaviour
{
    GameObject inventoryOb;
    Inventory inventorySc;
    SlotType slotTypeSc;
    public GameObject[] slots;

    void Start()
    {
        inventoryOb = GameObject.FindGameObjectWithTag("inventory");
        inventorySc = inventoryOb.GetComponent<Inventory>();

    }


    void Update()
    {
        foreach (GameObject slot in slots)
        {
            slotTypeSc = slot.GetComponent<SlotType>();
            int index = slotTypeSc.slotIndex;

            if (slot.transform.childCount > 0)
            {
                
                inventorySc.isFull[index] = true;
            }
            else
            {
                inventorySc.isFull[index] = false;
            }
        }


    }
}
