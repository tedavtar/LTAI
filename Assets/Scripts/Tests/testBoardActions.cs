using UnityEngine;
using System.Collections;

public class testBoardActions : MonoBehaviour {

	 
	// Use this for initialization
	void Start () {
		//testReachable ();
		//testMovePieceToTile ();
		testReachable1 ();
	}

	void testReachable(){
		BoardActions.addPieceToTile ("P1");
		BoardActions.selectReachablePieces ("P1", 20);
		//foreach (string tileID in GameLogic.reachableTiles) {
		//	print (tileID);		
		//}
	}

	void testMovePieceToTile(){
		//BoardActions.addPieceToTile ("P1");
		//BoardActions.selectReachablePieces ("P1", 4);
		//print (GameLogic.reachableTiles.Count);
		print (GameLogic.tileWithPiece);
		BoardActions.selectReachablePieces ("M", 4);

		print (GameLogic.reachableTiles.Count);
		BoardActions.movePieceToTile ("P3");
		print (GameLogic.tileWithPiece);
		//print (GameLogic.reachableTiles.Count);
	}

	//NIIIIIICE, works so much faster than testReachable using brute force!!
	void testReachable1(){
		BoardActions.addPieceToTile ("P1");
		BoardActions.selectReachablePieces1("P1", 20);
	}



}
