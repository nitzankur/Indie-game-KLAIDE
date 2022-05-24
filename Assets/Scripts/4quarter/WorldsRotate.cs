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
     private Vector3 screenPos;
     private bool _drag;
     private float startTime;
     private float baseAngle ;
     private Vector3 pos;
     private Vector2 mousePosStart;
     [SerializeField] private float step =10;
     [SerializeField] private GameObject left,button,top;


     private void Start()
    {
        WorldsManager.CharacterMove = false;
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
            if (PlayerControllerFour.EndOfPath && Time.time - startTime > 0.3f)
            {
                var ClockDrag = false;
                Vector2 mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
                var localpos = new Vector2(transform.position.x, transform.position.y);
                Vector2 tempPos = mousePos - localpos;
                float ang = Vector2.Angle(pos, tempPos);
                if (!WorldsManager.onButtom) RotateWorld(button,tempPos);
                if (!WorldsManager.onLeft) RotateWorld(left,tempPos);
                if (!WorldsManager.onRight) RotateWorld(gameObject,tempPos); 
                if (!WorldsManager.onTop) RotateWorld(top,tempPos);

                pos = tempPos;
            }
        }
    }
    private void RotateWorld(GameObject side, Vector3 tempPos)
    {
        side.transform.rotation = Quaternion.RotateTowards(side.transform.rotation,   Quaternion.FromToRotation(pos, tempPos) * side.transform.rotation, step);
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
