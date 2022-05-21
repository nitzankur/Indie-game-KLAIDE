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
     private float baseAngle ;
     private Vector3 pos;
     private bool rotateToRight;
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
                // transform.rotation = Quaternion.RotateTowards(transform.rotation,   Quaternion.FromToRotation(pos, tempPos) * transform.rotation, 1); // TODO: step really high, to not matter at all
                // // Wher target rotation is one of the quaternions we describe below?
                // // 1.
                // transform.rotation = Quaternion.FromToRotation(pos, tempPos) * transform.rotation;
                // // 2.
                // float ang = Vector2.SignedAngle(pos, tempPos); 
                // // // 2.1
                print(mousePos + " mousePos "+ mousePosStart + "mouse pos start ");
                if (mousePos.x > 0 && (mousePos.y > mousePosStart.y) || (mousePos.y < 0 &&
                                                                          mousePos.x > mousePosStart.x)
                                                                      || mousePos.x < 0 &&
                                                                      mousePos.y < mousePosStart.y
                                                                      || mousePos.y > 0 &&
                                                                      mousePos.x < mousePosStart.x)
                {
                    // if (ClockDrag) ang *= -1;
                    // else ang = ang;
                    mousePosStart = mousePos;
                }
                else if (mousePos.x > 0 && (mousePos.y < mousePosStart.y) || (mousePos.y < 0 && mousePos.x < mousePosStart.x)
                                                                     || mousePos.x < 0 && mousePos.y > mousePosStart.y
                                                                     || mousePos.y > 0 && mousePos.x > mousePosStart.x)
                {
                    ClockDrag = true;
                    ang *= -1;
                    
                }

                
                if (!WorldsManagerToturial.onLeft) left.transform.rotation = Quaternion.Euler(0,0,ang) * left.transform.rotation;
                if (!WorldsManagerToturial.onRight) transform.rotation = Quaternion.Euler(0,0,ang) * transform.rotation;
                // // 2.2
                // transform.rotation = Quaternion.AngleAxis(ang, Vector3.forward)* transform.rotation;
                // // // 3. the easiest option probably: <<<<<<<<<<<<< - //this to much faster
                // transform.Rotate(0,0,ang, Space.World);
	
                // Also worth looking at: https://docs.unity3d.com/ScriptReference/Transform.html
                pos = tempPos;
            }
            
        }
    }
    private void OnMouseDown()
    {
      
    }

    private void OnMouseDrag()
    {
       
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
