using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//implement all interfaces required for the drag and drop system
public class DragDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //virtual method
        DragStart();

        //debug
        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //move the object
        rectTransform.anchoredPosition += eventData.delta;

        //virtual method
        OnMove();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //virtual method
        DragEnd();
    }

    //virtual methods
    public virtual void DragStart()
    {
        //do nothing
    }

    public virtual void DragEnd()
    {
        //do nothing
    }

    public virtual void OnMove()
    {
        //do nothing
    }

    public virtual void PointerDown()
    {
        //do nothing
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //virtual method
        PointerDown();
    }
}