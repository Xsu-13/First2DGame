using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventdata)
    {
        //Debug.Log("PointDown");
    }
    public void OnEndDrag(PointerEventData eventdata)
    {
        //Debug.Log("endDrag");
        canvasGroup.blocksRaycasts = true;
    }
    public void OnBeginDrag(PointerEventData eventdata)
    {
        //Debug.Log("beginDrag");
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventdata)
    {
        rectTransform.anchoredPosition += eventdata.delta/ canvas.scaleFactor;
    }

    public void OnDrop(PointerEventData eventData)
    {
       
    }
}
