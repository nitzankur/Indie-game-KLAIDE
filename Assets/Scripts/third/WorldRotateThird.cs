
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldRotateThird : MonoBehaviour , IBeginDragHandler, IEndDragHandler, IDragHandler,IPointerDownHandler, IPointerClickHandler
{
     #region variables
     public static bool EndDrag;
     private bool _drag;
     private float _startTime;
     private Vector3 _pos;
     private Vector2 _mousePosStart;
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
            _mousePosStart = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
            var localpos = new Vector2(transform.position.x, transform.position.y);
            _pos = _mousePosStart - localpos;
            _startTime = Time.time;
        }

        if (Input.GetMouseButton(0))
        {
            if (PlayerControllerThird.EndOfPath && Time.time - _startTime > 0.3f)
            {
                Vector2 mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
                var localpos = new Vector2(transform.position.x, transform.position.y);
                Vector2 tempPos = mousePos - localpos;
                float ang = Vector2.Angle(_pos, tempPos);
                if (!WorldManagerThird.onButtom) RotateWorld(button,tempPos);
                if (!WorldManagerThird.onLeft) RotateWorld(left,tempPos);
                if (!WorldManagerThird.onRight) RotateWorld(gameObject,tempPos); 

                _pos = tempPos;
            }
        }
    }

    private void RotateWorld(GameObject side, Vector3 tempPos)
    {
        side.transform.rotation = Quaternion.RotateTowards(side.transform.rotation,   Quaternion.FromToRotation(_pos, tempPos) * side.transform.rotation, step);
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
        EndDrag = true;
    }
   
    
    #endregion


   
}
