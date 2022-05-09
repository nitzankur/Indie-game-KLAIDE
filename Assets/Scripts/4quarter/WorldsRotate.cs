using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldsRotate : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler,IDragHandler,IPointerClickHandler
{
    private bool _drag;
    private Vector3 _mousePos;




    public void OnBeginDrag(PointerEventData eventData)
    {
        _drag = true;
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }

    public void OnDrag(PointerEventData eventData)
    {
        var tmpPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if ((tmpPos.y >= _mousePos.y)&&(tmpPos.x<0 && Mathf.Abs(tmpPos.x) > Mathf.Abs(tmpPos.y)) 
            || (tmpPos.x >= _mousePos.x)&& (tmpPos.y > 0 && tmpPos.y >= Mathf.Abs(tmpPos.x))||
            (tmpPos.y <= _mousePos.y)&& (tmpPos.x > 0 && tmpPos.x > Mathf.Abs(tmpPos.y))|| (tmpPos.x<=_mousePos.x)&&
            (tmpPos.y < 0 &&  Mathf.Abs(tmpPos.x) <= Mathf.Abs(tmpPos.y)) )
        {
            print("Right dragging");
            WorldsManager.DragRight = true;
        }
        else if((tmpPos.y < _mousePos.y)&&(tmpPos.x<0 && Mathf.Abs(tmpPos.x) > Mathf.Abs(tmpPos.y))||
                (tmpPos.x < _mousePos.x)&& (tmpPos.y > 0 && tmpPos.y >= Mathf.Abs(tmpPos.x))||
                (tmpPos.y >= _mousePos.y)&& (tmpPos.x > 0 && tmpPos.x > Mathf.Abs(tmpPos.y))||
                (tmpPos.x<=_mousePos.x)&& (tmpPos.y < 0 &&  Mathf.Abs(tmpPos.x) <= Mathf.Abs(tmpPos.y)))
        {
            print("left dragging");
            WorldsManager.DragLeft = true;
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        WorldsManager.DragRight = false;
        WorldsManager.DragLeft = false;
        _drag = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_drag)
        {
            WorldsManager.CharacterMove = true;
           _drag = true;
        }
        
    }

    private void RightDragging(Vector3 tmpPos)
    {
        
    }

    private void LeftDragging()
    {
        
    }


}
