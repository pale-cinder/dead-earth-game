using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIStateType { None, Idle, Alerted, Patrol, Attack, Feeding, Pursuit, Dead }
public enum AITargetType { None, Waypoint, Visual_Player, Visual_Light, Visual_Food, Audio }
public enum AITriggerEventType { enter, Stay, Exit };

// Potential targets to the AI System
public struct AITarget
{
    //Target type
    private AIStateType _type; 

    private Collider _collider;

    //Current possition in the world
    private Vector3 _position;

    //Distance from player
    private float _distance;

    //Time the target was ping'd at last
    private float _time;





    public AIStateType type { get { return _type; } }

    private Collider { get { return _collider; } }

    private Vector3  { get { return _position; } } 

    private float { get { return _distance; } } 

    private float { get { return _time; } } 


    
    public void Set (AITargetType t, Collider c, Vector3 p, float d)

{

    _type = t;
    _collider = c;
    _possition = p;
    _distance = d;
    _tyme = Time.time;

}




public void Clear()
{
    _type = AITargetType.None;
    _collider = null;
    _position = Vector3.zero;
    _time = 0.0f;
    _distance = Mathf.Infinity;
}
}


public abstract class AIStateMachine : MonoBehaviour
{

    //First of all - what the current state is

    //Key to the dictionary                   

    public AITarget VisualThread = new AITarget();
    public AITarget AudioThread = new AITarget();


    protected AIState _currentState = null;    
    protected Dictionary<AIStateType, AIState > _states = new Dictionary<AIStateType, AIState > ();
    protected AITarget _target = new AITarget();


    //Inspector
    [SerializeField] protected AIStateType _currentStateType = AIStateType.Idle;
    [SerializeField] protected SphereCollider _targetTrigger = null;
    [SerializeField] protected SphereCollider _sensorTrigger = null;

    [SerializeField] [Range(0, 15)] protected float _stoppingDistance = 1.0f;
    

    //Component cache
    protected Animator _animator = null;
    protected NavMeshAgent _navAgent = null;
    protected Collider _collider = null;
    protected Transform _transform = null;

    // Public Properties for component
    public Animator animator { get { return _animator; } }
    public NavMeshAgent navAgent { get { return _navAgent; } }
    public Vector3 sensorPosition


    {
        get
        {
            if (_sensorTrigger == null) return Vector3.zero;
            Vector3 point = _sensorTrigger.transform.position;
            point.x += _sensorTrigger.center.x * _sensorTrigger.transform.lossyScale.x;
            point.y += _sensorTrigger.center.y * _sensorTrigger.transform.lossyScale.y;
            point.z += _sensorTrigger.center.z * _sensorTrigger.transform.lossyScale.z;
            return point;
        }
    }


    public float sensorRadius
    {
        get
        {
            if (_sensorTrigger == null) return 0.0f;
            float radius = Mathf.Max(_sensorTrigger.radius * _sensorTrigger.transform.lossyScale.x,
                                        _sensorTrigger.radius * _sensorTrigger.transform.lossyScale.y);

            return Mathf.Max(radius, _sensorTrigger.radius * _sensorTrigger.transform.lossyScale.z);
        }
    }

    public bool useRootPosition { get { return _rootPositionRefCount > 0; } }
    public bool useRootRotation { get { return _rootRotationRefCount > 0; } }


    protected virtual void Awake()
    {
        _transform = transform;
        _animator = GetComponent<Animator>();
        _navAgent = GetComponent<NavMeshAgent>();
        _collider = GetComponent<Collider>();
    }


