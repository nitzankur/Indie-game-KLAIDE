using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldsRotate : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler,IDragHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame


    public void OnBeginDrag(PointerEventData eventData)
    {
        print("start drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("dragging");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("end dragging");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("pointer down");
    }

    // private void OnMouseDown()
    // {
    //     print("mouse drag");
    // }
    //
    // private void OnMouseDrag()
    // {
    //     print("mouse down");
    // }
}
