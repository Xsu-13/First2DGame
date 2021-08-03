using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReturnIngr : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameObject parentSlot;
    Ingredient ingrSc;


    void Start()
    {
        ingrSc = GetComponent<Ingredient>();
    }

    
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventdata)
    {
        /*
        if (ingrSc.inCreateSlot)
        {
            this.transform.SetParent(parentSlot.transform);
        }
        */
    }

}