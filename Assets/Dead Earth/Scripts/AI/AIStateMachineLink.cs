using UnityEngine;
using System.Collections;

// Should be used as the base class for any StateMachineBehaviour that needs to communicate 
// with its AI State Machine

public class AIStateMachineLink : StateMachineBehaviour
{
    // The AI State Machine reference
    protected AIStateMachine _stateMachine;
    public AIStateMachine stateMachine { set { _stateMachine = value; } }
}
