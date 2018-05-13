using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIZombieState_Patrol1 : AIZombieState

{
    [SerializeField] AIWaypointNetwork aIWaypointNetwork = null;
    [SerializeField] bool _randomPartol = false; // (zombie is not fallowing the exact same point we put down
    [SerializeField] int _currentWaypint = 0;
    [SerializeField] [Range (0.0f, 3.0f)] float _speed = 1.0f; // for smooth transition between states 


    // Inspector assighn
    public override AIStateType GetStateType()
    {
        return AIStateType.Patrol;

    }

    public override AIStateType OnUpdate()
    {
        return AIStateType.Patrol;
    }


}
