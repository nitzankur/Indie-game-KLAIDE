using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Pathfinding;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    #region InspectorProperties
    
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private bool reachedEndOfPath;
    [SerializeField] private float speed = 2;
    [SerializeField] private float nextWaypointDistance, horRadDoor, verRadDoor,waitTime;
    [SerializeField] private string nextScene;
    [SerializeField] private int side = 0;
    [SerializeField] private bool move;
    [SerializeField] private bool front;
    [SerializeField] private bool flip = true;
    [SerializeField] private bool findDoor;
    [SerializeField] private GameObject door;
    private Animator playerAnimator;
    
    //TUTORIAL ADDITION
    [SerializeField] private bool tutorial;
    [SerializeField] private GameObject dot;
    [SerializeField] private GameObject tutorialMove;
    [SerializeField] private GameObject tutorialRotate;
    [SerializeField] private Vector3[] tutorialRotatePos;

    
    #endregion

    #region PrivateProperties

    private bool firstPoint = true; 
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
        side = 0;
        move = false;
        front = true;
    }

    private void OnPathComplete (Path p) {
        Debug.Log("A path was calculated. Did it fail with an error? " + p.error);
        if (!p.error) {
            _path = p;
            _currentWaypoint = 0;
        }
    }
    
    public void Update ()
    {
        GetInDoor();
        EndOfPath = reachedEndOfPath;
        if (tutorial && WorldRotateTutorial.RotateOnce && tutorialRotate.gameObject.activeSelf)
            tutorialRotate.GetComponent<Animator>().SetBool("Rotate", true);

        if (WorldsManagerToturial.CharacterMove)
        {
            reachedEndOfPath = false;
            AstarData.active.Scan();
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _seeker.StartPath(transform.position, targetPosition, OnPathComplete);
            WorldsManagerToturial.CharacterMove = false;
        }

        /*else
        {
            GetComponent<AudioSource>().Stop();
        }*/
        if (reachedEndOfPath)
        {
            if (tutorial && move)
                RotateTutorial();
            move = false;
            playerAnimator.SetBool("Move", move);
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
        transform.position += velocity * Time.deltaTime;
        IndicateDirection(_path.vectorPath[_currentWaypoint]);
    }
    

    private void IndicateDirection(Vector3 target)
    {
        if (tutorial)
        {
            dot.SetActive(false);
            tutorialMove.GetComponent<Animator>().SetBool("Click",true);
          //  tutorialMove.SetActive(false);
        }
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
            if (pos.y < 0){
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
    
    
    private void RotateTutorial()
    {
        if (!WorldRotateTutorial.RotateOnce)
        {
            tutorialRotate.transform.position = WorldsManagerToturial.onRight ? tutorialRotatePos[1] : tutorialRotatePos[0];
            tutorialRotate.SetActive(true);
        }
        else if (tutorialRotate.activeSelf)
            tutorialRotate.GetComponent<Animator>().SetBool("Rotate", true);
        
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
    
    private void GetInDoor()
    {
        var pos = gameObject.transform.position;
        var doorPos = door.transform.position;
        if (tutorial && WorldsManagerToturial.onRight && doorPos.x > 0 && pos.x < doorPos.x + horRadDoor && pos.x > 
            doorPos.x - horRadDoor && pos.y < doorPos.y + verRadDoor && pos.y >doorPos.y - verRadDoor)
        {
            front = false;
            side = 1;
            playerAnimator.SetInteger("Side", side);
            playerAnimator.SetBool("Front", front);
            findDoor = true;
            door.GetComponent<AudioSource>().enabled = true;
            StartCoroutine(WaitAndLoad());
        }
        else if (!tutorial && WorldsManagerToturial.onLeft && doorPos.x <= 0  && pos.x < doorPos.x + horRadDoor && pos.x > 
            doorPos.x - horRadDoor && pos.y < doorPos.y + verRadDoor && pos.y >doorPos.y - verRadDoor)
        {
            front = false;
            side = 1;
            playerAnimator.SetInteger("Side", side);
            playerAnimator.SetBool("Front", front);
            findDoor = true;
            door.GetComponent<AudioSource>().enabled = true;
            StartCoroutine(WaitAndLoad());
        }
    }
    

    IEnumerator WaitAndLoad()
    {   
        yield return new WaitForSeconds(waitTime);
        if (!FinishLevel)
        {
            FinishLevel = true;
            LevelManager.unlockedLevel++;
        }
        SceneManager.LoadScene(nextScene);
    }
}


