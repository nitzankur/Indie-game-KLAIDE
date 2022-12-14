using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;

public class PlayerControllerEight : MonoBehaviour
{
   #region InspectorProperties
    
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private bool reachedEndOfPath;
    [SerializeField] private float speed = 2;
    [SerializeField] private float nextWaypointDistance,horRadDoor, verRadDoor,waitTime;
    [SerializeField] private int side = 0;
    [SerializeField] private bool move;
    [SerializeField] private bool front;
    [SerializeField] private bool flip = true;
    [SerializeField] private bool findDoor;
    [SerializeField] private GameObject door,key, keyAppear, fade;
    [SerializeField] private Transform portalInsideRight, portalTop;
    [SerializeField] private float portalRadiusHor,portalRadiusVer,portalDistanceParameter;
    #endregion

    #region PrivateProperties
    
    private Animator playerAnimator;
    private bool firstPoint = true;
    private bool _key;
    private Seeker _seeker;
    private Path _path;
    private int _currentWaypoint = 0;
    private const float Radius = 9.1f;
    private float timeForStart;
    
    #endregion

    public static bool FinishLevel;
    public static bool EndOfPath,PortalON;
    public static bool Drag;
   
    public void Start ()
    {
        timeForStart = 0;
        findDoor = false;
        _key = false;
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
        if (timeForStart < 4.8)
        {
            timeForStart += Time.deltaTime;
            return;
        }
        GetInDoor();
        if (LevelManager.Level == 7)
        {
            Portal();
    
        }
        EndOfPath = reachedEndOfPath;
        if (WorldsManagerEight.CharacterMove)
        {
            reachedEndOfPath = false;
            AstarData.active.Scan();
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _seeker.StartPath(transform.position, targetPosition, OnPathComplete);
            WorldsManagerEight.CharacterMove = false;
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
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<PlayAudio>().enabled = false;
            return;
        }
        
        reachedEndOfPath = false;
        float distanceToWaypoint;
        print("firstPoint");
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
        Vector2 dir = (_path.vectorPath[_currentWaypoint] - transform.position).normalized;
        Vector2 velocity = dir * speed * speedFactor ;
        GetComponent<AIPath>().constrainInsideGraph = false;
        transform.position += (Vector3) velocity * Time.deltaTime ; 
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
        if (doorPos.x < 0 && Mathf.Abs(doorPos.x) > Mathf.Abs(doorPos.y) && WorldsManagerEight.onLeft &&_key && pos.x < doorPos.x + horRadDoor && pos.x > 
            doorPos.x - horRadDoor && pos.y < doorPos.y + verRadDoor && pos.y >doorPos.y - verRadDoor)
        {
            print("door");
            door.GetComponent<Animator>().SetTrigger("Key");
            front = false;
            side = 1;
            playerAnimator.SetInteger("Side", side);
            playerAnimator.SetBool("Front", front);
            door.GetComponent<AudioSource>().enabled = true;
            fade.GetComponent<Animator>().SetTrigger("fadeOut");
            StartCoroutine(waitAndLoad());
            _key = false;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        switch (LevelManager.Level)
        {
            case 6:
                TriggerLevel6(other);
                break;
            case 7:
                TriggerLevel7(other);
                break;
        }
    }

    private void TriggerLevel7(Collider2D other)
    {
        var pos = other.transform.position;
        if (!other.CompareTag("Key")) return;
        if (pos.x > 0 && pos.x > Mathf.Abs(pos.y) && WorldsManagerEight.onRight)
        {
            _key = true;
            FindObjectOfType<Camera>().GetComponent<AudioSource>().Play();
            other.gameObject.SetActive(false);
            keyAppear.SetActive(true);
         //   keyAppear.GetComponentInChildren<Animator>().enabled = true;
            foreach (var child in door.GetComponentsInChildren<Transform>())
            {
                child.GetComponent<SpriteRenderer>().enabled = true;
                child.GetComponent<Animator>().enabled = true;
            }
        }
    }

    private void TriggerLevel6(Collider2D other)
    {
        var pos = other.transform.position;
        if (!other.CompareTag("Key")) return;
        if (pos.y > 0 && pos.y >= Mathf.Abs(pos.x) && WorldsManagerEight.onInsideTop)
        {
            _key = true;
            FindObjectOfType<Camera>().GetComponent<AudioSource>().Play();
            other.gameObject.SetActive(false);
            keyAppear.SetActive(true);
//            keyAppear.GetComponentInChildren<Animator>().enabled = true;
            foreach (var child in door.GetComponentsInChildren<Transform>())
            {
                child.GetComponent<SpriteRenderer>().enabled = true;
                child.GetComponent<Animator>().enabled = true;
            }
        }
    }
        private void Portal()
    {
        var pos = gameObject.transform.position;
        var portalPosT = portalTop.transform.position;
        var portalPosIR = portalInsideRight.transform.position;
        if ((pos.x < (portalPosT.x + portalRadiusHor) && (pos.x > (portalPosT.x - portalRadiusHor)) && pos.y <
            portalPosT.y
            + portalRadiusVer && pos.y > portalPosT.y - portalRadiusVer && WorldsManagerEight.onTop))
        {
            PortalON = true;
            if(portalPosIR.x > 0 && portalPosIR.x > Mathf.Abs(portalPosIR.y)&& reachedEndOfPath && _path == null && 
               Vector3.Distance(portalPosIR, Vector3.zero) < Radius)
            {
                print("portal top");
                playerAnimator.SetTrigger("inPortal");
                if (_path != null)
                {
                    _currentWaypoint = _path.vectorPath.Count - 1;
                    _path.vectorPath[_currentWaypoint] = portalInsideRight.position;
                    _path = null;
                }
                reachedEndOfPath = true;
                PortalON = true;
                portalInsideRight.GetComponent<AudioSource>().Play();
                GetComponent<AIPath>().constrainInsideGraph = true;
                StartCoroutine(WaitForportalInsideRight());
                firstPoint = true;
                reachedEndOfPath = false;
                Drag = true;
                GetComponent<AudioSource>().Stop();
                GetComponent<PlayAudio>().enabled = false;
            } 
        }
           

        else if ((pos.x < (portalPosIR.x + portalRadiusHor) && 
                  (pos.x > (portalPosIR.x - portalRadiusHor)) &&
                  pos.y < portalPosIR.y + portalRadiusVer && pos.y > portalPosIR.y - portalRadiusVer &&
                  WorldsManagerEight.onInsideRight))
        {
            PortalON = true;
            if(portalPosT.y > 0 && portalPosT.y >= Mathf.Abs(portalPosT.x)&& reachedEndOfPath && _path == null)
            {
                print("portal right");
                playerAnimator.SetTrigger("inPortal");
                if (_path != null)
                {
                    _currentWaypoint = _path.vectorPath.Count - 1;
                    _path.vectorPath[_currentWaypoint] = portalTop.position;
                    _path = null;
                }
                reachedEndOfPath = true;
                PortalON = true;
                portalTop.GetComponent<AudioSource>().Play();
                GetComponent<AIPath>().constrainInsideGraph = true;
                StartCoroutine(WaitForTop());
                firstPoint = true;
                reachedEndOfPath = false;
                Drag = true;
                GetComponent<AudioSource>().Stop();
                GetComponent<PlayAudio>().enabled = false;
            }            
        }
        else
        {
            PortalON = false;
        }
        
    }
        #endregion   
    
    IEnumerator waitAndLoad()
    {   
        yield return new WaitForSeconds(waitTime);
        if (!FinishLevel)
        {
            FinishLevel = true;
            LevelManager.unlockedLevel++;
        }

        switch (LevelManager.Level)
        {
            case 6:
                LevelManager.Level = 7; //todo: change to 7 
                SceneManager.LoadScene("Level-7");
                break;
            case 7:
                //  LevelManager.Level = 7; //todo: change to end scene
                SceneManager.LoadScene("final-scene");
                break;
        }
    }
    
    IEnumerator WaitForTop()
    {   
        yield return new WaitForSeconds(0.7f);
        transform.position = portalTop.position;
        AstarData.active.Scan();
        move = false;
        _path = null;
    }
    
    IEnumerator WaitForportalInsideRight()
    {
        yield return new WaitForSeconds(0.7f);
        transform.position = portalInsideRight.position;
        AstarData.active.Scan();
        move = false;
        _path = null;
    }
    
}
