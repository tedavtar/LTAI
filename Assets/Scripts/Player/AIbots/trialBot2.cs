using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class trialBot2 : MonoBehaviour, Brain {
	
	//some definitions for gamestate info. unique bots can use these players + gamelogic to extract game state info as they need that's not private (oh, reminds me...better make certain things private--ex don't want bots to read cards in deck or hand)
	public Player opponent1; //who goes right after you
	public Player opponent2; //who goes right before you
	public Player me;

	//changed start to awake, got rid of NPE as me was null.
	void Awake(){
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

	public List<Card> getAllSpecialNegatives(){
		List<Card> rtn = new List<Card> ();
		foreach (Card c in me.hand) {
			if (c!= null && c.type == "SpecialNegative"){
				rtn.Add(c);
			}
		}
		return rtn;
	}

	public List<Card> getAllPortals(){
		List<Card> rtn = new List<Card> ();
		foreach (Card c in me.hand) {
			if (c!= null && c.type == "Portal"){
				rtn.Add(c);
			}
		}
		return rtn;
	}

	public List<Card> getAllAttacks(){
		List<Card> rtn = new List<Card> ();
		foreach (Card c in me.hand) {
			if (c!= null && c.type == "Attack"){
				rtn.Add(c);
			}
		}
		return rtn;
	}
	
	//BELOW are the Brain interface functions that must be implemented to give this Bot it's functionality. ABOVE Use opponent definitions (hand,deck length, list of cards played) and GameLogic statics (tileWithPiece, playerWhoseTurnItis) and gameObject.getcomponentPlayer(own player info) to help make an informed decision
	
	//let this bot 1st play random negative special (target next player), then play a random positive special
	
	//1st it'll play a random negative special if possible, else (if no such cards in hand) it'll draw
	public string[] doPhaseTwoEventOne(){
		string[] action = new string[2];
		action [0] = "draw";
		action [1] = "0";
		string cardName;
		List<Card> specialNegatives = getAllSpecialNegatives ();
		if (specialNegatives.Count > 0){
			int randomIndex = Random.Range (0, specialNegatives.Count);
			cardName = specialNegatives [randomIndex].name;
			//specialNegatives.RemoveAt (randomIndex);
			
			action [0] = cardName;
			action [1] = opponent1.ID.ToString(); //so will target player whose next}
		}
		
		return action;
	}
	
	//then it'll play a random positive special
	public string[] doPhaseTwoEventTwo(){
		string[] action = new string[2];
		action [0] = "draw";
		action [1] = "0";

		string cardName;
		List<Card> specialPositives = getAllSpecialPositives ();
		if (specialPositives.Count > 0){
			int randomIndex = Random.Range (0, specialPositives.Count);
			cardName = specialPositives [randomIndex].name;
			//specialPositives.RemoveAt (randomIndex);
		
			action [0] = cardName;
			action [1] = "0";
		}

		return action;
	}

	//I'll have this bot teleport if have portal else play an attack
	public string[] doPhaseThree(){
		string[] action = new string[2];
		action [0] = "teleport";
		action [1] = "0";
		string cardName;
		List<Card> portals = getAllPortals ();
		if (portals.Count > 0) {
			return action; //no need to do the random thing since will return portal, even if 2 portals		
		} else {
			List<Card> attacks = getAllAttacks ();
			int randomIndex = Random.Range (0, attacks.Count);
			cardName = attacks [randomIndex].name;
			//attacks.RemoveAt (randomIndex);
			action[0] = cardName;
			action [1] = opponent1.ID.ToString(); //so will target player whose next
		}
		return action;
	}

	//responds with any random attack card (always returns something since always is virtual attack)
	public string doCounterAttack(int attackerID, int attackVal){
		string infomessage = "I (" + me.name + ") am being attacked by Player" + attackerID.ToString () + " who is attacking with a value of " + attackVal.ToString ();
		if (!GameLogic.fastSimulationMode){
		print (infomessage);
		}
		string cardName = "virtualattack";
		List<Card> attacks = getAllAttacks ();
		int randomIndex = Random.Range (0, attacks.Count);
		cardName = attacks [randomIndex].name;
		return cardName;
	}

	//really stupid bot. If winning space is availale, it moves there, else it just moves randomly to any space that's available.
	public string movePiece(string[] spaces){
		//string rtn = "M";
		List<string> reachableSpacesCopy = new List<string> (spaces);


		if (reachableSpacesCopy.Contains("F11") && me.ID == 1){return "F11";}
		if (reachableSpacesCopy.Contains("F21") && me.ID == 2){return "F21";}
		if (reachableSpacesCopy.Contains("F31") && me.ID == 3){return "F31";}

		int randomIndex = Random.Range (0, reachableSpacesCopy.Count);
		string rtn = reachableSpacesCopy [randomIndex];
		return rtn;
	}
	
	public override string ToString ()
	{
		return "trialBot";
	}
}
