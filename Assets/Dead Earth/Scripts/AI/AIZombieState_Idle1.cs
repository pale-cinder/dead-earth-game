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
        _zombieStateMachine.ClearTarget();

        _zombieStateMachine.ClearTarget();

    }


    // Called by the State Machine each time
    public override AIStateType OnUpdate()
    {

        // Examing the visual threat and checking if this is the most damgerous threat  for the player
        if (_zombieStateMachine == null)
            return AIStateType.Idle;

            if (_zombieStateMachine.VisualThreat.type == AITargetType.Visual_Player)
            {
            // Call target trigger to be placed
            _zombieStateMachine.SetTarget(_zombieStateMachine.VisualThreat);
            return AIStateType.Pursuit;
            }

            // When the zombie sees the players light
            if (_zombieStateMachine.VisualThreat.type == AITargetType.Visual_Light)
            {
                // It is in front of the zombie.
                // If the player didnt visible and it is the light -->
                // Zombie starting to run to the side where the light comes from
                _zombieStateMachine.SetTarget(_zombieStateMachine.VisualThreat);
                return AIStateType.Alerted;
            }

            if (_zombieStateMachine.AudioThreat.type == AITargetType.Audio)
            {
                // The sounds goes on
                _zombieStateMachine.SetTarget(_zombieStateMachine.AudioThreat);
                return AIStateType.Alerted;
            }

            if (_zombieStateMachine.VisualThreat.type == AITargetType.Visual_Food)
            {
                _zombieStateMachine.SetTarget(_zombieStateMachine.VisualThreat);
                return AIStateType.Pursuit;

            }

            _timer += Time.deltaTime;

            if (_timer > _idleTime)
            {

                Debug.Log("Going to Patrol");
                return AIStateType.Patrol;
            }




            return AIStateType.Idle;

        

    }
}


