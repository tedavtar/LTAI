using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor {
	/*
	public override void OnInspectorGUI(){
		Player p = (Player)target;

		p.isMyTurn = EditorGUILayout.Toggle ("isMyTurn", p.isMyTurn);

	}*/
}
