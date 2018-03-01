using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIZombieState : AIState
{

    protected int _playerLayerMask = -1;
    protected int _bodyPartLayer = -1;
    protected AIZombieStateMachine _zombieStateMachine = null;



    private void Awake()
    {
        // Get a mask. (+1) hack = default layer
        _playerLayerMask = LayerMask.GetMask("Player", "AI Body Part")+1;

        // Get the layer index of the AI Body Part layer
        _bodyPartLayer = LayerMask.NameToLayer("AI Body Part");
    }




    public override void SetStateMachine(AIStateMachine stateMachene)
    {
        // Safety check
        if (_stateMachine.GetType () == typeof (AIZombieStateMachine))
        {
            base.SetStateMachine(stateMachene);
            _zombieStateMachine = (AIZombieStateMachine)stateMachene;

        }
    }





    // Identify different threats

    public override void OnTriggerEvent(AITargetEventType eventType, Collider other)
    {

        // base.OnTriggerEvent(eventType, other);
        // If we dont have the parent state machine 
        if (_zombieStateMachine == null)
            return;


        // Step in and process if the event is an enter
        if (eventType != AITargetEventType.Exit)
        {
            // What the type of the stored current event? 
            AITargetType curType = _zombieStateMachine.VisualThreat.type;

            // If it's not a player, examing the tag

        // If the entered collider is a Player
        if (other.CompareTag ("Player") )
            {
                // Calculate the distance from the zombie to the player,
                // sensorPosition - mesure the distance from the sensor from the heat not from the feet
                // --> store. Check if we store the player.

                // Get distance from the sensor to the collider
                float distance = Vector3.Distance(_zombieStateMachine.sensorPosition, other.transform.position);

                //If the current stored - is not a Player or if the Player is closer than a player previusly stored as a visual threat -->
                if  (curType != AITargetType.Visual_Player ||
                             (curType == AITargetType.Visual_Player && distance < _zombieStateMachine.VisualThreat.distance))
                {
                    // Is the collider within a view cone
                    RaycastHit hitInfo;
                    if (ColliderIsVisible (other, out hitInfo, _playerLayerMask))
                    {
                        // Set as a new visual target
                        _zombieStateMachine.VisualThreat.Set(AITargetType.Visual_Player, other, other.transform.position, distance);
                    }

                }

            }

            else
                if (other.CompareTag ("Flash Light") && curType != AITargetType.Visual_Player)
            {
                BoxCollider flashLightTrigger = (BoxCollider)other;

                // Distance 
                float distanceToThreat = Vector3.Distance(_zombieStateMachine.sensorPosition, flashLightTrigger.transform.position);

                // Calcalate the world space z of the box
                float zSize = flashLightTrigger.size.z * flashLightTrigger.transform.lossyScale.z;


                // Calculate if zombie is react on this. (different zombies has different sight, ex: Zombie reaction varies depands if zombie is feading now, his intelange)
                float aggFactor = distanceToThreat / zSize;
                
                if (aggFactor <= _zombieStateMachine.sight && aggFactor <= _zombieStateMachine.intelligence)
                {
                    _zombieStateMachine.VisualThreat.Set(AITargetType.Visual_Light, other, other.transform.position, distanceToThreat);

                }


            }

        }
    }







    // Test the passed collider against the zombie's fov and using the passed layer mask for line of sight testing
    protected virtual bool ColliderIsVisible (Collider other, out RaycastHit hitInfo, int layerMask = -1)
    {
        // Return something
        hitInfo = new RaycastHit();

        // State Machine sets
        if (_zombieStateMachine == null) return false;


        // Calculate the angle between the sensor origin anf the direction of the collider
        Vector3 head = _stateMachine.sensorPosition;
        Vector3 direction = other.transform.position - head;
        float angle = Vector3.Angle(direction, transform.forward);


        // If the angle is greater than half of fov then it is outside the view cone ---> return false (no visability)
        if (angle > _zombieStateMachine.fov * 0.5f)
            return false;

        // Return all of hits 
        RaycastHit[] hits = Physics.RaycastAll(head, direction.normalized, _zombieStateMachine.sensorRadius * _zombieStateMachine.sight, layerMask);

        // Find the closesr collider that is not the AIs own body part
        float closestColliderDistance = float.MaxValue;
        Collider closestCollider = null;

        // Step through all of the hits
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            // Is this is the closest hit than any other previously found and stored
            if (hit.distance < closestColliderDistance)
            {

                // If the hit is on the body part layer
                if (hit.transform.gameObject.layer == _bodyPartLayer)
                {
                    //Assume it is not our own body part
                    if (_stateMachine != GameSceneManager.instance.GetAIStateMachine(hit.rigidbody.GetInstanceID()))
                    {
                        // Store the collider, distance and hit information
                        closestColliderDistance = hit.distance;
                        closestCollider = hit.collider;
                        hitInfo = hit;

                    }

                }
                else
                {

                    // It is not a body part --> store this as a new closest hit that have found
                    closestColliderDistance = hit.distance;
                    closestCollider = hit.collider;
                    hitInfo = hit;

                }
            }
        }


        if (closestCollider && closestCollider.gameObject == other.gameObject)
           return true; // We found collider and clossest thing we hit is the thing that we were  testing



        return false;

    }
}
