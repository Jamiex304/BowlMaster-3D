using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	//public Vector3 launchVelocity;
	
	private Rigidbody rigidBody;
	private AudioSource audioSource;
	private Vector3 ballStartPosition;

	public bool inPlay = false;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();

		//Setting Gravity to False until Launch is called
		rigidBody.useGravity = false;

		//Capture Start Position
		ballStartPosition = transform.position;
	}

	//Launch Command for calling the Ball
	public void Launch(Vector3 velocity){
		inPlay = true;
		rigidBody.useGravity = true;
		rigidBody.velocity = velocity;
		audioSource.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Reset(){
		Debug.Log("Resetting Ball");
		inPlay = false;
		transform.position = ballStartPosition;
		rigidBody.velocity = Vector3.zero;
		rigidBody.angularVelocity = Vector3.zero;
		rigidBody.useGravity = false;
	}
}
