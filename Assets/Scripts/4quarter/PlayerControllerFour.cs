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
    [SerializeField] private float nextWaypointDistance,waitTime,horRadDoor, verRadDoor;
    [SerializeField] private int side = 0;
    [SerializeField] private bool move;
    [SerializeField] private bool front;
    [SerializeField] private bool flip = true;
    [SerializeField] private bool findDoor;
    [SerializeField] private int level;
    [SerializeField] private Transform PortalRight, PortalLeft;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject keyAppear;
    [SerializeField] private GameObject door;
    [SerializeField] private float portalRadiusHor,portalRadiusVer;
    [SerializeField] private float portalDistanceParameter;
    private Animator playerAnimator;

    #endregion

    #region PrivateProperties

    
    private bool firstPoint = true;
    private bool _key, PortalON;
    private Seeker _seeker;
    private Path _path;
    private int _currentWaypoint = 0;
    private bool onRightPortal, onLeftPortal;
    
    #endregion

    public static bool FinishLevel;
    public static bool EndOfPath;
   
    public void Start ()
    {
        findDoor = false;
        _seeker = GetComponent<Seeker>();
        playerAnimator = GetComponent<Animator>();
        reachedEndOfPath = true;
        key.SetActive(true);
        PortalON = true;
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
        GetInDoor();
        if (LevelManager.Level == 5 && !findDoor)
        {
            Portal();
        }
        
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
            GetComponent<AudioSource>().Stop();
            GetComponent<PlayAudio>().enabled = false;
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

    private void GetInDoor()
    {
        var doorPos = door.transform.position;
        var pos = gameObject.transform.position;
        if (LevelManager.Level == 5 && doorPos.y > 0 && doorPos.y >= Mathf.Abs(doorPos.x) && WorldsManager.onTop  && _key &&  pos.x < doorPos.x + horRadDoor && pos.x > 
            doorPos.x - horRadDoor && pos.y < doorPos.y + verRadDoor && pos.y >doorPos.y - verRadDoor)
        {
            print("door");
            GetInDoorHalper();
        }
        if (LevelManager.Level == 4 && (doorPos.y < 0 &&  Mathf.Abs(doorPos.x) <= Mathf.Abs(doorPos.y) && WorldsManager.onButtom  && _key  &&  pos.x < doorPos.x + horRadDoor && pos.x > 
            doorPos.x - horRadDoor && pos.y < doorPos.y + verRadDoor && pos.y >doorPos.y - verRadDoor))
        {
            print("door");
            GetInDoorHalper();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        var pos = other.transform.position;

        if (other.CompareTag("Key"))
        {
            print("key0" + WorldsManager.onRight);
            if (LevelManager.Level == 5 && (pos.y > 0 && pos.y >= Mathf.Abs(pos.x)) && WorldsManager.onTop)
            {
                turnOfKey(other);
            }
            else if (LevelManager.Level == 4 && (pos.x > 0 && pos.x > Mathf.Abs(pos.y)) && WorldsManager.onRight)
            {
                print("key");
                turnOfKey(other);
            }
        }
    }

    private void turnOfKey(Collider2D other)
    {
        print("key") ;
        _key = true;
        FindObjectOfType<Camera>().GetComponent<AudioSource>().Play();
        door.GetComponent<Animator>().SetTrigger("Key");
        keyAppear.SetActive(true);
        key.SetActive(false);
    }

    private void GetInDoorHalper()
    {
        print("door should open");
        findDoor = true;
        front = false;
        side = 1;
        playerAnimator.SetInteger("Side", side);
        playerAnimator.SetBool("Front", front);
        if (LevelManager.Level == 4)
        {
            StartCoroutine(waitAndLoad(sceneName: "StartLevel5"));
            LevelManager.Level = 5;
        }
        else if (LevelManager.Level == 5)
        {
            door.GetComponent<AudioSource>().enabled = true;
            StartCoroutine(waitAndLoad(sceneName: "Level6")); //todo: change to start of level 5
            LevelManager.Level = 6;
        }
        _key = false;
    }


    private void Portal()
    {
        var pos = gameObject.transform.position;
        var portalPosL = PortalLeft.transform.position;
        var portalPosR = PortalRight.transform.position;
        if ((pos.x < (portalPosL.x+portalRadiusHor) && (pos.x > (portalPosL.x-portalRadiusHor))&& pos.y < portalPosL.y +portalRadiusVer && pos.y>portalPosL.y-portalRadiusVer &&WorldsManager.onLeft &&
            PortalRight.position.x > 0 &&  PortalRight.position.x > Mathf.Abs( PortalRight.position.y))&PortalON)
                {
                    print("portal left");
                    if (_path != null)
                    {
                        _currentWaypoint = _path.vectorPath.Count - 1;
                        _path.vectorPath[_currentWaypoint] = PortalRight.position;
                        _path = null;
                    }
                    reachedEndOfPath = true;
                    PortalON = true;
                    GetComponent<AIPath>().constrainInsideGraph = true;
                    PortalLeft.GetComponent<AudioSource>().Play();
                    transform.position = PortalRight.position ;//+ Vector3.right * portalDistanceParameter;
                    firstPoint = true;
                    PortalON = false;
                    StartCoroutine(WaitAndMove());
                    
                }

                else if ((pos.x < (portalPosR.x + portalRadiusHor) && (pos.x > (portalPosR.x-portalRadiusHor))&& pos.y < portalPosR.y +portalRadiusVer && pos.y>portalPosR.y-portalRadiusVer && WorldsManager.onRight && 
                          (PortalLeft.position.x<0 && Mathf.Abs(PortalLeft.position.x) > Mathf.Abs(PortalLeft.position.y)&&PortalON)))
                {
                    print("portal right");
                    if (_path != null)
                    {
                        _currentWaypoint = _path.vectorPath.Count - 1;
                        _path.vectorPath[_currentWaypoint] = PortalLeft.position;
                        _path = null;
                    }
                    reachedEndOfPath = true;
                    PortalON = true;
                    GetComponent<AIPath>().constrainInsideGraph = true;
                    PortalRight.GetComponent<AudioSource>().Play();
                    transform.position = PortalLeft.position;//+ Vector3.left* portalDistanceParameter;
                    firstPoint = true;
                    PortalON = false;
                    StartCoroutine(WaitAndMove());
                }
    }
    
    #endregion   
    
    IEnumerator waitAndLoad(string sceneName)
    {   
        yield return new WaitForSeconds(waitTime);
        if (!FinishLevel)
        {
            FinishLevel = true;
            LevelManager.unlockedLevel++;
            
        }
        SceneManager.LoadScene(sceneName);
        
    }
    
    IEnumerator WaitAndMove()
    {   
        yield return new WaitForSeconds(2);
        PortalON = true;
    }
}
