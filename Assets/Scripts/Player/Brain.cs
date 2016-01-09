using UnityEngine;
using System.Collections;

public interface Brain {
	
	//string arrays below. size 2. return ("nameofcardtoplay" or "draw", "numberoftarget" or "0")

	string[] doPhaseTwoEventOne();

	string[] doPhaseTwoEventTwo();

	string[] doPhaseThree();

	//so Attacking player's Player component's playAttack will call doCounterAttack() on target player's Brain implementing brain. This fuction will end with "return Player.playCounterAttackCard(indexOfCardInHand) which in turn does the normal play dispose of card from hand, but also returns an int that is the cards attack value 
	string doCounterAttack(int attackerID, int attackVal);


	//should return a tileID. ex. "M". then player can check is reachable given spaces and if so, move it there
	string movePiece(string[] spaces);

}
