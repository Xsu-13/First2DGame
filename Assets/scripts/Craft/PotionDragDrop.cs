using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PotionDragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public static Vector3 startPos;
    public static Transform startParent;
    public  Transform homeSlot;

    public static GameObject itemBeingDrages;

    public Transform parentObj;
    public Vector3 startPosition;
    Potion pot;

    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        pot = GetComponent<Potion>();
    }

    void Start()
    {

        startPos = transform.position;
        startPosition = startPos;
        parentObj = transform.parent;
        homeSlot = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        startPos = transform.position;
        startParent = transform.parent;
        itemBeingDrages = gameObject;

    }

    public void OnDrag(PointerEventData eventdata)
    {
        
            rectTransform.anchoredPosition += eventdata.delta / canvas.scaleFactor;
    }



    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if(transform.parent == startParent)
            transform.position = startPos;
        itemBeingDrages = null;
    }





    void Update()
    {

    }

}
