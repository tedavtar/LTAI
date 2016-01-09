using UnityEngine;
using System.Collections;

public class testPlayCard : MonoBehaviour {

	public string description = "This tests the card's play methods";

	void Start () {
		//testPlayPortal ();
		//testSpecialPositive ();
		//testSpecialNegative ();
	}
	

	void testPlayPortal () {
		Card p = new PortalCard ("TestPortal");

		PortalCard p1 = (PortalCard)p;
		//pretend that player1 is playing p1
		p1.Play (1);
	}

	void testSpecialPositive(){
		Vector3 effect = new Vector3 (1, 2, 4);
		Card sp = new SpecialCard ("TestSpecialP", effect, true);
		SpecialCard sp1 = (SpecialCard)sp;
		//the 2nd argument doesn't matter because it's a SpecialPositive and has no target
		sp1.Play (1, 0);
	}

	void testSpecialNegative(){
		Vector3 effect = new Vector3 (1, 2, 8);
		Card sp = new SpecialCard ("TestSpecialN", effect, false);
		SpecialCard sp1 = (SpecialCard)sp;
		//the 1st argument doesn't matter because it's a SpecialNegative. Player3 is target
		sp1.Play (1, 3);
	}
}
