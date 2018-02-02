using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class NavAgentExample : MonoBehaviour
{
    private NavMeshAgent _navAgent = null;


	// Use this for initialization
	void Start ()
    {

        _navAgent = GetComponent<NavMeshAgent>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
