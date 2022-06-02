using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldsRotate :  MonoBehaviour , IBeginDragHandler, IEndDragHandler, IDragHandler,IPointerDownHandler, IPointerClickHandler
{
    #region variables
     public static bool EndDrag;
     private bool _drag;
     private float _startTime;
     private Vector3 _pos;
     private Vector2 _mousePosStart;
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
            _mousePosStart = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
            var localpos = new Vector2(transform.position.x, transform.position.y);
            _pos = _mousePosStart - localpos;
            _startTime = Time.time;
            if (!_drag)
            {
                WorldsManager.CharacterMove = true;
            }
        }

        if (Input.GetMouseButton(0))
        {
            _drag = true;
            EndDrag = false;
            if (PlayerControllerFour.EndOfPath && Time.time - _startTime > 0.3f)
            {
                Vector2 mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
                var localpos = new Vector2(transform.position.x, transform.position.y);
                Vector2 tempPos = mousePos - localpos;
                if (!WorldsManager.onButtom) RotateWorld(button,tempPos);
                if (!WorldsManager.onLeft) RotateWorld(left,tempPos);
                if (!WorldsManager.onRight) RotateWorld(gameObject,tempPos); 
                if (!WorldsManager.onTop) RotateWorld(top,tempPos);

                _pos = tempPos;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            _drag = false;
            EndDrag = true; 
        }
    }
    private void RotateWorld(GameObject side, Vector3 tempPos)
    {
        side.transform.rotation = Quaternion.RotateTowards(side.transform.rotation,   Quaternion.FromToRotation(_pos, tempPos) * side.transform.rotation, step);
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
        _drag = true;
      //  print("begin drag");
    
    }
    public void OnDrag(PointerEventData eventData)
    {
    
    }
    public void OnEndDrag(PointerEventData eventData)
    {
    
    }
   
    
    #endregion


   
}
