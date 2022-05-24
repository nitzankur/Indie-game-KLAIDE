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
            Vector2 mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
            var pos = new Vector2(transform.position.x, transform.position.y);
            pos = mousePos - pos;
        }

        if (Input.GetMouseButton(0))
        {
            if (PlayerController.EndOfPath && Time.time - startTime > 0.3f)
            {
                Vector2 mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
                var pos = new Vector2(transform.position.x, transform.position.y);
                Vector2 tempPos = mousePos - pos;
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
        // if (Input.GetMouseButtonDown(0))
        // {
        //     // pos = Camera.main.WorldToScreenPoint(transform.position);
        //     // pos = Input.mousePosition - pos ;
        //     Vector3 mousePos = Camera.main.WorldToScreenPoint(Input.mousePosition);
        //     pos = mousePos - transform.position;
        //     // print("mousePos" + Input.mousePosition);
        //     // baseAngle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        //     // print("base angle" + baseAngle);
        //     // if (!WorldsManager.onRight){ baseAngle -= Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;}
        //     // if (!WorldsManager.onLeft) { baseAngle += Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg; }
        // }
        //
        // if (Input.GetMouseButton(0))
        // {
        //     Vector3 tmpPos = Camera.main.WorldToScreenPoint(Input.mousePosition);
        //     tmpPos = Input.mousePosition - tmpPos;
        //     
        //     // float ang = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg - baseAngle;
        //     rotateOnce = true;
        //     if (PlayerController.EndOfPath && Time.time - startTime > 0.3f)
        //     {
        //         if (!WorldsManagerToturial.onLeft) left.transform.rotation = Quaternion.FromToRotation(pos, tmpPos);
        //         if (!WorldsManagerToturial.onRight) transform.rotation = Quaternion.FromToRotation(pos, tmpPos);
        //     }
        // }

        //     // if (col == Physics2D.OverlapPoint(mousePos))
        //     // {
        //        // var mousePos1 = myCam.WorldToScreenPoint(Input.mousePosition);
        //         Vector3 curPos = Input.mousePosition - screenPos;
        //         // float angle = Mathf.Atan2(vec3.y, vec3.x) * Mathf.Rad2Deg;
        //         if (PlayerController.EndOfPath && Time.time - startTime > 0.3f)
        //         {
        //            
        //             startPos.z = 0;
        //             curPos.z = 0;
        //             if (!WorldsManagerToturial.onLeft) left.transform.rotation = Quaternion.FromToRotation(startPos, curPos);
        //             if (!WorldsManagerToturial.onRight) transform.rotation= Quaternion.FromToRotation(startPos, curPos);
        //             // if (!WorldsManagerToturial.onLeft) WorldsManagerToturial.LeftRotate(startPos, curPos); 
        //             // if (!WorldsManagerToturial.onRight) WorldsManagerToturial.RightRotate(startPos, curPos); 
        //             // if (!WorldsManagerToturial.onRight) WorldsManagerToturial.RightRotate(angle, angleOffset); 
        //         }
            //    
         //    // }
         // }
        
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
