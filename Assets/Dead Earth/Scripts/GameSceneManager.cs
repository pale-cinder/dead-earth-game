using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour {


    // Store instances
    private static GameSceneManager _instance = null;

    //returns the game scene static object

    public static GameSceneManager instance
    {
        get
        {
            if (_instance == null)
                // Search the scene for the very first instance
                _instance = (GameSceneManager)FindObjectOfType(typeof(GameSceneManager));
            return _instance;
        }
    }

    
    private Dictionary<int, AIStateMachine> _stateMachines = new Dictionary<int, AIStateMachine>();

    
    public void RegisterAIStateMachine(int key, AIStateMachine stateMachine)
    {
        if (!_stateMachines.ContainsKey(key))
        {
            _stateMachines[key] = stateMachine;
        }
    }

    // Returns an AI State Machine reference searched on by the instance ID of an object
    public AIStateMachine GetAIStateMachine(int key)
    {

        //store result of the surching
        AIStateMachine machine = null;

        //passing key in dictionary
        if (_stateMachines.TryGetValue(key, out machine))
        {
            return machine;
        }

        return null;
    }


}
