using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class WorldRotateTutorial : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler,IDragHandler,IPointerClickHandler
{
    public static bool endDrag;
    private bool _drag;
    private Vector3 _mousePos;


    private void Start()
    {
        WorldsManagerToturial.CharacterMove = false;
    }

    private void Update()
    { 
        //WorldsManagerToturial.CharacterMove = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _drag = true;
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        print("begin drag");

    }

    public void OnDrag(PointerEventData eventData)
    {
        var tmpPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if ((tmpPos.y >= _mousePos.y)&&(tmpPos.x<0)|| (tmpPos.y <= _mousePos.y)&&(tmpPos.x>0))
        {
            WorldsManagerToturial.DragRight = true;
        }
        else if((tmpPos.y >= _mousePos.y)&&(tmpPos.x>0)||(tmpPos.y <= _mousePos.y)&&(tmpPos.x<0))
        {
            WorldsManagerToturial.DragLeft = true;
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        WorldsManagerToturial.DragRight = false;
        WorldsManagerToturial.DragLeft = false;
        _drag = false;
        endDrag = true;
        print("end drag");

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_drag)
        {
            print("pointerClick");
            WorldsManagerToturial.CharacterMove = true;
        }
        
    }

    private void RightDragging(Vector3 tmpPos)
    {
        
    }

    private void LeftDragging()
    {
        
    }

}
