using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PotionDragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public  Vector3 startPos;
    public static Transform startParent;
    public static GameObject itemBeingDrages;

    //Changes
    GameObject image;
    Sprite sprite;
    public Transform parentObj;
    public Vector3 startPosition;

    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        startPos = transform.position;
        startParent = transform.parent;
        itemBeingDrages = gameObject;

        //Changes
        /*
        image = new GameObject();
        image.transform.parent = canvas.transform;
        rectTransform = image.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(60, 60);
        image.AddComponent<Image>();
        image.GetComponent<Image>().sprite = sprite;
        */

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


    void Start()
    {
        //Changes
        //sprite = GetComponent<Image>().sprite;

        startPos = transform.position;
        startPosition = startPos;
        parentObj = transform.parent;
    }


    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventdata)
    {

    }


}
