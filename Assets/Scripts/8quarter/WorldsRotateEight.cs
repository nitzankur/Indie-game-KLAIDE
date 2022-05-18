using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldsRotateEight :  MonoBehaviour , IBeginDragHandler, IEndDragHandler, IDragHandler,IPointerDownHandler, IPointerClickHandler
{
    #region variables
     public static bool endDrag;
     private Vector3 _mousePos;
     private Camera myCam;
     private Vector3 screenPos;
     private static float angleOffset;
     private Collider2D col;
     private bool _drag;
     private float startTime;


     private void Start()
    {
        WorldsManagerEight.CharacterMove = false;
        myCam = Camera.main;
        col = GetComponent<Collider2D>();
    }
    #endregion
    
    #region World rotate
    void Update()
    {
        Vector3 mousePos = myCam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            startTime = Time.time;
            if (col == Physics2D.OverlapPoint(mousePos))
            { 
                screenPos = myCam.WorldToScreenPoint(transform.position);
                var vec3 = Input.mousePosition - screenPos;
                angleOffset = (Mathf.Atan2(transform.right.y, transform.right.x) - Mathf.Atan2(vec3.y, vec3.x)) *
                              Mathf.Rad2Deg;
            }
        }
        if (Input.GetMouseButton(0))
        {   
            if (col == Physics2D.OverlapPoint(mousePos))
            {
                Vector3 vec3 = Input.mousePosition - screenPos;
                float angle = Mathf.Atan2(vec3.y, vec3.x) * Mathf.Rad2Deg;
                if (PlayerControllerEight.EndOfPath && (Time.time - startTime > 0.2f))
                {
                    if (!WorldsManagerEight.onLeft) WorldsManagerEight.LeftRotate(angle, angleOffset); 
                    if (!WorldsManagerEight.onRight) WorldsManagerEight.RightRotate(angle, angleOffset); 
                    if(!WorldsManagerEight.onTop) WorldsManagerEight.TopRotate(angle, angleOffset);
                    if(!WorldsManagerEight.onBottom) WorldsManagerEight.ButtomRotate(angle, angleOffset);
                    
                    if (!WorldsManagerEight.onInsideLeft) WorldsManagerEight.LeftInsideRotate(-angle, angleOffset); 
                    if (!WorldsManagerEight.onInsideRight) WorldsManagerEight.RightInsideRotate(-angle, angleOffset); 
                    if(!WorldsManagerEight.onInsideTop) WorldsManagerEight.TopInsideRotate(-angle, angleOffset);
                    if(!WorldsManagerEight.onInsideBottom) WorldsManagerEight.ButtomInsideRotate(-angle, angleOffset);
                }
            }
        }
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
            WorldsManagerEight.CharacterMove = true;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _drag = true;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
    
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("end drag");
        _drag = false;
        endDrag = true;
    }

    #endregion


   
}
