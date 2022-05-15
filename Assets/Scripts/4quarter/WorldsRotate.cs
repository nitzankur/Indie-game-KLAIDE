using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldsRotate :  MonoBehaviour , IBeginDragHandler, IEndDragHandler, IDragHandler,IPointerDownHandler, IPointerClickHandler
{
    #region variables
     public static bool endDrag;
     private Vector3 _mousePos;
     private Camera myCam;
     private Vector3 screenPos;
     public static float angleOffset;
     private Collider2D col;
     private bool _drag;
     public static bool rotateOnce;


     private void Start()
    {
        rotateOnce = false;
        WorldsManager.CharacterMove = false;
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
        { if (col == Physics2D.OverlapPoint(mousePos))
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
                if (PlayerControllerFour.EndOfPath)
                {
                    rotateOnce = true;
                    if (!WorldsManager.onLeft) WorldsManager.LeftRotate(angle, angleOffset); 
                    if (!WorldsManager.onRight) WorldsManager.RightRotate(angle, angleOffset); 
                    if(!WorldsManager.onTop) WorldsManager.TopRotate(angle, angleOffset);
                    if(!WorldsManager.onButtom) WorldsManager.ButtomRotate(angle, angleOffset);
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
            WorldsManager.CharacterMove = true;
            
            
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
        print("end drag");
        _drag = false;
        endDrag = true;
    }
   
    
    #endregion


   
}
