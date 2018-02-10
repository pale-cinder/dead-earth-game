﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Animator _animator = null;
    private int _horizontalHash = 0;
    private int _verticalHash = 0;


	// Use this for initialization
	void Start ()
    {
        _animator = GetComponent<Animator>();
        _horizontalHash = Animator.StringToHash("Horizontal");
        _verticalHash = Animator.StringToHash("Vertical");

	}
	
	// Update is called once per frame
	void Update ()
    {
        //  * 2.32f and * 5.72f max values in the blend tree
        float xAxis = Input.GetAxis("Horizontal") * 2.32f;
        float yAxis = Input.GetAxis("Vertical") * 5.72f;
        
        // --!play with speed for slowing down and going faster
        _animator.SetFloat(_horizontalHash, xAxis, 0.8f, Time.deltaTime);
        _animator.SetFloat(_verticalHash, yAxis, 1.0f, Time.deltaTime);
	}
}
