using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

	//Variables
	public float standingThreshold = 3f;

	private float distanceToRaise = 45f;
	private Rigidbody rigidBody;

	void Awake () {
    	this.GetComponent<Rigidbody>().solverVelocityIterations = 10;
	}

	void Start(){
		//print(name + IsStanding());
		rigidBody = GetComponent<Rigidbody>();
	}

	void Update(){
		//print(name + IsStanding());
	}

	public bool IsStanding() { 
		float tiltX = (transform.eulerAngles.x < 180f) ? transform.eulerAngles.x : 360 - transform.eulerAngles.x;
		float tiltZ = (transform.eulerAngles.z < 180f) ? transform.eulerAngles.z : 360 - transform.eulerAngles.z; 

		if (tiltX > standingThreshold || tiltZ > standingThreshold) {
		 	return false; 
		}else {
			return true;
		}
	}

	public void RaiseIfStanding(){
		if(IsStanding()){
			rigidBody.useGravity = false;
			transform.Translate(new Vector3(0, distanceToRaise,0),Space.World);
		}	
	}

	public void Lower(){
		transform.Translate(new Vector3(0, -distanceToRaise,0),Space.World);
		rigidBody.useGravity = true;
	}
}
