using System.Collections;
using UnityEngine;


// Base class of all AI States used by AI System
public abstract class AIState : MonoBehaviour
{
    // Called by the parent state machine to assign to its reference
    public virtual void SetStateMachine (AIStateMachine stateMachene) { _stateMachine = stateMachene; }

    //public abstract AIStateType GetStateType;

  
    public virtual void OnEnterState() { }
    public virtual void OnExitState() { }
    //public abstract AIStateType OnUpdate() { }

    //public virtual void OnAnimatorUpdated() { }
    
    public virtual void OnAnimatorIKUpdated() { } //such as using humanoid avatar

    public virtual void OnTriggerEvent(AITargetEventType eventType, Collider other) { } //Collider that generated the ivent 

    public virtual void OnDestinationReached(bool isReached) { } //if we reached the target or not

    

    public abstract AIStateType GetStateType();
    public abstract AIStateType OnUpdate();

  

    protected AIStateMachine _stateMachine;



    public virtual void OnAnimatorUpdated()
    {
        // Get the number of meters the root motion has updated for this update and divide by deltaTime to get meters per second. We then assign this to the nav agent's velocity.
        if (_stateMachine.useRootPosition)
            _stateMachine.NavAgent.velocity = _stateMachine.animator.deltaPosition / Time.deltaTime;

        // Grab the root rotation from the animator and assign as our transform's rotation.
        if (_stateMachine.useRootRotation)
            _stateMachine.transform.rotation = _stateMachine.animator.rootRotation;

    }

    // Convert the passed sphere collider's position and radius into world space taking into acount hierarchical scaling
    public static void ConvertSphereColliderToWorldSpace (SphereCollider col, out Vector3 pos, out float radius)
    {
        pos = Vector3.zero;
        radius = 0.0f;

        if (col == null)
            return;

        // Convert into world space
        // Calculate and store the world possition of sphere center
        pos = col.transform.position;
        pos.x += col.center.x * col.transform.lossyScale.x;
        pos.y += col.center.y * col.transform.lossyScale.y;
        pos.z += col.center.z * col.transform.lossyScale.z;

        // Calculate world space radius od sphere
        radius = Mathf.Max( col.radius * col.transform.lossyScale.x,
                            col.radius * col.transform.lossyScale.y);

        radius = Mathf.Max( radius, col.radius * col.transform.lossyScale.z);

    }

}
