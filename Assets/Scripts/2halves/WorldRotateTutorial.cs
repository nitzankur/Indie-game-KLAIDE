using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class WorldRotateTutorial : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler,
    IPointerClickHandler
{
    #region variables
    public static bool endDrag;
    private Vector3 _mousePos;
    private Camera myCam;
    private Vector3 screenPos;
    private float angleOffset;
    private Collider2D col;
    private bool _drag;
   

    private void Start()
    {
        WorldsManagerToturial.CharacterMove = false;
        myCam = Camera.main;
        col = GetComponent<Collider2D>();
    }
    #endregion

    #region sperate between walking and worlds rotate
    public void OnBeginDrag(PointerEventData eventData)
    {
        _drag = true;
        print("begin drag");

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
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_drag)
        {
            WorldsManagerToturial.CharacterMove = true;
        }
    }
    #endregion


    #region World rotate
    void Update()
    {
        Vector3 mousePos = myCam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (col == Physics2D.OverlapPoint(mousePos))
            {
                screenPos = myCam.WorldToScreenPoint(transform.position);
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
                if (!WorldsManagerToturial.onLeft) WorldsManagerToturial.LeftRotate(angle, angleOffset); 
                if (!WorldsManagerToturial.onRight) WorldsManagerToturial.RightRotate(angle, angleOffset); 
            }
        }
    }
    

    #endregion
   
}
