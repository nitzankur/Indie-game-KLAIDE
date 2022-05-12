
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldRotateThird : MonoBehaviour , IBeginDragHandler, IEndDragHandler, IDragHandler,IPointerDownHandler, IPointerClickHandler
{
     #region variables
     public static bool endDrag;
     private Vector3 _mousePos;
     private Camera myCam;
     private Vector3 screenPos;
     public static float angleOffset;
     private float curTime;
     private Collider2D col;
     private bool _drag;
    

    private void Start()
    {
        WorldManagerThird.CharacterMove = false;
        myCam = Camera.main;
        col = GetComponent<Collider2D>();
    }
    #endregion
    
    #region World rotate
    void Update()
    {
        
        Vector3 mousePos = myCam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {   curTime = Time.time;
            if (col == Physics2D.OverlapPoint(mousePos))
            {   screenPos = myCam.WorldToScreenPoint(transform.position);
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
                if ((Time.time - curTime) > 0.2f)
                {
                    if (!WorldManagerThird.onLeft) WorldManagerThird.LeftRotate(angle, angleOffset); 
                    if (!WorldManagerThird.onRight) WorldManagerThird.RightRotate(angle, angleOffset);
                    if (!WorldManagerThird.onButtom) WorldManagerThird.buttomRotate(angle, angleOffset);
                }
               
            }
         }
    }
    

    #endregion

    #region sperate between walking and World rotate
    public void OnPointerDown(PointerEventData eventData)
    {
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_drag)
        {
            WorldManagerThird.CharacterMove = true;
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
