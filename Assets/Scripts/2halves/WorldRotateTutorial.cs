using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldRotateTutorial : MonoBehaviour , IBeginDragHandler, IEndDragHandler, IDragHandler,IPointerDownHandler, IPointerClickHandler
{
     #region variables
     public static bool endDrag;
     private Vector3 _mousePos;
     private Camera myCam;
     private Vector3 screenPos;
     public static float angleOffset;
     private float startTime;
     private Collider2D col;
     private bool _drag;
     public static bool rotateOnce;


     private void Start()
    {
        rotateOnce = false;
        WorldsManagerToturial.CharacterMove = false;
        myCam = Camera.main;
        print(Camera.main);
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
            { screenPos = myCam.WorldToScreenPoint(transform.position);
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
                if (PlayerController.EndOfPath && Time.time - startTime > 0.3f)
                {
                    rotateOnce = true;
                    if (!WorldsManagerToturial.onLeft) WorldsManagerToturial.LeftRotate(angle, angleOffset); 
                    if (!WorldsManagerToturial.onRight) WorldsManagerToturial.RightRotate(angle, angleOffset); 
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
            WorldsManagerToturial.CharacterMove = true;
            
            
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _drag = true;
      //  print("begin drag");
    
    }
    public void OnDrag(PointerEventData eventData)
    {
    
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //print("end drag");
        _drag = false;
        endDrag = true;
    }
   
    
    #endregion


   
}
