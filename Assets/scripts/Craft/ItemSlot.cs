using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
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


    public void OnDrop(PointerEventData eventData)
    {
        /*if (!item)
        {           
            DragDrop.itemBeingDrages.transform.SetParent(transform);
            
        }  */ 
    }

}
