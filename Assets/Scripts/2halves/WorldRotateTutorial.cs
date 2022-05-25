using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldRotateTutorial : MonoBehaviour , IBeginDragHandler, IEndDragHandler, IDragHandler,IPointerDownHandler, IPointerClickHandler
{
     #region variables
     public static bool EndDrag;
     private float _startTime;
     private bool _drag;
     public static bool RotateOnce;
     [SerializeField] private GameObject left;
     [SerializeField] private float step =10;
     private Vector3 _pos;
     private Vector2 _mousePosStart;


     private void Start()
    {
        RotateOnce = false;
        WorldsManagerToturial.CharacterMove = false;
        print("Pos" + _pos);
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
            _startTime = Time.time;
        }

        if (Input.GetMouseButton(0))
        {
            if (PlayerController.EndOfPath && Time.time - _startTime > 0.3f)
            {
                var ClockDrag = false;
                Vector2 mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
                var localpos = new Vector2(transform.position.x, transform.position.y);
                Vector2 tempPos = mousePos - localpos;
                // One of below: // TODO: add +/- factor based on side, if we need to? im not sure we do
                // // 0.
                float ang = Vector2.Angle(_pos, tempPos);
                if (!WorldsManagerToturial.onLeft) left.transform.rotation = Quaternion.RotateTowards
                    (left.transform.rotation,   Quaternion.FromToRotation(_pos, tempPos) * left.transform.rotation, step); // TODO: step really high, to not matter at all
                if (!WorldsManagerToturial.onRight) transform.rotation = Quaternion.RotateTowards
                    (transform.rotation,   Quaternion.FromToRotation(_pos, tempPos) * transform.rotation, step); 

                _pos = tempPos;
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
        RotateOnce = true;
        _drag = false;
        EndDrag = true;
    }
   
    
    #endregion


   
}
