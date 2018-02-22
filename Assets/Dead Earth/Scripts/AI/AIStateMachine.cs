using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum AIStateType { None, Idle, Alerted, Patrol, Attack, Feeding, Pursuit, Dead }
public enum AITargerTyoe { None, Waypoint, Visual_Player, Visual_Light, Visual_Food, Audio }


public abstract class AIStateMachine : MonoBehaviour
{

    //First of all - what the current state is


    //Key to the dictionary                   

    //Private

    private Dictionary<AIStateType, AIState>    _states = new Dictionary<AIStateType, AIState > ();

    protected virtual void Start ()
    {
        AIState[] states = GetComponent<AIState>();
        
        //Make shure that the dictionary doesn't have an AI state that has AIState key

        //Check the valid

        foreach (AIState state in states)
        {
            if (state! = null && !_states.ContainsKey(state.GetStateType)))
                {
                _states[state.GetStateType()]
            }
        }
    }

}
