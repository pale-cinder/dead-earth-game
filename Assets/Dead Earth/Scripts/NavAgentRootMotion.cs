using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class NavAgentRootMotion: MonoBehaviour
{
    //Inspector Assigned Variable
    public AIWaypointNetwork WaypointNetwork = null;
    public int CurrentIndex = 0;
    public bool HasPath = false;
    public bool PathPending = false;
    public bool PathStale = false;
    public NavMeshPathStatus PathStatus = NavMeshPathStatus.PathInvalid;
    public AnimationCurve JumpCurve = new AnimationCurve();

    //Private Members 
    private NavMeshAgent _navAgent = null;
    private Animator _animator = null;
   


    // Use this for initialization
    void Start()
    {

        _navAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        /*
         * _navAgent.UpdatePosition = false;
         * _navAgent.updateRotation = false;
         */

        // If not valid Waypoint Network has been assigned then return

        _navAgent.updateRotation = false;
        

        if (WaypointNetwork == null) return;

        SetNextDestination(false);
    }
        

        void SetNextDestination ( bool increment )
        {
            if (!WaypointNetwork) return;

            //Check the value of increment bool
            //If increment is True --> assign value of 1, another case --> value of 0

            int incStep = increment ? 1 : 0;


            // Calculate index of next waypoint

            Transform nextWaypointTransform = null;

            int nextWaypoint = (CurrentIndex + incStep >= WaypointNetwork.Waypoints.Count) ? 0 : CurrentIndex + incStep;
            nextWaypointTransform = WaypointNetwork.Waypoints[nextWaypoint];

        //Keep incrementing until we find the loop
        if (nextWaypointTransform != null)
        {
            // Update the current waypoint index, assign its position as the NavMeshAgents
            // Destination and then return
            CurrentIndex = nextWaypoint;
            _navAgent.destination = nextWaypointTransform.position;
            return;
        }

        // We did not find a valid waypoint in the list for this iteration
        CurrentIndex = nextWaypoint;
    }

   
    void Update()

       {
       
        // Copy NavMeshAgents state into inspector visible variables
        HasPath = _navAgent.hasPath;
        PathPending = _navAgent.pathPending;
        PathStale = _navAgent.isPathStale;
        PathStatus = _navAgent.pathStatus;

           
        
        //if we dont have a path
        if ((_navAgent.remainingDistance<=_navAgent.stoppingDistance && !PathPending) || PathStatus == NavMeshPathStatus.PathInvalid /*|| PathStatus==NavMeshPathStatus.PathPartial*/)
            
            //set the destination to the next waypoint
            SetNextDestination(true);
        else
        if (_navAgent.isPathStale)
            SetNextDestination(false);
       
    }


    private void OnAnimatorMove()
    {
        
    }


    /*
    IEnumerator Jump(float duration)
    {
        OffMeshLinkData data = _navAgent.currentOffMeshLinkData;
        Vector3 startPos = _navAgent.transform.position;
        Vector3 endPos = data.endPos + (_navAgent.baseOffset * Vector3.up);
        float time = 0.0f;

        while (time<=duration)
        {
            float t = time / duration;
            _navAgent.transform.position = Vector3.Lerp(startPos, endPos, t) + (JumpCurve.Evaluate(t)*Vector3.up);

            time += Time.deltaTime;
            yield return null;


        }
        _navAgent.CompleteOffMeshLink();
    }*/
}