using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PotionSlot : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    public GameObject potionItem;
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
    public bool SlotIsEmpty = true;
    public int count;

    public void OnDrop(PointerEventData eventData)
    {
        if (!item)
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