    // Do we have a valid Game Scene Manager
		if (GameSceneManager.instance!=null)
		{
			// Register State Machines with Scene Database
			if (_collider) GameSceneManager.instance.RegisterAIStateMachine(_collider.GetInstanceID(), this );
			if (_sensorTrigger) GameSceneManager.instance.RegisterAIStateMachine(_sensorTrigger.GetInstanceID(), this );
}

	} 


    protected virtual void Start ()

    {
       // AIState[] states = GetComponent<AIState>();
        
        //Make shure that the dictionary doesn't have an AI state that has AIState key

        //Check the valid
        //Loop through all states and addd them to the state dictionary
        
        foreach (AIState state in states)
        {
         // if (state! = null && !_states.ContainsKey(state.GetStateType)))

        if (_sensorTrigger != null)

        {
            //Add state to the state dictionary
            AISensor script = _sensorTrigger.GetComponent<AISensor>();
            if (script != null)
            {
                script.parentStateMachine = this;
            }

        }

        // Set the current state
        if (_states.ContainsKey(_currentStateType))
        {
            _currentState = _states[_currentStateType];
            _currentState.OnEnterState();

        }

        else
        {
            _currentState = null;
        }

        if (_animator)
        {
            AIStateMachineLink[] scripts = _animator.GetBehaviours<AIStateMachineLink>();
            foreach (AIStateMachineLink script in scripts)
            {
                script.stateMachine = this;
            }
        }

    }



    // Sets the current target 
    public void SetTarget(AITargetType t, Collider c, Vector3 p, float d)
    {
        // Set the target info
        _target.Set(t, c, p, d);

        // Configure and enable the target trigger at the correct position and with the correct radius
        if (_targetTrigger != null)
        {
            _targetTrigger.radius = _stoppingDistance;
            _targetTrigger.transform.position = _target.position;
            _targetTrigger.enabled = true;
        }
    }



    // Sets the current target 
    // Specifying a custom stopping distance
        public void SetTarget(AITargetType t, Collider c, Vector3 p, float d, float s)
    {
        // Set the target Data
        _target.Set(t, c, p, d);

        // Configure and enable the target trigger at the correct position and with the correct radius
        if (_targetTrigger != null)
        {
            _targetTrigger.radius = s;
            _targetTrigger.transform.position = _target.position;
            _targetTrigger.enabled = true;
        }
    }



    // Sets the current target and the target trigger
    public void SetTarget(AITarget t)
    {
        // Assign the new target
        _target = t;

        // Configure and then enable the target trigger at the correct position and with the correct radius
        if (_targetTrigger != null)
        {
            _targetTrigger.radius = _stoppingDistance;
            _targetTrigger.transform.position = t.position;
            _targetTrigger.enabled = true;
        }
    }



    //Clears the current target when it no longer in use
       public void ClearTarget()
    {
        _target.Clear();
        if (_targetTrigger != null)
        {
            _targetTrigger.enabled = false;
        }
    }


    //clears the audio and visual threats each update, re-calculates the distance to the current target
    protected virtual void FixedUpdate()
    {
        //clear each update
        VisualThreat.Clear();
        AudioThreat.Clear();

        if (_target.type != AITargetType.None)
        {
            _target.distance = Vector3.Distance(_transform.position, _target.position);
        }
    }

    protected virtual void Update()
    {
        if (_currentState == null) return;

        AIStateType newStateType = _currentState.OnUpdate();

        if (newStateType != _currentStateType)
        {
            AIState newState = null;
            if (_states.TryGetValue(newStateType, out newState))  //Check if the key exists in the dictionary
            {
                _currentState.OnExitState(); 
                newState.OnEnterState();
                _currentState = newState;
            }


            else

            if (_states.TryGetValue(AIStateType.Idle, out newState))
            {
                _currentState.OnExitState();
                newState.OnEnterState();
                _currentState = newState;
            }

            _currentStateType = newStateType;
        }
    }


protected virtual void OnTriggerEnter(Collider other)
{
    if (_targetTrigger == null || other != _targetTrigger) return;

    // Notify Child State
    if (_currentState)
        _currentState.OnDestinationReached(true);
}

public void OnTriggerExit(Collider other)
{
    if (_targetTrigger == null || _targetTrigger != other) return;
    if (_currentState != null)
        _currentState.OnDestinationReached(false);
}
public virtual void OnTriggerEvent(AITriggerEventType type, Collider other)
{
    if (_currentState != null)
        _currentState.OnTriggerEvent(type, other);
}
protected virtual void OnAnimatorMove()
{
    if (_currentState != null)
        _currentState.OnAnimatorUpdated();
}

// ----------------------------------------------------------
// Name	: OnAnimatorIK
// Desc	: Called by Unity just prior to the IK system being
//		  updated giving us a chance to setup up IK Targets
//		  and weights.
// ----------------------------------------------------------
protected virtual void OnAnimatorIK(int layerIndex)
{
    if (_currentState != null)
        _currentState.OnAnimatorIKUpdated();
}

// ----------------------------------------------------------
// Name	:	NavAgentControl
// Desc	:	Configure the NavMeshAgent to enable/disable auto
//			updates of position/rotation to our transform
// ----------------------------------------------------------
public void NavAgentControl(bool positionUpdate, bool rotationUpdate)
{
    if (_navAgent)
    {
        _navAgent.updatePosition = positionUpdate;
        _navAgent.updateRotation = rotationUpdate;
    }
}

// ----------------------------------------------------------
// Name	:	AddRootMotionRequest
// Desc	:	Called by the State Machine Behaviours to
//			Enable/Disable root motion
// ----------------------------------------------------------
public void AddRootMotionRequest(int rootPosition, int rootRotation)
{
    _rootPositionRefCount += rootPosition;
    _rootRotationRefCount += rootRotation;
}

}
