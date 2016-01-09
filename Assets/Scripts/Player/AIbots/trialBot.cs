using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class trialBot : MonoBehaviour, Brain {

	//some definitions for gamestate info. unique bots can use these players + gamelogic to extract game state info as they need that's not private (oh, reminds me...better make certain things private--ex don't want bots to read cards in deck or hand)
	public Player opponent1; //who goes right after you
	public Player opponent2; //who goes right before you
	public Player me;

	void Start(){
		me = gameObject.GetComponent<Player> ();
		switch (gameObject.name) {
		case "Player1":
			opponent1 = GameObject.Find("Player2").GetComponent<Player>();
			opponent2 = GameObject.Find("Player3").GetComponent<Player>();
			break;
		case "Player2":
			opponent1 = GameObject.Find("Player3").GetComponent<Player>();
			opponent2 = GameObject.Find("Player1").GetComponent<Player>();
			break;
		default:
			opponent1 = GameObject.Find("Player1").GetComponent<Player>();
			opponent2 = GameObject.Find("Player2").GetComponent<Player>();
			break;
		}
	}

	public List<Card> getAllSpecialPositives(){
		List<Card> rtn = new List<Card> ();
		foreach (Card c in me.hand) {
			if (c!= null && c.type == "SpecialPositive"){
				rtn.Add(c);
			}
		}
		return rtn;
	}

	//BELOW are the Brain interface functions that must be implemented to give this Bot it's functionality. ABOVE Use opponent definitions (hand,deck length, list of cards played) and GameLogic statics (tileWithPiece, playerWhoseTurnItis) and gameObject.getcomponentPlayer(own player info) to help make an informed decision

	//let this bot 1st draw a card, then play a random positive special

	//1st it'll draw a card
	public string[] doPhaseTwoEventOne(){
		string[] action = new string[2];
		action [0] = "draw";
		action [1] = "0";
		return action;
	}

	//then it'll play a random positive special (but if none availabe, it'll draw)
	public string[] doPhaseTwoEventTwo(){
		string[] action = new string[2];
		action [0] = "draw";

		action [1] = "0";		
		string cardName;
		List<Card> specialPositives = getAllSpecialPositives ();
		if (specialPositives.Count > 0){
			int randomIndex = Random.Range (0, specialPositives.Count);
			cardName = specialPositives [randomIndex].name;
			specialPositives.RemoveAt (randomIndex);
		
			action [0] = cardName;
		}
			
		return action;
	}

	public string[] doPhaseThree(){
		string[] action = new string[2];
		return action;
	}

	public string doCounterAttack(int attackerID, int attackVal){
		return "virtualattack";
	}

	public string movePiece(string[] spaces){
		return "M";
	}

	public override string ToString ()
	{
		return "trialBot";
	}
}
