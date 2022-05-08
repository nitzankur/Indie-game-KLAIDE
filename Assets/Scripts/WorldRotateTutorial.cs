using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class WorldRotateTutorial : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler,IDragHandler,IPointerClickHandler
{
    public static bool endDrag;
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
