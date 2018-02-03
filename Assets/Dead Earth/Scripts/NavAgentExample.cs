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
    public NavMeshPathStatus PathStatus = NavMeshPathStatus.PathInvalid;

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


            // Calculate index of next waypoint

            // Transform nextWaypointTransform = null;

            int nextWaypoint = (CurrentIndex + incStep >= WaypointNetwork.Waypoints.Count) ? 0 : CurrentIndex + incStep;
            Transform nextWaypointTransform = WaypointNetwork.Waypoints[nextWaypoint];

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
        CurrentIndex++;
    }

   
    void Update()
    {
        // Copy NavMeshAgents state into inspector visible variables
        HasPath = _navAgent.hasPath;
        PathPending = _navAgent.pathPending;
        PathStale = _navAgent.isPathStale;
        PathStatus = _navAgent.pathStatus;

        // If we no path and one isn't pending then set the next waypoint  as the target
        // otherwise if path is stale --> regenerate path
        if ((!HasPath && !PathPending) || PathStatus == NavMeshPathStatus.PathInvalid /*|| PathStatus==NavMeshPathStatus.PathPartial*/)
            SetNextDestination(true);
        else
        if (_navAgent.isPathStale)
            SetNextDestination(false);

    }
}