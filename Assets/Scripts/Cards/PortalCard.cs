using UnityEngine;
using System.Collections;

public class PortalCard : Card {

	public PortalCard(string name){
		this.name = name;
		this.type = "Portal";
	}

	//the owner of the card-1 = player1
	public void Play(int owner){

		string ownerString = owner.ToString ();
		string portalID = "P" + ownerString;

		BoardActions.movePieceToTile (portalID);
	}
}
