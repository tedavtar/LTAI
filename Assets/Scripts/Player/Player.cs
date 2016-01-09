using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public float selectedPause = 0f;

	float actualSelectedPause = 1f;

	public int IT;
	public int HT;
	public int CF;

	public int ID;
	public string name;

	//public bool isMyTurn;

	public string[] deckDisplay;

	public List<Card> deck;

	public string[] handDisplay;

	public Card[] hand;


	public Brain myBrain;

	// changed from start to awake since want to have this done very first
	void Awake () {

		selectedPause = actualSelectedPause;

		initStanding ();
		loadID ();

		initDeck ();
		
		initHand ();

		myBrain = gameObject.GetComponents (typeof(Brain))[0] as Brain;
		//print (myBrain);
	}

	void Start(){
		if (GameLogic.fastSimulationMode){
			selectedPause = 0f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//rather than extend editor to display deck
		deckDisplay = deckAsStringArray ();
		handDisplay = handAsStringArray ();
	}





	public void initStanding (){
		IT = CF = HT = 7;
	}

	//sets up hand, loads the virtual attack card, draws 4 cards
	public void initHand(){
		hand = new Card[6];

		int[] attackVal = new int[4];
		attackVal [0] = 0;
		attackVal [1] = 1;
		attackVal [2] = 1;
		attackVal [3] = 1;
		Card virtualattack = new AttackCard ("virtualattack", attackVal);
		hand[0] = virtualattack;

		for (int i=0; i<4; i++) {
			drawCard();
		}
	}

	public void initTestShorterDeck(){
		deck = new List<Card> ();
		//here's just a starter deck--nothing special or significant about my choices, basically from all the previous art I've done/some intuition judgement--just to be consistent with the game
		
		//add 3 portals
		for (int i=0;i<3;i++){
			Card p = new PortalCard ("teleport");
			deck.Add (p);
		}
	}

	//loads some cards in deck
	public void initDeck(){



		deck = new List<Card> ();
		//here's just a starter deck--nothing special or significant about my choices, basically from all the previous art I've done/some intuition judgement--just to be consistent with the game

		//add 3 portals
		for (int i=0;i<3;i++){
			Card p = new PortalCard ("teleport");
			deck.Add (p);
		}

		//add attack cards (except virtual attack) and hidden passion (I believe it's too powerful/too much time to compute result of attack)
		int[] attackVal = new int[4];

		attackVal [0] = 30;
		attackVal [1] = 0;
		attackVal [2] = 0;
		attackVal [3] = 0;

		Card curveset = new AttackCard ("curveset", attackVal);
		deck.Add (curveset);
		//since no hidden passion and flowers is also rather too powerful, I'll add 2 more curvesets and just 1 more flowers
		deck.Add (curveset);
		deck.Add (curveset);

		/* weird this is causing curveset to have flowers' attack val that is rather than 30, 20+HT+CF
		attackVal [0] = 20;
		attackVal [1] = 0;
		attackVal [2] = 1;
		attackVal [3] = 1;
		*/
		int[] attackVal2 = new int[4];
		attackVal2 [0] = 20;
		attackVal2 [1] = 0;
		attackVal2 [2] = 1;
		attackVal2 [3] = 1;
		Card flowers = new AttackCard ("flowers", attackVal2);
		deck.Add (flowers);


		//add specials (2 sets)
		Vector3 effect = new Vector3 (0, 0, 0);
		for (int i=0;i<2;i++){
			effect = new Vector3(5,0,0);
			Card adderall = new SpecialCard ("adderall", effect, true);
			deck.Add (adderall);

			effect = new Vector3(5,0,0);
			Card caffeinepill = new SpecialCard ("caffeinepill", effect, true);
			deck.Add (caffeinepill);

			effect = new Vector3(5,0,0);
			Card drunk = new SpecialCard ("drunk", effect, false);
			deck.Add (drunk);

			effect = new Vector3(0,0,3);
			Card hairdo = new SpecialCard ("hairdo", effect, true);
			deck.Add (hairdo);

			effect = new Vector3(0,0,5);
			Card intimidate = new SpecialCard ("intimidate", effect, false);
			deck.Add (intimidate);

			effect = new Vector3(0,5,0);
			Card steroids = new SpecialCard ("steroids", effect, true);
			deck.Add (steroids);

			effect = new Vector3(0,5,0);
			Card trashtalk = new SpecialCard ("trashtalk", effect, false);
			deck.Add (trashtalk);

			effect = new Vector3(0,0,10);
			Card wallflowerbloom = new SpecialCard ("wallflowerbloom", effect, true);
			deck.Add (wallflowerbloom);

			effect = new Vector3(0,3,3);
			Card workout = new SpecialCard ("workout", effect, true);
			deck.Add (workout);


		}

	}



	public void drawCard(){
		int deckLength = deck.Count;
		int whereToAdd = nextEmptySlot();
		if (deckLength > 0){
			if (whereToAdd >= 0){
				int index = Random.Range(0,deckLength);
				Card cardDrawn = deck[index];
				deck.RemoveAt(index);
				hand[whereToAdd] = cardDrawn;
			} else {
				if (!GameLogic.fastSimulationMode){
				print ("Hand is full!");
				}
			}
		} else {
			if (!GameLogic.fastSimulationMode){
			print ("Out of Cards!");
			}
		}

		if (deck.Count == 0) {
			switch(ID){
			case 1:
				GameLogic.p1outofcards = true;
				if (!GameLogic.fastSimulationMode){
				print ("p1 out of cards");
				}
				if (GameLogic.p1outofcards && GameLogic.p2outofcards && GameLogic.p3outofcards){
					//print ("reached");
					GameLogic.gameNotOver = false;
					
				}
				break;
			case 2:
				GameLogic.p2outofcards = true;
				if (!GameLogic.fastSimulationMode){
				print ("p2 out of cards");
				}
				if (GameLogic.p1outofcards && GameLogic.p2outofcards && GameLogic.p3outofcards){
					//print ("reached");
					GameLogic.gameNotOver = false;
					
				}
				break;
			default:

				GameLogic.p3outofcards = true;
				if (!GameLogic.fastSimulationMode){
				print ("p3 out of cards");
				}
				if (GameLogic.p1outofcards && GameLogic.p2outofcards && GameLogic.p3outofcards){
					//print ("reached");
					GameLogic.gameNotOver = false;
					
				}
				break;


			}


		}

	}

	//This will return the index of the next empty slot or -1 if all slots are full
	public int nextEmptySlot(){
		for (int i=0; i<6; i++) {
			if (hand[i] == null){
				return i;
			}	
		}
		return -1;
	}

	//used in Update to show hand in inspector
	string[] handAsStringArray(){
		string[] rtn = new string[6];
		for(int i=0; i<6;i++) {
			if (hand[i] == null){
				break;
			}
			rtn[i] = hand[i].ToString();
		}
		
		return rtn;
	}

	//used in Update to show deck in inspector
	string[] deckAsStringArray(){
		string[] rtn = new string[deck.Count];
		for(int i=0; i<deck.Count;i++) {
			rtn[i] = deck[i].ToString();
		}

		return rtn;
	}

	void printDeck(){
		print (gameObject.name + "'s deck");
		foreach (Card c in deck) {
			print(c);		
		}
		print ("\n");
	}

	void loadID(){
		name = gameObject.name;
		switch (gameObject.name) {
		case "Player1":
			ID = 1;
			break;
		case "Player2":
			ID = 2;
			break;
		default:
			ID = 3;
			break;
		}
	}

	//play's the nont attack card by name. the problem is that the other play methods are by index not name, and I can see problem with index changing after removal of subsequent cards, so end up playing wrong card-but playing card by name is always fine (of course as long as card with name is in hand). however, can still make use of these methods. got to iterate through hand till reach ANY card with name. then call the playatindex methods based on type of card
	public void playNonAttackCard(string name, int target){
		for (int i=0; i<hand.Length; i++) {
			if (hand[i] != null){
				Card cardToPlay = hand[i];
				if (cardToPlay.name.Equals(name)){
					switch(cardToPlay.type){
					case "Portal":
						playPortalCard(i);
						break;
					case "SpecialPositive":
						playSpecialPositiveCard(i);
						break;
					default:
						playSpecialNegativeCard(i,target);
						break;
					}
					return; //since without this we might play card with same name twice
				}
			}
		}
	}

	//plays a portal card at index of hand. do the effect then remove the card from the hand.
	public void playPortalCard(int index){



		Card cardToPlay = hand [index];
		PortalCard p = (PortalCard)cardToPlay;
		p.Play (ID);
		//so now played the cards. what's left is to remove it from hand

		//print ("before " + nextEmptySlot());

		removeCardFromHand (index);


		//print ("after " + nextEmptySlot());
	}

	//litterally exact same is playPortalCard in that bam, just use the card's play effect using ID
	public void playSpecialPositiveCard(int index){
		Card cardToPlay = hand [index];
		SpecialCard s = (SpecialCard)cardToPlay;
		//since we're assuming that Brain would call this with index of SpecialPositive, can put 0 as target since no target
		s.Play (ID, 0);
		removeCardFromHand (index);
	}

	public void playSpecialNegativeCard(int index, int target){
		Card cardToPlay = hand [index];
		SpecialCard s = (SpecialCard)cardToPlay;
		//1st argument doesn't matter, since a SpecialNegative is being played, only the target matters
		s.Play (0, target);
		removeCardFromHand (index);
	}

	public void removeCardFromHand(int index){
		List<Card> handcopy = new List<Card> (hand);
		handcopy.RemoveAt (index);
		hand = new Card[6];
		//hand = handcopy.ToArray (); this as actually shrinking hand's size.
		for (int i=0; i<5;i++) {
			hand[i] = handcopy[i];	
		}
		hand [5] = null;
	}

	public IEnumerator startAttack(string name, int target){ //public void startAttack but I'll make it a coroutine so that I can waitforseconds in it so observer can see the selected pieces
		for (int i=0; i<hand.Length; i++) {
			if (hand[i] != null){
				Card cardToPlay = hand[i];
				if (cardToPlay.name.Equals(name)){
					AttackCard a = (AttackCard)cardToPlay;
					int attackVal = getAttackValue(a.attackVal);
					if (!GameLogic.fastSimulationMode){
					print(attackVal.ToString());
					}
					playAttack(i);
					//as of now should get rid of card if it's not VirtAttk and printed attackValue, so we have what we need to start a counterattack-the ID above of me the attacker, attackVal i'm attacking with, and my target
					string myTarget = "Player" + target.ToString();
					GameObject targetGameObject = GameObject.Find(myTarget);
					Brain myTargetBrain = targetGameObject.GetComponents (typeof(Brain))[0] as Brain;;
					string counterAttackCardName = myTargetBrain.doCounterAttack(ID,attackVal);
					if (!GameLogic.fastSimulationMode){
					print ("counterattacking cardname: " + counterAttackCardName);
					}
					int counterAttackVal = targetGameObject.GetComponent<Player>().playCounterAttack(counterAttackCardName);
					if (!GameLogic.fastSimulationMode){
					print ("counterattacking cardVal: " + counterAttackVal.ToString());
					}

					int difference = attackVal - counterAttackVal;

					//case where I get to move
					if (difference > 0){
						BoardActions.selectReachablePieces1(GameLogic.tileWithPiece,difference);

						yield return new WaitForSeconds(selectedPause);

						string[] reachableTiles = new string[GameLogic.reachableTiles.Count];
						GameLogic.reachableTiles.CopyTo(reachableTiles);
						//foreach (string id in reachableTiles){print (id);}
						string destinationTileID = myBrain.movePiece(reachableTiles);
						//good time to do a check to make sure that this destinationTileID is in fact within reachableTiles -- else the AI is trying to cheat
						BoardActions.movePieceToTile(destinationTileID);
					}
					//case where target player beat me so gets to move
					if (difference < 0){
						difference = -1 * difference;
						BoardActions.selectReachablePieces1(GameLogic.tileWithPiece,difference);

						yield return new WaitForSeconds(selectedPause);

						string[] reachableTiles = new string[GameLogic.reachableTiles.Count];
						GameLogic.reachableTiles.CopyTo(reachableTiles);
						//foreach (string id in reachableTiles){print (id);}
						string destinationTileID = myTargetBrain.movePiece(reachableTiles);
						BoardActions.movePieceToTile(destinationTileID);
					}



					if (difference == 0){
						if (!GameLogic.fastSimulationMode){
						print ("attacks canceled");
						}
					}

					yield return null; //return;
				}
			}
		}
	}

	//just removes the card (except for virtualattack)
	public void playAttack(int index){
		Card cardToPlay = hand [index];
		if (cardToPlay.name != "virtualattack") {
			removeCardFromHand (index);
		}
	}

	public int playCounterAttack(string name){
		int attackVal = 0;
		for (int i=0; i<hand.Length; i++) {
			if (hand[i] != null){
				Card cardToPlay = hand[i];
				if (cardToPlay.name.Equals(name)){
					AttackCard a = (AttackCard)cardToPlay;
					attackVal = getAttackValue(a.attackVal);
					playAttack(i);
					return attackVal;
				}
			}
		}
		return attackVal;
	}
	/*
	 * create playCounterAttack and in that call playAttack
	//does counterattack by playing an attack card at index. Returns the int of the attack value of the card played
	public int playCounterAttack(int index){
		Card cardToPlay = hand [index];
		return 0;
	}*/

	//takes in the int[4] array of an attack card and based on IT,HT,CF vals computes the attackVal. a helper for play(Counter)Attack and Brain-implementing AI class would use this to determine attackVals of attack cards in hand. 
	public int getAttackValue(int[] attackVal){
		return attackVal[0] + IT*attackVal[1] + HT*attackVal[2] + CF*attackVal[3];
	}

	public IEnumerator doTurn(){
		//isMyTurn = true;
		//print (GameLogic.playerWhoseTurnItIs);
		if (!GameLogic.fastSimulationMode){
		print (name + " started his turn");

		print (name + " will attempt to draw a card");
		}
		drawCard();

		string[] phase2actions1 = myBrain.doPhaseTwoEventOne ();
		//print (phase2actions1[0]); //should be either "draw" or "nameofcardtoplay"
		if (phase2actions1[0].Equals("draw")){
			if (!GameLogic.fastSimulationMode){
			print (name + " will attempt to draw a card");
			}
			drawCard();

		} else {
			if (!GameLogic.fastSimulationMode){
			print (name + " will play " + phase2actions1[0]);
			}
			playNonAttackCard(phase2actions1[0],int.Parse(phase2actions1[1]));
		}

		string[] phase2actions2 = myBrain.doPhaseTwoEventTwo ();
		//print (phase2actions1[0]); //should be either "draw" or "nameofcardtoplay"
		if (phase2actions2[0].Equals("draw")){
			if (!GameLogic.fastSimulationMode){
			print (name + " will attempt to draw a card");
			}
			drawCard();
		} else {
			if (!GameLogic.fastSimulationMode){
			print (name + " will play " + phase2actions2[0]);
			}
			playNonAttackCard(phase2actions2[0],int.Parse(phase2actions2[1]));
		}


		string[] phase3action = myBrain.doPhaseThree ();
		if (phase3action[0].Equals("teleport")){
			if (!GameLogic.fastSimulationMode){
			print (name + " will teleport");
			}
			playNonAttackCard(phase3action[0],int.Parse(phase3action[1]));
		} else{
			if (!GameLogic.fastSimulationMode){
			print (name + " will play " + phase3action[0] + "and attack Player" + phase3action[1] );
			}
			//startAttack(phase3action[0],int.Parse(phase3action[1]));
			yield return StartCoroutine( startAttack(phase3action[0],int.Parse(phase3action[1]))      );
		}

		//yield return new WaitForSeconds(2);
		//isMyTurn = false;
		if (!GameLogic.fastSimulationMode){
		print (name + " finished his turn");
		}
		yield return null;
	}



}
