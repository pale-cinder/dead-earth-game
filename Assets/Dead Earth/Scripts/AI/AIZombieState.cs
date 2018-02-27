using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIZombieState : AIState
{
    // Identify different threats

    public override void OnTriggerEvent(AITriggerEventType eventType, Collider other)
    {

        // base.OnTriggerEvent(eventType, other);

        if (_stateMachine == null)
            return;

        if (eventType != AITriggerEventType.Exit)
        {
            AITriggerEventType curType = _stateMachine.VisualThreat;

            // If it's not a player, examing the tag

        if (other.CompareTag ("Player") )
            {
                // Calculate the distance from the zombie to the player
                Vector3 distance = Vector3.Distance(_stateMachine.sensorPosition, other.transform.position);
                if  curType != AITargetType.Visual_Player ||
                    curType == AITargetType.Visual_Player&& distance <_stateMachine.VisualThreat.distance))
                    {


                    }

            }


        }
    }


}
