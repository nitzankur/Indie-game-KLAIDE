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
     private Vector3 screenPos;
     private float startTime;
     private bool _drag;
     public static bool rotateOnce;
     private Vector3 startPos;
     [SerializeField] private GameObject left;
     [SerializeField] private float step =10;
     private float baseAngle ;
     private Vector3 pos;
     private Vector2 mousePosStart;


     private void Start()
    {
        rotateOnce = false;
        WorldsManagerToturial.CharacterMove = false;
        print("Pos" + pos);
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
            if (PlayerController.EndOfPath && Time.time - startTime > 0.3f)
            {
                var ClockDrag = false;
                Vector2 mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
                var localpos = new Vector2(transform.position.x, transform.position.y);
                Vector2 tempPos = mousePos - localpos;
                // One of below: // TODO: add +/- factor based on side, if we need to? im not sure we do
                // // 0.
                float ang = Vector2.Angle(pos, tempPos);
                rotateOnce = true;
                if (!WorldsManagerToturial.onLeft) left.transform.rotation = Quaternion.RotateTowards(left.transform.rotation,   Quaternion.FromToRotation(pos, tempPos) * left.transform.rotation, step); // TODO: step really high, to not matter at all
                if (!WorldsManagerToturial.onRight) transform.rotation = Quaternion.RotateTowards(transform.rotation,   Quaternion.FromToRotation(pos, tempPos) * transform.rotation, step); 

                pos = tempPos;
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
