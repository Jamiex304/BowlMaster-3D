using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneBox : MonoBehaviour {

	private PinSetter pinsetter;

	// Use this for initialization
	void Start () {
		pinsetter = GameObject.FindObjectOfType<PinSetter>();
	}

	void OnTriggerExit(Collider collider){
		if(collider.gameObject.name == "Bowling_Ball"){
			pinsetter.ballOutOfPlay = true;
		}
	}
}
