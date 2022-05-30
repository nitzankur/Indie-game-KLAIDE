using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldsRotateEight :  MonoBehaviour , IBeginDragHandler, IEndDragHandler, IDragHandler,IPointerDownHandler, IPointerClickHandler
{
    #region variabales
     public static bool EndDrag,StartDrag;
     private bool _drag;
     private float startTime;
     private Vector3 _pos;
     private Vector2 _mousePosStart;
     [SerializeField] private float step =10;
     [SerializeField] private GameObject left,right,button,top,leftInside,rightInside,topInside,buttonInside;


     private void Start()
    {
        WorldsManagerEight.CharacterMove = false;
        EndDrag = true;

    }
    #endregion
    
    #region World rotate
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _mousePosStart = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
            var localpos = new Vector2(transform.position.x, transform.position.y);
            _pos = _mousePosStart - localpos;
            startTime = Time.time;
        }

        if (Input.GetMouseButton(0))
        {
            if (PlayerControllerEight.EndOfPath && Time.time - startTime > 0.3f)
            {
              
                Vector2 mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
                var localpos = new Vector2(transform.position.x, transform.position.y);
                Vector2 tempPos = mousePos - localpos;
                if (!WorldsManagerEight.onLeft) RotateWorld(left, tempPos); 
                if (!WorldsManagerEight.onRight) RotateWorld(right, tempPos);  
                if(!WorldsManagerEight.onTop) RotateWorld(top, tempPos);  
                if(!WorldsManagerEight.onBottom) RotateWorld(button, tempPos); 
                
                if (!WorldsManagerEight.onInsideLeft) RotateInsideWorld(leftInside, tempPos); 
                if (!WorldsManagerEight.onInsideRight) RotateInsideWorld(rightInside, tempPos);  
                if(!WorldsManagerEight.onInsideTop) RotateInsideWorld(topInside, tempPos); 
                if(!WorldsManagerEight.onInsideBottom) RotateInsideWorld(buttonInside, tempPos);
                _pos = tempPos;
            }
        }
    }
    private void RotateWorld(GameObject side, Vector3 tempPos)
    {
        side.transform.rotation = Quaternion.RotateTowards(side.transform.rotation,   Quaternion.FromToRotation(_pos, tempPos) * side.transform.rotation, step);
    }
    
    private void RotateInsideWorld(GameObject side, Vector3 tempPos)
    {
        side.transform.rotation = Quaternion.RotateTowards(side.transform.rotation,   Quaternion.FromToRotation(_pos, tempPos) * side.transform.rotation, -step/5);
    }
    
    
    #endregion

    #region sperate between walking and worlds rotate
    public void OnPointerDown(PointerEventData eventData)
    {
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_drag)
        {
            EndDrag = true;
            WorldsManagerEight.CharacterMove = true;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _drag = true;
        StartDrag = true;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
    
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _drag = false;
        StartDrag = false;
    }

    #endregion


   
}
