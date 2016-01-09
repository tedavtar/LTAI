using UnityEngine;
using System.Collections;

public class AttackCard : Card {

	//attackVal is an int[4] in form (Constant,IT,HT,CF)
	public int[] attackVal;

	public AttackCard(string name, int[] attackVal){
		this.name = name;
		this.type = "Attack";
		this.attackVal = attackVal;
	}
	    
	public override string ToString(){
		string attackVals = "";
		if (attackVal [0] != 0) {
			attackVals += attackVal[0].ToString(); 		
		}
		if (attackVal [1] != 0) {
			attackVals += " + " + attackVal[1].ToString() + "*IT"; 		
		}
		if (attackVal [2] != 0) {
			attackVals += " + " + attackVal[2].ToString() + "*HT"; 		
		}
		if (attackVal [3] != 0) {
			attackVals += " + " + attackVal[3].ToString() + "*CF"; 		
		}


		string description = base.ToString ();
		description += "attackVal: " + attackVals + "\n";
		return description;
	}

}
