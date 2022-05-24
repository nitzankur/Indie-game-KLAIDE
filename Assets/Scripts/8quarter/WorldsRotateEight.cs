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
     private float baseAngle ;
     private Vector3 pos;
     private Vector2 mousePosStart;
     [SerializeField] private float step =10;
     [SerializeField] private GameObject left,button,top,leftInside,rightInside,topInside,buttonInside;


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
        if (Input.GetMouseButtonDown(0))
        {
            mousePosStart = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
            var localpos = new Vector2(transform.position.x, transform.position.y);
            pos = mousePosStart - localpos;
        }

        if (Input.GetMouseButton(0))
        {
            if (PlayerControllerEight.EndOfPath && Time.time - startTime > 0.3f)
            {
                Vector2 mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
                var localpos = new Vector2(transform.position.x, transform.position.y);
                Vector2 tempPos = mousePos - localpos;
                if (!WorldsManagerEight.onLeft) RotateWorld(left, tempPos); 
                    if (!WorldsManagerEight.onRight) RotateWorld(gameObject, tempPos);  
                    if(!WorldsManagerEight.onTop) RotateWorld(top, tempPos);  
                    if(!WorldsManagerEight.onBottom) RotateWorld(button, tempPos); 
                    
                    if (!WorldsManagerEight.onInsideLeft) RotateInsideWorld(leftInside, tempPos); 
                    if (!WorldsManagerEight.onInsideRight) RotateInsideWorld(rightInside, tempPos);  
                    if(!WorldsManagerEight.onInsideTop) RotateInsideWorld(topInside, tempPos); 
                    if(!WorldsManagerEight.onInsideBottom) RotateInsideWorld(buttonInside, tempPos);
                    pos = tempPos;
            }
        }
    }
    private void RotateWorld(GameObject side, Vector3 tempPos)
    {
        side.transform.rotation = Quaternion.RotateTowards(side.transform.rotation,   Quaternion.FromToRotation(pos, tempPos) * side.transform.rotation, step);
    }
    
    private void RotateInsideWorld(GameObject side, Vector3 tempPos)
    {
        side.transform.rotation = Quaternion.RotateTowards(side.transform.rotation,   Quaternion.FromToRotation(pos, tempPos) * side.transform.rotation, -step/5);
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
