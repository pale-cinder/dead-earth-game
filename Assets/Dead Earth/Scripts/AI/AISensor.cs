using UnityEngine;
using System.Collections;


// Notifies the parent AIStateMachine of any threats that
// enter its trigger via the AIStateMachine's OnTriggerEvent
// method

public class AISensor : MonoBehaviour
{
    // Private
    private AIStateMachine _parentStateMachine = null;
    public AIStateMachine parentStateMachine { set { _parentStateMachine = value; } }

    void OnTriggerEnter(Collider col)
    {
        if (_parentStateMachine != null)
            _parentStateMachine.OnTriggerEvent(AITargetEventType.Enter, col);
    }

    void OnTriggerStay(Collider col)
    {
        if (_parentStateMachine != null)
            _parentStateMachine.OnTriggerEvent(AITargetEventType.Stay, col);
    }

    void OnTriggerExit(Collider col)
    {
        if (_parentStateMachine != null)
            _parentStateMachine.OnTriggerEvent(AITargetEventType.Exit, col);
    }

}
