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
    public bool MixedMode = true;

    //Private Members 
    private NavMeshAgent _navAgent = null;
    private Animator _animator = null;
    private float _smoothAngle = 0;


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

        
        //calculation of angle
        Vector3 localDesiredVelocity = transform.InverseTransformVector(_navAgent.desiredVelocity);
        float angle = Mathf.Atan2(localDesiredVelocity.x, localDesiredVelocity.z) * Mathf.Rad2Deg;
        _smoothAngle = Mathf.MoveTowardsAngle(_smoothAngle, angle, 80.0f * Time.deltaTime);

        float speed = localDesiredVelocity.z;


        _animator.SetFloat("Angle", _smoothAngle);
        _animator.SetFloat("Speed", speed, 0.1f, Time.deltaTime);

        if (_navAgent.desiredVelocity.sqrMagnitude > Mathf.Epsilon)
        {
            if (!MixedMode||
                (MixedMode&& Mathf.Abs(angle)<80.0f && _animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Locomotion") ))
            {
                Quaternion lookRotation = Quaternion.LookRotation(_navAgent.desiredVelocity, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5.0f * Time.deltaTime);
            }
            
        }
        


        
        //if we dont have a path
        if ((_navAgent.remainingDistance<=_navAgent.stoppingDistance && !PathPending) || PathStatus == NavMeshPathStatus.PathInvalid /*|| PathStatus==NavMeshPathStatus.PathPartial*/)
            
            //set the destination to the next waypoint
            SetNextDestination(true);
        else
        if (_navAgent.isPathStale)
            SetNextDestination(false);


       
    }

    // pop in up values before unity uses this values, calculate velocity --> set the final velocity to navAgent
    void OnAnimatorMove()
    {
        if (MixedMode && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Locomotion"))
            transform.rotation = _animator.rootRotation;


       // transform.rotation = _animator.rootRotation;
        _navAgent.velocity = _animator.deltaPosition / Time.deltaTime;

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