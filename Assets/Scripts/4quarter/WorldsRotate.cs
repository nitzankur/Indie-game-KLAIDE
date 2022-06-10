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
     private float timeForStart;


     private void Start()
     {
         timeForStart = 0;
        WorldsManager.CharacterMove = false;
    }
    #endregion
    
    #region World rotate
    void Update()
    {
        if (timeForStart < 4.8)
        {
            timeForStart += Time.deltaTime;
            return;
        }    
        if (Input.GetMouseButtonDown(0))
        {
            _mousePosStart = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
            var localpos = new Vector2(transform.position.x, transform.position.y);
            _pos = _mousePosStart - localpos;
            _startTime = Time.time;
        }

        if (Input.GetMouseButton(0))
        {
            if ((PlayerControllerFour.EndOfPath || PlayerControllerFour.Drag) && Time.time - _startTime > 0.3f && LevelManager.Level != 5 || !PlayerControllerFour.PortalON)
            {   
                Vector2 mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
                var localpos = new Vector2(transform.position.x, transform.position.y);
                Vector2 tempPos = mousePos - localpos;
                if (!WorldsManager.onButtom) RotateWorld(button,tempPos);
                if (!WorldsManager.onLeft) RotateWorld(left,tempPos);
                if (!WorldsManager.onRight) RotateWorld(gameObject,tempPos); 
                if (!WorldsManager.onTop) RotateWorld(top,tempPos);

                _pos = tempPos;
                GetComponent<AudioSource>().Stop();
            }
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
        EndDrag = true;
    }
   
    
    #endregion


   
}
