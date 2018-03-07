using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIZombieState_Idle1 : AIZombieState
{

    public override AIStateType GetStateType()
    {

        Debug.Log("State Type being fetched by state machine");

        return AIStateType.Idle;

    }




    public override AIStateType OnUpdate()
    {



        return AIStateType.Idle;

    }

}
