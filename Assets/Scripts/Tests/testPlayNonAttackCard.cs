using UnityEngine;
using System.Collections;


public class testPlayNonAttackCard : MonoBehaviour {

	public string description = "tests to ensure that Player class has methods to play non attack cards.  Also an example for what the Brain classes AI methods would do after doing the decision logic and then executing it--by means of calling it's relavant Player classes methods";

	// Use this for initialization
	void Start () {

		//just to see original hand
		foreach (Card c in GameObject.Find ("Player1").GetComponent<Player>().hand) {
			print (c);		
		}
		//testPlayPortal ();
		//testPlaySpecialPositive ();
		//testPlaySpecialNegative ();
	}

	//will test play only the first portal card--likely need to run many times since low rel frequency portals in deck. how auspicious--got it right first try
	void testPlayPortal(){
		Player p1 = GameObject.Find ("Player1").GetComponent<Player> ();
		for (int i=0; i<p1.hand.Length; i++) {
			if (p1.hand[i] != null && p1.hand[i].type.Equals("Portal")){
				print (i);
				print (p1.hand[i]);
				//p1.playPortalCard(i);
				//target is 0 since portal cards have no target, just name ("teleport") in this case
				p1.playNonAttackCard(p1.hand[i].name,0);

				return;
			}
		}

	}

	void testPlaySpecialPositive(){

		Player p1 = GameObject.Find ("Player1").GetComponent<Player> ();
		//List<Card> handcopy = new List<Card> (p1.hand);
		for (int i=0; i<p1.hand.Length; i++) {
			if (p1.hand[i] != null && p1.hand[i].type.Equals("SpecialPositive")){
				print (i);
				print (p1.hand[i]);
				//p1.playSpecialPositiveCard(i);
				p1.playNonAttackCard(p1.hand[i].name,0);
				
				//return; Want to make sure that can play multiple specialPos cards in hand and that no index problems due to removing from hand. actually, I think best to make hand copy for this test due to the for loop. What will happen in Player.doTurn is say Brain tells player to play 2 specials, than players will iterate thourgh handCOPY twice and play them
				break;
			}
		}
		//do this twice--thus make sure that can play multiple specialPos cards by name--proof that no index shift isses. won't be due to name but just to be super safe
		for (int i=0; i<p1.hand.Length; i++) {
			if (p1.hand[i] != null && p1.hand[i].type.Equals("SpecialPositive")){
				print (i);
				print (p1.hand[i]);
				//p1.playSpecialPositiveCard(i);

				
				//return; Want to make sure that can play multiple specialPos cards in hand and that no index problems due to removing from hand. actually, I think best to make hand copy for this test due to the for loop. What will happen in Player.doTurn is say Brain tells player to play 2 specials, than players will iterate thourgh handCOPY twice and play them
				return;
			}
		}
	}

	//test targets p3
	void testPlaySpecialNegative(){
		
		Player p1 = GameObject.Find ("Player1").GetComponent<Player> ();
		for (int i=0; i<p1.hand.Length; i++) {
			if (p1.hand[i] != null && p1.hand[i].type.Equals("SpecialNegative")){
				print (i);
				print (p1.hand[i]);
				//targets p3
				//p1.playSpecialNegativeCard(i,3);
				p1.playNonAttackCard(p1.hand[i].name,3);
				
				return;
			}
		}
	}


}
