using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorState { Open, Animation, Closed };


public class SlidingDoorDemo : MonoBehaviour
{

    public float SlidingDistance = 2.0f;
    public float Duration = 1.5f;
    public AnimationCurve JumpCurve = new AnimationCurve();

    private Transform _transform = null;
    private Vector3 _openPos = Vector3.zero;
    private Vector3 _closedPose = Vector3.zero;
    private DoorState _doorState = DoorState.Closed;


	// Use this for initialization
	void Start ()
    {
        _transform = transform;
        _closedPose = _transform.position;
        _openPos = _closedPose + (_transform.right * SlidingDistance);

	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Space)&& _doorState!=DoorState.Animation)

            //Checking if the door is closed or open
           
        {
            StartCoroutine(AnimateDoor((_doorState == DoorState.Open) ? DoorState.Closed : DoorState.Open));
        }
	}

    IEnumerator AnimateDoor (DoorState newState)
    {
        //Don't reanimate the door if the door was already was animated

        _doorState = DoorState.Animation;

        //Specified by time (that set to 0 in the begining)

        float time = 0.0f;

        Vector3 startPos = (newState == DoorState.Open) ? _closedPose : _openPos;
        Vector3 endPos = (newState == DoorState.Open) ? _openPos : _closedPose;

        while (time<=Duration)
        {
            float t = time / Duration;
            _transform.position = Vector3.Lerp(startPos, endPos, JumpCurve.Evaluate(t));

            //Update time

            time += Time.deltaTime;

            yield return null;

        }

        //Forse set to end position (just in case)

        _transform.position = endPos;

        _doorState = newState;

    }

}
