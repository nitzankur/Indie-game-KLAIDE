using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Pathfinding;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region InspectorProperties
    
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private bool reachedEndOfPath;
    [SerializeField] private float speed = 2;
    [SerializeField] private float nextWaypointDistance = 1;
    
    #endregion

    #region PrivateProperties
    
    private Seeker _seeker;
    private Path _path;
    private int _currentWaypoint = 0;

    #endregion
    
   
    public void Start () {
        _seeker = GetComponent<Seeker>();
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
        if (reachedEndOfPath)
            _path = null;
        
        if (WorldsManager.CharacterMove && reachedEndOfPath)
        {
            reachedEndOfPath = false;
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _seeker.StartPath(transform.position, targetPosition, OnPathComplete);
            //WorldsManager.CharacterMove = false;
        }
        
        if (_path == null)
            return;
        
        reachedEndOfPath = false;
        float distanceToWaypoint;
     
       
        while (true) {
            distanceToWaypoint = Vector3.Distance(transform.position, _path.vectorPath[_currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance) {
               
                if (_currentWaypoint + 1 < _path.vectorPath.Count) {
                    _currentWaypoint++;
                } else {
                    reachedEndOfPath = true;
                    break;
                }
            } else {
                break;
            }
        }
        var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint/nextWaypointDistance) : 1f;
        Vector3 dir = (_path.vectorPath[_currentWaypoint] - transform.position).normalized;
        Vector3 velocity = dir * speed * speedFactor;
        transform.position += velocity * Time.deltaTime;
    }


}


