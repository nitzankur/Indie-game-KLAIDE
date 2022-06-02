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
    }
    #endregion
    
    #region World rotate
    void Update()
    {
        print(_drag + "drag");
        if (Input.GetMouseButtonDown(0))
        {
            _mousePosStart = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
            var localpos = new Vector2(transform.position.x, transform.position.y);
            _pos = _mousePosStart - localpos;
            _startTime = Time.time;
            if (!_drag)
            {
                WorldsManagerToturial.CharacterMove = true;
            }
        }

        if (Input.GetMouseButton(0))
        {
            RotateOnce = false;
            _drag = true;
            EndDrag = false;
            if (PlayerController.EndOfPath && Time.time - _startTime > 0.3f)
            {
                Vector2 mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
                var localpos = new Vector2(transform.position.x, transform.position.y);
                Vector2 tempPos = mousePos - localpos;
                float ang = Vector2.Angle(_pos, tempPos);
                if (!WorldsManagerToturial.onLeft) left.transform.rotation = Quaternion.RotateTowards
                    (left.transform.rotation,   Quaternion.FromToRotation(_pos, tempPos) * left.transform.rotation, step); // TODO: step really high, to not matter at all
                if (!WorldsManagerToturial.onRight) transform.rotation = Quaternion.RotateTowards
                    (transform.rotation,   Quaternion.FromToRotation(_pos, tempPos) * transform.rotation, step);
                _pos = tempPos;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            RotateOnce = true;
            _drag = false;
            EndDrag = true; 
        }
    }
  

    #endregion

    #region sperate between walking and worlds rotate
    public void OnPointerDown(PointerEventData eventData)
    {
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
       
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
       
      //  print("begin drag");
    
    }
    public void OnDrag(PointerEventData eventData)
    {
    
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        print("end drag");
       
    }
   
    
    #endregion


   
}
