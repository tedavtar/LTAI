using UnityEngine;
using System.Collections;

public class SpecialCard : Card {

	public Vector3 effect;
	public bool positive;

	//effect a Vector3 (IT,HT,CF)
	public SpecialCard(string name, Vector3 effect, bool positive){
		this.name = name;
		this.positive = positive;
		if (positive) {
			this.type = "SpecialPositive";		
		} else {
			this.type = "SpecialNegative";
		}
		this.effect = effect;
	}

	public void Play(int owner, int target){
		if (positive) {
			string ownerString = owner.ToString ();
			string playerName = "Player" + ownerString;
			Player whoPlayed = GameObject.Find(playerName).GetComponent<Player>();
			whoPlayed.IT += (int)effect.x;
			whoPlayed.HT += (int)effect.y;
			whoPlayed.CF += (int)effect.z;
			if (whoPlayed.IT > 10){
				whoPlayed.IT = 10;
			}
			if (whoPlayed.HT > 10){
				whoPlayed.HT = 10;
			}
			if (whoPlayed.CF > 10){
				whoPlayed.CF = 10;
			}
		} else {
			string targetString = target.ToString ();
			string targetName = "Player" + targetString;
			Player targeted = GameObject.Find(targetName).GetComponent<Player>();
			targeted.IT -= (int)effect.x;
			targeted.HT -= (int)effect.y;
			targeted.CF -= (int)effect.z;
			if (targeted.IT < 0){
				targeted.IT = 0;
			}
			if (targeted.HT < 0){
				targeted.HT = 0;
			}
			if (targeted.CF < 0){
				targeted.CF = 0;
			}
		}
	}

	public override string ToString(){
		string effectString = "";
		if (positive){
			if (effect.x != 0) {
				effectString += "+" + effect.x + " IT ";
			}
			if (effect.y != 0) {
				effectString += "+" + effect.y + " HT ";
			}
			if (effect.z != 0) {
				effectString += "+" + effect.z + " CF ";
			}
		} else {
			if (effect.x != 0) {
				effectString += "-" + effect.x + " IT ";
			}
			if (effect.y != 0) {
				effectString += "-" + effect.y + " HT ";
			}
			if (effect.z != 0) {
				effectString += "-" + effect.z + " CF ";
			}
		}
		
		string description = base.ToString ();
		description += "effect: " + effectString + "\n";
		return description;
	}
}
