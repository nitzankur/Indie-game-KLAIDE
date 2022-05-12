using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;

public class PlayerControllerThird : MonoBehaviour
{
   #region InspectorProperties
    
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private bool reachedEndOfPath;
    [SerializeField] private float speed = 2;
    [SerializeField] private float nextWaypointDistance;
    [SerializeField] private int side = 0;
    [SerializeField] private bool flip = true;
    [SerializeField] private bool findDoor;
    private Animator playerAnimator;
    
    #endregion

    #region PrivateProperties

    private bool firstPoint = true;
    private bool _key;
    private Seeker _seeker;
    private Path _path;
    private int _currentWaypoint = 0;
    #endregion
    
   
    public void Start ()
    {
        findDoor = false;
        _seeker = GetComponent<Seeker>();
        playerAnimator = GetComponent<Animator>();
        reachedEndOfPath = true;
    }

    public void OnPathComplete (Path p) {
        Debug.Log("A path was calculated. Did it fail with an error? " + p.error);
        if (!p.error) {
            _path = p;
            _currentWaypoint = 0;
        }
    }
    public void Update () {

        if (WorldManagerThird.CharacterMove && reachedEndOfPath)
        {
            reachedEndOfPath = false;
            AstarData.active.Scan();
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _seeker.StartPath(transform.position, targetPosition, OnPathComplete);
            WorldManagerThird.CharacterMove = false;
        }

        if (reachedEndOfPath)
        {
            playerAnimator.SetInteger("side", side);
            _path = null;
        }
            
        
        if (_path == null)
            return;
        
        reachedEndOfPath = false;
        float distanceToWaypoint;
        
        if (firstPoint)
        {
            transform.position = _path.vectorPath[0];
            _currentWaypoint++;
            firstPoint = false;
            return;
        }
       
        while (true) {
            distanceToWaypoint = Vector3.Distance(transform.position, _path.vectorPath[_currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance) {
                if (_currentWaypoint + 1 < _path.vectorPath.Count) {
                    _currentWaypoint++;
                } 
                else {
                    reachedEndOfPath = true;
                    break;
                }
            } else {
                break;
            }
        }
        var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint/nextWaypointDistance) : 1f;
        Vector3 dir = (_path.vectorPath[_currentWaypoint] - transform.position).normalized;
        Vector3 velocity = dir * speed * speedFactor ;
        transform.position += velocity * Time.deltaTime;
        IndicateDirection(_path.vectorPath[_currentWaypoint]);
    }


    private void IndicateDirection(Vector3 target)
    {
        if (findDoor) return;
        var pos = transform.position;
        float angle = Mathf.Atan2
            (target.y - pos.y, target.x - pos.x) * 180 / Mathf.PI;

        
        if (angle < 60 && angle > -90) //RIGHT
        {
            side = 0;
            playerAnimator.SetInteger("side", 2);
            playerAnimator.SetTrigger("walkFront");
            print("right");
            if (pos.y < 0)
                flip = false;
            else
                flip = true;
            GetComponent<SpriteRenderer>().flipX = flip;
        }
        else if (angle >= 60 && angle < 120) //UP
        {
            side = 1;
            playerAnimator.SetInteger("side", side);
        }
        if (angle <= -60 && angle > -120) //DOWN
        {
            side = 0;
            playerAnimator.SetInteger("side", side);
        }
        else if ((angle <= -90) || (angle > 120)) //LEFT
        {
            side = 0;
            playerAnimator.SetInteger("side", 2);
            playerAnimator.SetTrigger("walkFront");
            if (pos.y < 0)
                flip = true;
            else
                flip = false;
            GetComponent<SpriteRenderer>().flipX = flip;
        }
    }
    
    
    private void FixedUpdate()
    { 
        Vector3 targ = Vector3.zero; 
        Vector3 objectPos = transform.position; 
        targ.x = targ.x - objectPos.x; 
        targ.y = targ.y - objectPos.y; 
        float angle = (Mathf.Atan2(targ.y, targ.x) - 80)* Mathf.Rad2Deg; 
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var pos = other.transform.position;
        if (other.CompareTag("Door"))
        {
            if (pos.x <= 0f && (pos.y > 0 || pos.y < 0 && pos.x < pos.y) && WorldManagerThird.onLeft && _key)
            {
                side = 1;
                playerAnimator.SetInteger("side", side);
                findDoor = true;
                StartCoroutine(waitAndLoad("Level2"));
                _key = false;
            }
        }

        else if (other.CompareTag("Key"))
        {
            print("key0");
            if (pos.x > 0f && (pos.y >0 || pos.y <0 &&pos.x>Mathf.Abs(pos.y)) && WorldManagerThird.onRight)
            {
                print("key") ;
                _key = true;
            }
        }
    }
    
    IEnumerator waitAndLoad(string sceneName)
    {   
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }
}
