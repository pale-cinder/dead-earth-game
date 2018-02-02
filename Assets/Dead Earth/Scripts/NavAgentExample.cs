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
	void Start ()
    {

        _navAgent = GetComponent<NavMeshAgent>();

        if (WaypointNetwork == null) return;

        //If we may got forgot assign the transform

        if (WaypointNetwork.Waypoints[CurrentIndex] != null)
        {
            _navAgent.destination = WaypointNetwork.Waypoints[CurrentIndex].position;
        }



	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
