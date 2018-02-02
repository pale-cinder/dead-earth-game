using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class NavAgentExample : MonoBehaviour
{
    //Inspector Assigned Variable
    public AIWaypointNetwork WaypointNetwork = null;
    public int CurrentIndex = 0;
    public bool HasPath = false;
    public bool PathPending = false;
    public bool PathStale = false;


    //Private Members 
    private NavMeshAgent _navAgent = null;


    // Use this for initialization
    void Start()
    {

        _navAgent = GetComponent<NavMeshAgent>();

        // If not valid Waypoint Network has been assigned then return
        if (WaypointNetwork == null) return;

        SetNextDestination(false);
    }
        

        void SetNextDestination ( bool increment )
        {
            if (!WaypointNetwork) return;

            //Check the value of increment bool
            //If increment is True --> assign value of 1, another case --> value of 0

            int incStep = increment ? 1 : 0;

            Transform nextWaypointTransform = null;

            //Keep incrementing until we find the loop

            while (nextWaypointTransform==null)
            {
                int nextWaypoint = (CurrentIndex + incStep >= WaypointNetwork.Waypoints.Count) ? 0 : CurrentIndex + incStep;

                nextWaypointTransform = WaypointNetwork.Waypoints[nextWaypoint];

                if (nextWaypointTransform!=null)
                {
                    CurrentIndex = nextWaypoint;
                    _navAgent.destination = nextWaypointTransform.position;
                    return;
                                       
                }

            }

        }

    void Update()
    {
        HasPath = _navAgent.hasPath;
        PathPending = _navAgent.pathPending;
        PathStale = _navAgent.isPathStale;

        if (HasPath && !PathPending)
            SetNextDestination(true);

       else

            if (_navAgent.isPathStale)
            SetNextDestination(false);

    }

}



