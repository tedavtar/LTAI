using UnityEngine;
using System.Collections;

public class RecordBook{
	int totalGames;
	int gamesP1won;
	int gamesP2won;
	int gamesP3won;
	public RecordBook(int totalGames){
		this.totalGames = totalGames;
		gamesP1won = 0;
		gamesP2won = 0;
		gamesP3won = 0;
	}

	public void reportVictory(int victorID){
		switch (victorID) {
		case 1:
			gamesP1won += 1;
			break;
		case 2:
			gamesP2won += 1;
			break;
		default:
			gamesP3won += 1;
			break;
		}
	}

	public override string ToString(){
		string summary = "";
		int gamesDrawn = totalGames - gamesP1won - gamesP2won - gamesP3won;
		float oneHundred = 100f;
		float percentP1 = oneHundred * (gamesP1won / (float)totalGames);
		float percentP2 = oneHundred * (gamesP2won / (float)totalGames);
		float percentP3 = oneHundred * (gamesP3won / (float)totalGames);
		float percentDrawn = oneHundred * (gamesDrawn / (float)totalGames);

		summary += "Player1 won " + percentP1.ToString() + "% of the time\n" ;
		summary += "Player2 won " + percentP2.ToString() + "% of the time\n" ;
		summary += "Player3 won " + percentP3.ToString() + "% of the time\n" ;
		summary += "Players drew " + percentDrawn.ToString() + "% of the time\n" ;
		return summary;
	}
}
