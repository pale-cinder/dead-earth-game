using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIZombieState_Patrol1 : AIZombieState

{
    [SerializeField] AIWaypointNetwork aIWaypointNetwork = null;
    [SerializeField] bool _randomPartol = false; // (zombie is not fallowing the exact same point we put down
    [SerializeField] int _currentWaypint = 0;
    [SerializeField] [Range (0.0f, 3.0f)] float _speed = 1.0f; // for smooth transition between states 


    // Inspector assign
    public override AIStateType GetStateType()
    {
        return AIStateType.Patrol;

    }



    public override void OnEnterState()
    {

        Debug.Log("Enterinf Patrol State");

        // Call base class implementation
        base.OnEnterState();

        // Check if it valid, if not then simply return
        if (_zombieStateMachine == null)
            return;
                
        _zombieStateMachine.NavAgentControl(true, false); // Do not control the rotation
        _zombieStateMachine.speed = _speed;

        // Next we are not seeking, not feeding, not attaking 
        _zombieStateMachine.seeking = 0;
        _zombieStateMachine.feeding = false;
        _zombieStateMachine.attackType = 0;

        // If we need to do a very sharp turn --> drop down the temporary state into the allarted state
        // Get the target into the fild of view

        if (_zombieStateMachine.targetType ! = AITargetType.Waypoints)
        {


            _zombieStateMachine.ClearTarget();

            if (_waypointNetwork ! = null && _waypointNetwork.Waypoint.Count > 0 )
            {
                if (_randomPartol)
                {
                    _currentWaypint = Random.Range (0, _waypointNetwork.Waypoints.Count-1)
                }

                if ()
                Transform waypoint = _waypointNetwork.Waypoints[_currentWaypint];
                if (waypoint != null)


            }
        }

    }




    public override AIStateType OnUpdate()
    {
        return AIStateType.Patrol;
    }


}
