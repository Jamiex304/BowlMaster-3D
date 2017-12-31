using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

	//Variables
	public Text pinCountUIDisplay;
	public int lastStanding = -1;
	public GameObject pinSet;
	public bool ballOutOfPlay = false;

	private float lastChangeTime;
	private Ball ball;

	//Connecting PinSetter to ActionMasters
	private int lastSettledCount = 10;
	private ActionMaster actionMaster = new ActionMaster();

	//Connecting to Animator
	private Animator animator;

	// Use this for initialization
	void Start () {
		ball = GameObject.FindObjectOfType<Ball>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		//print(CountStanding());
		pinCountUIDisplay.text = CountStanding().ToString();

		if(ballOutOfPlay){
			UpdateStandingCountAndSettle();
			pinCountUIDisplay.color = Color.red;
		}
	}

	public void RaisePins(){
		//Only Raise Standing Pins only by the distancetoraise value
		Debug.Log("Raising Pins");
		foreach(Pin pin in GameObject.FindObjectsOfType<Pin>()){
			pin.RaiseIfStanding();
		}
	}

	public void LowerPins(){
		//Place Pins back down on the ground
		Debug.Log("Lowering Pins");
		foreach(Pin pin in GameObject.FindObjectsOfType<Pin>()){
			pin.Lower();
		}
	}

	public void RenewPins(){
		//Spawns a new set of pins
		Debug.Log("Renewing Pins");
		Instantiate(pinSet, new Vector3(0,5,1829), Quaternion.identity);
	}

	void UpdateStandingCountAndSettle(){
		//Update the lastStanding Count
		//Call Method Pins have settled
		int currentStanding = CountStanding();

		if(currentStanding != lastStanding){
			lastChangeTime = Time.time;
			lastStanding = currentStanding;
			return;
		}

		float settleTime = 3f;//How Long to wait to consider the pins have settled and a final count can be got
		if((Time.time - lastChangeTime) > settleTime){//If last change more than 3seconds ago
			PinsHaveSettled();
		}
	}

	void PinsHaveSettled(){
		int standing = CountStanding();
		int pinFall = lastSettledCount - standing;
		lastSettledCount = standing;


		ActionMaster.Action action = actionMaster.Bowl(pinFall);
		Debug.Log(action);

		if(action == ActionMaster.Action.Tidy){
			animator.SetTrigger("tidyTrigger");
		}else if(action == ActionMaster.Action.EndTurn){
			animator.SetTrigger("resetTrigger");
			lastSettledCount = 10;
		}else if(action == ActionMaster.Action.Reset){
			animator.SetTrigger("resetTrigger");
			lastSettledCount = 10;
		}else if(action == ActionMaster.Action.EndGame){
			throw new UnityException ("Dont Know How to handle an End Game Yet");
		}

		ball.Reset();
		lastStanding = -1; //Indicates Pins have settle and a new round has not started yet (i.e the Ball has not entered the zone)
		ballOutOfPlay = false;
		pinCountUIDisplay.color = Color.green;
	}

	int CountStanding(){
		int standing = 0;

		foreach(Pin pin in GameObject.FindObjectsOfType<Pin>()){
			if(pin.IsStanding()){
				standing++;
			}	
		}

		return standing;
	}

	void OnTriggerExit(Collider collider){
		//print("Something Left the Area");
		GameObject thingLeft = collider.gameObject;

		if(thingLeft.GetComponentInParent<Pin>()){
			print("Pin Left");
			Destroy(thingLeft.transform.parent.gameObject);
		}
	}
}
