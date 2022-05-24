
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldRotateThird : MonoBehaviour , IBeginDragHandler, IEndDragHandler, IDragHandler,IPointerDownHandler, IPointerClickHandler
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
     [SerializeField] private GameObject left,button;
     private void Start()
    {
        WorldManagerThird.CharacterMove = false;
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
            if (PlayerControllerThird.EndOfPath && Time.time - startTime > 0.3f)
            {
                var ClockDrag = false;
                Vector2 mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
                var localpos = new Vector2(transform.position.x, transform.position.y);
                Vector2 tempPos = mousePos - localpos;
                float ang = Vector2.Angle(pos, tempPos);
                if (!WorldManagerThird.onButtom) RotateWorld(button,tempPos);
                if (!WorldManagerThird.onLeft) RotateWorld(left,tempPos);
                if (!WorldManagerThird.onRight) RotateWorld(gameObject,tempPos); 

                pos = tempPos;
            }
        }
    }

    private void RotateWorld(GameObject side, Vector3 tempPos)
    {
        side.transform.rotation = Quaternion.RotateTowards(side.transform.rotation,   Quaternion.FromToRotation(pos, tempPos) * side.transform.rotation, step);
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
