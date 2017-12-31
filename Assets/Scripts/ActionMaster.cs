using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster{

	public enum Action{Tidy, Reset, EndTurn, EndGame};

	private int[] bowls = new int[21];
	private int bowl = 1;

	public Action Bowl(int pins){
		if(pins < 0 || pins > 10){throw new UnityException("Invalid Pin Count");}

		bowls [bowl - 1] = pins;

		if (bowl == 21){
			return Action.EndGame;
		}

		//Last Frame
		if(bowl >= 19 && pins == 10){
			bowl ++;
			return Action.Reset;
		}else if(bowl == 20){
			bowl ++;
			if(bowls [19-1]==10 && bowls [20 -1]==0){
				return Action.Tidy;
			}else if((bowls [19-1] + bowls [20 -1]) % 10 == 0){
				return Action.Reset;
			}else if(Bowl21Awarded()){
				return Action.Tidy;
			}else {
				return Action.EndGame;
			}
		}

		//1st Turn (Less Than 10)
		if(bowl % 2 != 0){//First Bowl of Frames 1-9
			if(pins == 10){
				bowl += 2;
				return Action.EndTurn;
			}
			else{
				bowl += 1;
				return Action.Tidy;
			}
		}else if (bowl % 2 == 0){//Second bowl of frames 1-9
			bowl +=1;
			return Action.EndTurn;
		}

		throw new UnityException("Not Sure What Action to Return");
	}

	private bool Bowl21Awarded(){
		//Remember Arrays Start At 0
		return(bowls [19-1] + bowls [20-1] >= 10);
	}
}
