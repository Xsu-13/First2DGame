using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    Vector3 startPos;
    public static Transform startParent;
    public static GameObject itemBeingDrages;
    [SerializeField] Transform[] slots;
    Ingredient ingredientScript;
    GameObject ingr;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        
    }
    private void Start()
    {
        
    }
    public void OnBeginDrag(PointerEventData eventdata)
    {

        /*canvasGroup.blocksRaycasts = false;
        startPos = transform.position;*/
        //startParent = transform.parent;
        //itemBeingDrages = gameObject;
    }

    
    public void OnEndDrag(PointerEventData eventdata)
    {
        /*canvasGroup.blocksRaycasts = true;
        if(transform.parent == startParent)
            transform.position = startPos;
        itemBeingDrages = null;*/
    }
    
    public void OnDrag(PointerEventData eventdata)
    {
        //rectTransform.anchoredPosition += eventdata.delta/ canvas.scaleFactor;
    }
    public void OnPointerDown(PointerEventData eventdata)
    {

        GameObject ingr = transform.GetChild(0).gameObject;
        ingredientScript = ingr.GetComponent<Ingredient>();

        if (ingredientScript.count > 0)
        {

            if (slots[0].GetComponent<ItemSlot>().SlotIsEmpty)
            {
                ingr.transform.SetParent(slots[0]);
                slots[0].GetComponent<Image>().sprite = transform.GetComponent<Image>().sprite;
                slots[0].GetComponent<ItemSlot>().SlotIsEmpty = false;
                ingredientScript = ingr.GetComponent<Ingredient>();
                ingredientScript.count -= 1;

            }
            else if (slots[1].GetComponent<ItemSlot>().SlotIsEmpty)
            {
                ingr.transform.SetParent(slots[1]);
                slots[1].GetComponent<Image>().sprite = transform.GetComponent<Image>().sprite;
                slots[1].GetComponent<ItemSlot>().SlotIsEmpty = false;
                ingredientScript = ingr.GetComponent<Ingredient>();
                ingredientScript.count -= 1;
            }
            else Debug.Log("Busy!");
        }
        else Debug.Log("недостаточно ингредиентов");
    }

    public void OnDrop(PointerEventData eventData)
    {
       
    }

}
