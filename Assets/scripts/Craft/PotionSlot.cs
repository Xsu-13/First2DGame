using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PotionSlot : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    public GameObject potionItem;
    public bool isEmpty = true;
    public Transform another;

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

    /*
    public bool isEmty
    {
        get
        {
            if (transform.childCount > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    */
    public int count;


    public void OnDrop(PointerEventData eventData)
    {
       
            if (count > 0)
            {
                another = transform.GetChild(0).transform;
                if (PotionDragDrop.startParent.GetComponent<SlotType>().slotTp == slotType.stock)
                {
                    another.transform.SetParent(another.GetComponent<PotionDragDrop>().homeSlot);
                    another.transform.position = another.GetComponent<PotionDragDrop>().homeSlot.position;
                }
                else
                {
                    another.transform.SetParent(PotionDragDrop.startParent);
                    another.transform.position = PotionDragDrop.startParent.position;
                }


                PotionDragDrop.itemBeingDrages.transform.SetParent(transform);
                PotionDragDrop.itemBeingDrages.transform.position = transform.position;

            }
            if (count == 0)
            {
                PotionDragDrop.itemBeingDrages.transform.SetParent(transform);
                PotionDragDrop.itemBeingDrages.transform.position = transform.position;

            }
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (count > 0 && eventData.button == PointerEventData.InputButton.Right)
        {
            potionItem = transform.GetChild(0).gameObject;
            potionItem.transform.SetParent(potionItem.GetComponent<PotionDragDrop>().parentObj);
            potionItem.transform.position = potionItem.GetComponent<PotionDragDrop>().startPosition;

        }
    }

    public void Update()
    {
        count = transform.childCount;
    }
}
