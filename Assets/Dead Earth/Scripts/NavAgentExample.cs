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


    //Private Members 
    private NavMeshAgent _navAgent = null;


    // Use this for initialization
    void Start()
    {

        _navAgent = GetComponent<NavMeshAgent>();

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

            CurrentIndex++;



        }

	}



