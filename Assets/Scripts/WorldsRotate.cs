using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldsRotate : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler,IDragHandler
{


    // Update is called once per frame


    public void OnBeginDrag(PointerEventData eventData)
    {
       print("on Begin Drag"); 
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("dragging");
        WorldsManager.Drag = true;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("end dragging");
        WorldsManager.Drag = false;
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
