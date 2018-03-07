using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIZombieState_Idle1 : AIZombieState
{

    [SerializeField] Vector2 _idleTimeRange = new Vector2(10.0f, 60.0f);

    float _idleTime = 0.0f;
    float _timer = 0.0f;



    public override AIStateType GetStateType()
    {

        

        return AIStateType.Idle;

    }


    public override void OnEnterState()
    {

        Debug.Log("Enterinf Idle State");


        base.OnEnterState();

        if (_zombieStateMachine == null)
            return;

        _idleTime = Random.Range(_idleTimeRange.x, _idleTimeRange.y);
        _timer = 0.0f;

        _zombieStateMachine.NavAgentControl(true, false);
        _zombieStateMachine.speed = 0;
        _zombieStateMachine.seeking = 0;
        _zombieStateMachine.feeding = false;
        _zombieStateMachine.attackType = 0;
        _zombieStateMachine.ClearTarget ();

        _zombieStateMachine.ClearTarget();
        
    }

    public override AIStateType OnUpdate()
    {



        return AIStateType.Idle;

    }

}
