using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public float Speed = 0.0f;

    private Animator _controller = null;


	// Use this for initialization
	void Start ()
    {
        _controller = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        _controller.SetFloat("Speed", Speed);
	}
}
