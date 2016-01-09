using UnityEngine;
using System.Collections;

public class testCard1 : MonoBehaviour {

	public string description = "this tests the initialization and printing of an cards";

	//this tests printing out of an attack card
	void Start () {
		testAttackCard ();
		testSpecialPositiveCard ();
		testSpecialNegativeCard ();
		testPortalCard ();
	}
	
	void testAttackCard(){
		int[] attackVal = new int[4];
		attackVal [0] = 30;
		attackVal [1] = 1;
		attackVal [2] = 2;
		attackVal [3] = 3;
		Card a = new AttackCard ("TestAttack", attackVal);
		print(a);
	}

	void testSpecialPositiveCard(){
		Vector3 effect = new Vector3 (2, 3, 4);
		Card sp = new SpecialCard ("TestSpecialP", effect, true);
		print (sp);
	}

	void testSpecialNegativeCard(){
		Vector3 effect = new Vector3 (1, 2, 3);
		Card sp = new SpecialCard ("TestSpecialN", effect, false);
		print (sp);
	}

	void testPortalCard(){
		Card p = new PortalCard ("TestPortal");
		print (p);
	}
}
