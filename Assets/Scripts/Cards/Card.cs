using UnityEngine;
using System.Collections;

public class Card {

	public string name;
	public string type;

	public Card(){

	}

	public override string ToString(){
		string description = "name: " + name + "\ntype: " + type + "\n";
		return description;
	}
}
