using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;

public class PlayerControllerFour : MonoBehaviour
{
   #region InspectorProperties
    
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private bool reachedEndOfPath;
    [SerializeField] private float speed = 2;
    [SerializeField] private float nextWaypointDistance;
    [SerializeField] private int side = 0;
    [SerializeField] private bool move;
    [SerializeField] private bool front;
    [SerializeField] private bool flip = true;
    [SerializeField] private bool findDoor;
    [SerializeField] private Transform PortalRight, PortalLeft;
    [SerializeField] private GameObject key;
    private Animator playerAnimator;

    #endregion

    #region PrivateProperties

    
    private bool firstPoint = true;
    private bool _key,PortalON;
    private Seeker _seeker;
    private Path _path;
    private int _currentWaypoint = 0;
    
    #endregion

    public static bool FinishLevel;
    public static bool EndOfPath;
   
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

    #region Calculate Path
    public void Update ()
    {
        EndOfPath = reachedEndOfPath;
        if (WorldsManager.CharacterMove)
        {
            reachedEndOfPath = false;
            AstarData.active.Scan();
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _seeker.StartPath(transform.position, targetPosition, OnPathComplete);
            WorldsManager.CharacterMove = false;
        }

        if (reachedEndOfPath)
        {
            move = false;
            playerAnimator.SetBool("Move", move);
            playerAnimator.SetInteger("Side", side);
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
        GetComponent<AIPath>().constrainInsideGraph = false;
        transform.position += velocity * Time.deltaTime;
        IndicateDirection(_path.vectorPath[_currentWaypoint]);
    }
    
    #endregion
  

    #region IndicateDirection

    private void IndicateDirection(Vector3 target)
    {
        move = true;
        playerAnimator.SetBool("Move", move);

        if (findDoor) return;
        var pos = transform.position;
        float angle = Mathf.Atan2
            (target.y - pos.y, target.x - pos.x) * 180 / Mathf.PI;


        if (angle < 60 && angle > -90) //RIGHT
        {
            side = 2;
            if (pos.y < 0)
            {
                front = (angle > -10);
                flip = false;
            }
            else
            {
                front = !(angle > -10);
                flip = true;
            }
            GetComponent<SpriteRenderer>().flipX = flip;
        }


        else if (angle >= 60 && angle < 120) //UP
        {
            front = pos.y < 0;
            side = pos.y < 0 ? 0 : 1;
        }

        if (angle <= -60 && angle > -120) //DOWN
        {
            front = !(pos.y < 0);
            side = pos.y < 0 ? 1 : 0;
        }
        else if (angle <= -90 || angle > 120) //LEFT
        {
            side = 2;
            if (pos.y < 0)
            {
                if (angle > 120 && angle < 180)
                    front = true;
                else
                    front = false;
                flip = true;
            }
            else
            {
                if (angle > 120 && angle < 180)
                    front = false;
                else
                    front = true;
                flip = false;
            }

            GetComponent<SpriteRenderer>().flipX = flip;

            if (angle > 120 && angle < 180)
                front = false;
            else
                front = true;

        }

        playerAnimator.SetInteger("Side", side);
        playerAnimator.SetBool("Front", front);
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
    

    #endregion

    #region Trigger
    private void OnTriggerStay2D(Collider2D other)
    {
        var pos = other.transform.position;
        if (other.CompareTag("Door"))
        {
            if (GameManager.Level == "level_2" && pos.y > 0 && pos.y >= Mathf.Abs(pos.x) && WorldsManager.onTop && _key)
            {
                getInDoor(other);
            }
            if (GameManager.Level == "level_2" && (pos.y < 0 &&  Mathf.Abs(pos.x) <= Mathf.Abs(pos.y) && WorldsManager.onButtom && _key))
            {
                getInDoor(other);
            }
        }
        

        else if (other.CompareTag("Key"))
        {
            print("key0" + WorldsManager.onRight);
            if (GameManager.Level == "level_2" && (pos.y > 0 && pos.y >= Mathf.Abs(pos.x)) && WorldsManager.onTop)
            {
                turnOfKey(other);
            }
            else if (GameManager.Level == "level_3" && (pos.x > 0 && pos.x > Mathf.Abs(pos.y)) && WorldsManager.onRight)
            {
                print("door");
                turnOfKey(other);
            }
        }
    }

    private void turnOfKey(Collider2D other)
    {
        print("key") ;
        _key = true;
       key.SetActive(false);
    }

    private void getInDoor(Collider2D other)
    {
        front = false;
        side = 1;
        playerAnimator.SetInteger("Side", side);
        playerAnimator.SetBool("Front", front);
        StartCoroutine(waitAndLoad("StartLevel3"));
        _key = false;
        key.SetActive(true);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Portal"))
        {
            if (!PortalON)
            {
                print("Portal");
                if (other.transform == PortalLeft && WorldsManager.onLeft &&
                    PortalRight.position.x > 0 &&  PortalRight.position.x > Mathf.Abs( PortalRight.position.y))
                {
                    if (_path != null)
                    {
                        _currentWaypoint = _path.vectorPath.Count - 1;
                        _path.vectorPath[_currentWaypoint] = PortalRight.position;
                        _path = null;
                    }
                    reachedEndOfPath = true;
                    PortalON = true;
                    GetComponent<AIPath>().constrainInsideGraph = true;
                    transform.position = PortalRight.position;
                    firstPoint = true;
                    // Physics2D.IgnoreLayerCollision(3, 8, true);
                    // StartCoroutine(waitSecond());
                    // WorldsManager.onLeft = false;
                }

                else if (other.transform == PortalRight && WorldsManager.onRight && 
                         (PortalLeft.position.x<0 && Mathf.Abs(PortalLeft.position.x) > Mathf.Abs(PortalLeft.position.y)))
                {
                    if (_path != null)
                    {
                        _currentWaypoint = _path.vectorPath.Count - 1;
                        _path.vectorPath[_currentWaypoint] = PortalLeft.position;
                        _path = null;
                    }
                    reachedEndOfPath = true;
                    PortalON = true;
                    GetComponent<AIPath>().constrainInsideGraph = true;
                    transform.position = PortalLeft.position;
                    
                    firstPoint = true;
                    // Physics2D.IgnoreLayerCollision(3, 8, true);
                    // StartCoroutine(waitSecond());
                }
            }
            else if (PortalLeft.gameObject.GetInstanceID() == other.gameObject.GetInstanceID() || 
                     PortalRight.gameObject.GetInstanceID() == other.gameObject.GetInstanceID())
                PortalON = false;

        }
           
    }
    

    #endregion   
    
    IEnumerator waitAndLoad(string sceneName)
    {   
        yield return new WaitForSeconds(1f);
        if (!FinishLevel)
        {
            FinishLevel = true;
            LevelManager.unlockedLevel++;
        }
        SceneManager.LoadScene(sceneName);
    }
    
    IEnumerator waitSecond()
    {
        yield return new WaitForSeconds(2f);
        Physics2D.IgnoreLayerCollision(3, 8, false);
    }
}
