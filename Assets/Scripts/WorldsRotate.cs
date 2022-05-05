using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldsRotate : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler,IDragHandler,IPointerClickHandler
{
    private bool drag;


    // Update is called once per frame


    public void OnBeginDrag(PointerEventData eventData)
    {
        drag = true;
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
        drag = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!drag)
        {
           print("character should move"); 
           WorldsManager.CharacterMove = true;
           drag = true;
        }
        
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
