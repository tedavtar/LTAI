using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardActions : MonoBehaviour {

	 //move this to gamelogic since want to see and access across all players
	//public static HashSet<string> reachableTiles;

	void Awake(){
		//reachableTiles = new HashSet<string> ();

		//removePieceFromTile("M");
		//selectTile ("M");
		//selectReachablePieces ("M", 2);
	
	}

	public static void movePieceToTile(string id){

		if (!GameLogic.fastSimulationMode){
			//clear current tile with piece
			resetTile (GameLogic.tileWithPiece);
			
			foreach (string tileID in GameLogic.reachableTiles) {
				//clears all selected tiles
				resetTile(tileID);
			}
		}
		//clears reachableTiles
		GameLogic.reachableTiles = new HashSet<string> ();

		addPieceToTile (id);

		//do a check for win
		switch (id) {
		case "F11":
			print ("Player1 wins");
			GameLogic.myRecordBook.reportVictory(1);
			GameLogic.gameNotOver = false;
			break;
		case "F21":
			print ("Player2 wins");
			GameLogic.myRecordBook.reportVictory(2);
			GameLogic.gameNotOver = false;
			break;
		case "F31":
			print ("Player3 wins");
			GameLogic.myRecordBook.reportVictory(3);
			GameLogic.gameNotOver = false;
			break;
		default:
			break;
		}
	}

	public static void addPieceToTile(string id){
		GameLogic.tileWithPiece = id;
		if (!GameLogic.fastSimulationMode){
			GameObject targetTile = GameObject.Find (id);
			targetTile.GetComponent<Tile> ().addPiece ();
		}
	}

	public static void resetTile(string id){
		GameObject targetTile = GameObject.Find (id);
		targetTile.GetComponent<Tile> ().reset ();
	}

	public static void selectTile(string id){
		GameObject targetTile = GameObject.Find (id);
		targetTile.GetComponent<Tile> ().select();
	}



	//kk so I'm going to change the premise and make it MOVE UP TO how many pieces you can move! so just need to change the bool visitNode()
	public static void selectReachablePieces1(string id, int movesToDo){
		GameLogic.reachableTiles = new HashSet<string> ();

		List<object[]> visitedNodes = new List<object[]>();
		Queue<object[]> fringe = new Queue<object[]> ();

		int moves = movesToDo;
		//bool stopAddingToFringe = false;

		Tile t = GameObject.Find (id).GetComponent<Tile> ();
		object[] rootNode = makeNode (t, moves);

		visitedNodes.Add (rootNode);
		fringe.Enqueue (rootNode);

		while (true) { //while(!stopadd...)
			object[] testNode = fringe.Dequeue();
			int testMoves = (int)testNode[1];
			if (testMoves < 0){break;}
			Tile testTile = (Tile)testNode[0];

			moves = testMoves - 1;

			List<Tile> neighborTiles = new List<Tile>();
			if (testTile.tile1 != null){
				neighborTiles.Add(testTile.tile1.GetComponent<Tile>());
			}
			if (testTile.tile2 != null){
				neighborTiles.Add(testTile.tile2.GetComponent<Tile>());
			}
			if (testTile.tile3 != null){
				neighborTiles.Add(testTile.tile3.GetComponent<Tile>());
			}
			foreach(Tile neighborTile in neighborTiles){
				string testID = neighborTile.ID;
				bool shouldIVisit = visitNode(testID,moves,visitedNodes);
				if (shouldIVisit){
					object[] childNode = makeNode(neighborTile,moves);
					visitedNodes.Add (childNode);
					fringe.Enqueue (childNode);
				}
			}

		}

		foreach (object[] node in visitedNodes) {
			Tile nodeTile = (Tile)node[0];
			string nodeID = nodeTile.ID;
			int nodeMoves = (int)node[1];
			//print ("Node: Tile: " + nodeID + " moves: " + nodeMoves.ToString());

				
				
			if (!nodeID.Equals(id) && nodeMoves == 0){ //so only allow to move to DIFFERENT spot
					if (!GameLogic.fastSimulationMode){
						selectTile(nodeID);
					}	
					GameLogic.reachableTiles.Add(nodeID);
					
			}
			
		}

	}

	public static bool visitNode(string testID, int moves, List<object[]> visitedNodes){
		bool rtn = true;
		foreach(object[] node in visitedNodes){
			Tile nodeTile = (Tile)node[0];
			string nodeID = nodeTile.ID;
			int nodeMoves = (int)node[1];

			if(testID.Equals(nodeID)){
				/*
				int difference = nodeMoves - moves;
				if ((difference % 2) == 0){
					rtn = false;
				}*/
				if(nodeMoves == moves){
					return false;
				}

			}
		}
		return rtn;
	}

	public static object[] makeNode(Tile t, int moves){
		object[] rtnNode = new object[2];
		rtnNode [0] = t;
		rtnNode [1] = moves;
		return rtnNode;
	}
















	public static void selectReachablePieces(string id, int moves){
		GameLogic.reachableTiles = new HashSet<string> ();

		List<string> reachablePieces = new List<string>();
		Tile targetTileScript = GameObject.Find (id).GetComponent<Tile> ();
		reachableHelper (reachablePieces, targetTileScript, moves); //should fill reachablePieces with IDs of them
		foreach(string reachableID in reachablePieces){

			//print (reachableID); so what's happening is that many copies of id meaning can reach the same piece from not limited to 1 route--so bad since we're reaching the same tile many times from different paths
			if (!reachableID.Equals(id)){ //so only allow to move to DIFFERENT spot
				selectTile(reachableID);
				GameLogic.reachableTiles.Add(reachableID);

			}
		}
	}

	static void reachableHelper(List<string> toAddTo, Tile currentTile, int moves){
		if (moves == 0) {
			toAddTo.Add(currentTile.ID);
		} else {
			if (currentTile.tile1 != null){
				reachableHelper(toAddTo,currentTile.tile1.GetComponent<Tile>(),moves - 1);
			}
			if (currentTile.tile2 != null){
				reachableHelper(toAddTo,currentTile.tile2.GetComponent<Tile>(),moves - 1);
			}
			if (currentTile.tile3 != null){
				reachableHelper(toAddTo,currentTile.tile3.GetComponent<Tile>(),moves - 1);
			}
		}
	}
}
