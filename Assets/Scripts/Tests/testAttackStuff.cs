using UnityEngine;
using System.Collections;

public class testAttackStuff : MonoBehaviour {

	// Use this for initialization
	void Start () {
		foreach (Card c in GameObject.Find ("Player1").GetComponent<Player>().hand) {
			print (c);		
		}
		//testGetAttackCardValue (); //well got 34 for flowers, 30 curveset, and 21 virtual attack, so confident it works
	}
	

	void testGetAttackCardValue(){

		Player p1 = GameObject.Find ("Player1").GetComponent<Player> ();
		for (int i=0; i<p1.hand.Length; i++) {
			if (p1.hand[i] != null && p1.hand[i].type.Equals("Attack")){
				print (i);
				print (p1.hand[i]);
				AttackCard attackCard = (AttackCard)p1.hand[i];
				int attackVal = p1.getAttackValue(attackCard.attackVal);
				print (attackVal);
				
				//return;
			}
		}

	}


}
