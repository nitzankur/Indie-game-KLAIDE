using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;


public class PlayerControllerThird : MonoBehaviour
{
   #region InspectorProperties
    
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private bool reachedEndOfPath;
    [SerializeField] private float speed = 2;
    [SerializeField] private float nextWaypointDistance,verRadDoor,horRadDoor,waitTime;
    [SerializeField] private int side = 0;
    [SerializeField] private bool move;
    [SerializeField] private bool front;
    [SerializeField] private bool flip = true;
    [SerializeField] private bool findDoor;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject door, fade;
    private Animator playerAnimator;

    #endregion

    #region PrivateProperties

    private bool firstPoint = true;
    private bool _key;
    private Seeker _seeker;
    private Path _path;
    private int _currentWaypoint = 0;
    private float timeForStart;
    #endregion

    public static bool FinishLevel;
    public static bool EndOfPath;
   
    public void Start ()
    {
        timeForStart = 0;
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
    public void Update ()
    {
        if (timeForStart < 4.8)
        {
            timeForStart += Time.deltaTime;
            return;
        }
        GetInDoor();
        EndOfPath = reachedEndOfPath;
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
        transform.position += velocity * Time.deltaTime;
        IndicateDirection(_path.vectorPath[_currentWaypoint]);
    }


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
        var doorPos = door.transform.position;
        var pos = gameObject.transform.position;
        if (doorPos.x <=- 0.3f && (doorPos.y > 0 || doorPos.y < 0 &&doorPos.x < doorPos.y-1.7f) && WorldManagerThird.onLeft && _key
            &&  pos.x < doorPos.x + horRadDoor && pos.x > 
            doorPos.x - horRadDoor && pos.y < doorPos.y + verRadDoor && pos.y >doorPos.y - verRadDoor)
        {
            front = false;
            side = 1;
            door.GetComponent<Animator>().SetTrigger("Key");
            playerAnimator.SetInteger("Side", side);
            playerAnimator.SetBool("Front", front);
            fade.GetComponent<Animator>().SetTrigger("fadeOut");
            door.GetComponent<AudioSource>().enabled = true;
            StartCoroutine(waitAndLoad("Level-4"));
            _key = false;
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        var pos = other.transform.position;
        if (other.CompareTag("Key"))
        {
            print("key0" + WorldManagerThird.onRight);
            if (pos.x > 0f && (pos.y >0 || pos.y <0 &&pos.x>Mathf.Abs(pos.y)) && WorldManagerThird.onRight)
            {
                print("key");
                FindObjectOfType<Camera>().GetComponent<AudioSource>().Play();
                _key = true;
                key.SetActive(true);

                /*foreach (var child in door.GetComponentsInChildren<Transform>())
                {
                    child.GetComponent<SpriteRenderer>().enabled = true;
                    child.GetComponent<Animator>().enabled = true;
                }*/

                other.gameObject.SetActive(false);
            }
        }
        /*if (other.CompareTag("Door"))
        {
            if (pos.x <= 0f && (pos.y > 0 || pos.y < 0 && pos.x < pos.y) && WorldManagerThird.onLeft && _key)
            {
                front = false;
                side = 1;
                
                playerAnimator.SetInteger("Side", side);
                playerAnimator.SetBool("Front", front);
                other.GetComponent<AudioSource>().enabled = true;
                StartCoroutine(waitAndLoad("Level-4"));
                _key = false;
            }
        }*/
    }
    
    IEnumerator waitAndLoad(string sceneName)
    {   
        yield return new WaitForSeconds(waitTime);
        if (!FinishLevel)
        {
            FinishLevel = true;
            LevelManager.unlockedLevel++;
        }
        Menu.fromRestart = false;
        LevelManager.Level = 4;
        SceneManager.LoadScene(sceneName);
    }
}
