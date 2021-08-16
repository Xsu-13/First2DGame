using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReturnIngr : MonoBehaviour, IPointerDownHandler
{
    public bool isEmpty;
    [SerializeField] Sprite emptySprite;


    void Start()
    {
        isEmpty = true;
    }

    
    void Update()
    {
        if(transform.childCount > 0)
        {
            isEmpty = false;
        }
        else
        {
            isEmpty = true;
        }
    }

    public void OnPointerDown(PointerEventData eventdata)
    {
        if(!isEmpty)
        {
            GameObject ingr = transform.GetChild(0).gameObject;
            Ingredient ingrSc = ingr.GetComponent<Ingredient>();
            ingr.transform.SetParent(ingrSc.startParent);
            DragDrop dragDropSc = ingrSc.startParent.GetComponent<DragDrop>();
            transform.GetComponent<ItemSlot>().SlotIsEmpty = true;
            transform.GetComponent<Image>().sprite = emptySprite;
            ingrSc.count += 1;
        }
    }

}