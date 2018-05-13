using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIZombieState_Patrol1 : AIZombieState

{
    [SerializeField] AIWaypointNetwork aIWaypointNetwork = null;

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
