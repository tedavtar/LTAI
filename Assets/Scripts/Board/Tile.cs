using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tile : MonoBehaviour {

	public string ID;

	public bool hasPiece = false;
	public bool isSelected = true;

	public GameObject tile1;
	public GameObject tile2;
	public GameObject tile3;



	public void setID(string i){
		ID = i;
	}

	public void select(){
		float r = 46 / 255.0f;
		float g = 48 / 255.0f;
		float b = 146 / 255.0f;
		Color selectedColor = new Color (r,g,b,1);
		isSelected = true;
		gameObject.GetComponent<Image> ().color = selectedColor;
	}

	public void addPiece(){
		hasPiece = true;
		gameObject.GetComponent<Image> ().color = Color.red;
	}

	public void reset(){
		float r = 0 / 255.0f;
		float g = 113 / 255.0f;
		float b = 188 / 255.0f;
		Color unSelectedColor = new Color (r,g,b,1);
		hasPiece = false;
		isSelected = false;
		gameObject.GetComponent<Image> ().color = unSelectedColor;
	}


	//connects two tiles bi-directionally  also make static since it's a method particular to the Tile class, not a given tile
	public static void connectTiles(GameObject tile1, GameObject tile2) {
		connectTileHelper(tile1, tile2);
		connectTileHelper(tile2, tile1);
	}
	
	//connects two tiles uni-directionally
	public static void connectTileHelper(GameObject tile1, GameObject tile2) {
		Tile tile1Script = tile1.GetComponent<Tile>();
		
		if (!tile1Script.tile1) {
			tile1Script.tile1 = tile2;
		} else {
			if (!tile1Script.tile2) {
				tile1Script.tile2 = tile2;
			} else {
				if (!tile1Script.tile3) {
					tile1Script.tile3 = tile2;
				}
			}
		}
	}


}
