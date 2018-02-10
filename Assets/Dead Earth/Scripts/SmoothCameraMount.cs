using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraMount : MonoBehaviour

{

    public Transform Mount = null;
    public float Speed = 5.0f;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.position = Vector3.Lerp(transform.position, Mount.position, Time.deltaTime * Speed);

        transform.rotation = Quaternion.Slerp(transform.rotation, Mount.rotation, Time.deltaTime * Speed);
	}
}
