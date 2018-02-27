using System.Collections;
using UnityEngine;

public abstract class AIState : MonoBehaviour
{
    public void SetStateMachine (AIStateMachine stateMachene) { _stateMachine = stateMachene; }

    //public abstract AIStateType GetStateType;

  
    public virtual void OnEnterState() { }
    public virtual void OnExitState() { }
    //public abstract AIStateType OnUpdate() { }

    //public virtual void OnAnimatorUpdated() { }
    
    public virtual void OnAnimatorIKUpdated() { } //such as using humanoid avatar

    public virtual void OnTriggerEvent(AITriggerEventType eventType, Collider other) { } //Collider that generated the ivent 

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
}
