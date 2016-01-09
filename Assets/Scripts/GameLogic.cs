using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour {

	public static bool fastSimulationMode;
	public int numSimulations;

	public static bool gameNotOver;

	public static bool p1outofcards;
	public static bool p2outofcards;
	public static bool p3outofcards;

	public bool p1outcardDisplay;
	public bool p2outcardDisplay;
	public bool p3outcardDisplay;



	Player p1;
	Player p2;
	Player p3;

	public static string playerWhoseTurnItIs;

	public static Player[] players;

	public float turnTime = 0f;

	float actualTurnTime = 1.5f;

	public string tileWithPieceDisplay;

	public static string tileWithPiece = "M";

	public static HashSet<string> reachableTiles;

	public static RecordBook myRecordBook;

	void Awake(){
		signalPlayersStillHaveCardsLeft ();
		fastSimulationMode = true;
		numSimulations = 70;
		if (!fastSimulationMode){//so slow
			turnTime = actualTurnTime;
		}
		players = new Player[3];
		reachableTiles = new HashSet<string> ();

		myRecordBook = new RecordBook (numSimulations);
	}
	

	// Use this for initialization
	void Start () {
		StartCoroutine (runSimulations ());
	}

	IEnumerator runSimulations(){
		gameNotOver = true;
		//BoardActions.addPieceToTile ("M"); instead do this in boardactions no instead loadboard since now board set up, avoid null pe
		p1 = GameObject.Find ("Player1").GetComponent<Player> ();
		p2 = GameObject.Find ("Player2").GetComponent<Player> ();
		p3 = GameObject.Find ("Player3").GetComponent<Player> ();

		float startTime = Time.time;
		
		for (int i = 0; i<numSimulations;i++){
			yield return StartCoroutine (playGame ());
		}

		float endTime = Time.time;
		float timeSimulationsTook = endTime - startTime;
		print (timeSimulationsTook.ToString ());
		print (myRecordBook);
	}
	
	// Update is called once per frame
	void Update () {
		tileWithPieceDisplay = tileWithPiece;
		
		p1outcardDisplay = p1outofcards;
		p2outcardDisplay = p2outofcards;
		p3outcardDisplay = p3outofcards;

	}

	void signalPlayersStillHaveCardsLeft(){
		p1outofcards = false;
		p2outofcards = false;
		p3outofcards = false;
	}

	IEnumerator playGame(){
		

		players[0] = p1;
		players[1] = p2;
		players[2] = p3;
		while(gameNotOver){
			foreach (Player player in players) {
				if(gameNotOver){
					playerWhoseTurnItIs = player.name;
					float startTime = Time.time;
					yield return StartCoroutine(player.doTurn());
					float endTime = Time.time;
					float timeTurnTook = endTime - startTime;
					if (timeTurnTook < turnTime){
						yield return new WaitForSeconds(turnTime - timeTurnTook);
					}
					float finalEndTime = Time.time;
					//print (finalEndTime - startTime);
					//yield return null;
				}
			}
		}
		if (!fastSimulationMode){
		print ("game over");
		}
		StartCoroutine (restoreGameState ());
		yield return null;	
	}

	IEnumerator restoreGameState(){
		if (!fastSimulationMode){
		print ("will restore game state to beginning");
		}
		foreach (Player player in players) {
			player.initStanding();
			player.initDeck();
			player.initHand();
		}
		BoardActions.movePieceToTile ("M");
		signalPlayersStillHaveCardsLeft ();
		gameNotOver = true;
		yield return null;
	}



}
