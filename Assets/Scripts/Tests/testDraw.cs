using UnityEngine;
using System.Collections;

public class testDraw : MonoBehaviour {

	Player p1;

	// Use this for initialization
	void Start () {
		p1 = GameObject.Find("Player1").GetComponent<Player>();

		//testNextEmptySlot1 (); //success, 1 gets printed out. Virtual attack at 0 so next empty is 1
		//testDrawCard1 (); //success, ran a few times, deck size became 1 less and hand got 1 card which was not always the same across multiple runs so random card is being drawn
		//testDrawCardFull (); //success, printed out hand full message and only drew 5 cards. i changed 6 to 5 and again got full hand but no message
	
	}
	
	void testNextEmptySlot1(){

		print (p1.nextEmptySlot ());
	}

	void testDrawCard1(){
		p1.drawCard ();
	}

	void testDrawCardFull(){
		for (int i = 0; i<6;i++){
			p1.drawCard ();
		}
	}
}
