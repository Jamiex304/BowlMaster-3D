using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ball))]
public class DragLaunch : MonoBehaviour {

	private Ball ball;
	private Vector3 dragStart, dragEnd;
	private float startTime, endTime;

	//Test Code (To Be Removed)
	public bool developerTest;
	public Vector3 developerTestVelocity;

	// Use this for initialization
	void Start () {
		ball = GetComponent<Ball>();
	}

	public void DragStart(){
		//Capture time and posistion of drag start
		dragStart = Input.mousePosition;
		startTime = Time.time;
	}

	public void DragEnd(){
		//Launch the ball
		dragEnd = Input.mousePosition;
		endTime = Time.time;

		float dragDuration = endTime - startTime;

		float launchSpeedX = (dragEnd.x - dragStart.x) / dragDuration;
		float launchSpeedZ = (dragEnd.y - dragStart.y) / dragDuration;

		//Note developerTest & developerTestVelocity to be removed just in place to auto roll the ball correctly
		if (developerTest == false){
			Vector3 launchVelocity = new Vector3(launchSpeedX, 0, launchSpeedZ);
			if(ball.inPlay == false){
				ball.Launch(launchVelocity);
			}
		}else {
			ball.Launch(developerTestVelocity);
		}

	}

	public void MoveStart (float amount){
		//Debug.Log ("Ball Moved " + amount);
		if(! ball.inPlay){
			ball.transform.Translate(new Vector3 (amount,0,0));
		}
	}
}
