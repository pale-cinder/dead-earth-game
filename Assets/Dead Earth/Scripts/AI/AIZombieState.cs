using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIZombieState : AIState
{

    protected int _playerLayerMask = -1;


    private void Awake()
    {
        _playerLayerMask = LayerMask.GetMask("Player", "AI Body Part")+1;
    }



    // Identify different threats

    public override void OnTriggerEvent(AITargetEventType eventType, Collider other)
    {

        // base.OnTriggerEvent(eventType, other);

        if (_stateMachine == null)
            return;

        if (eventType != AITargetEventType.Exit)
        {
            AITargetType curType = _stateMachine.VisualThreat.type;

            // If it's not a player, examing the tag

        if (other.CompareTag ("Player") )
            {
                // Calculate the distance from the zombie to the player,
                // sensorPosition - mesure the distance from the sensor from the heat not from the feet
                // --> store. Check if we store the player.
                float distance = Vector3.Distance(_stateMachine.sensorPosition, other.transform.position);
                if  (curType != AITargetType.Visual_Player ||
                             (curType == AITargetType.Visual_Player && distance < _stateMachine.VisualThreat.distance))
                {
                    RaycastHit hitInfo;
                    if (ColliderIsVisible (other, out hitInfo, _playerLayerMask))
                    {

                    }

                }

            }


        }
    }

    protected virtual bool ColliderIsVisible (Collider other, out RaycastHit hitInfo, int layerMask = -1)
    {
        hitInfo = new RaycastHit();

        if (_stateMachine == null || _stateMachine.GetType()!=typeof(AIZombieStateMachine) ) 
            return false;

        AIZombieStateMachine zombieMachine = (AIZombieStateMachine)_stateMachine;

        Vector3 head = _stateMachine.sensorPosition;
        Vector3 direction = other.transform.position - head;
        float angle = Vector3.Angle(direction, transform.forward);

        if (angle > zombieMachine.fov * 0.5f)
            return false;

        // Return all of the hits the ray
        RaycastHit[] hits = Physics.RaycastAll(head, direction.normalized, _stateMachine.sensorRadius * zombieMachine.sight, layerMask);
        return false;

    }
}
