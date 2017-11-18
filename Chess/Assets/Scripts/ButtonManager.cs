using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

	[SerializeField]
	private Button[] _btnRefs;

	public void SelectButton(int value) {
		Debug.Log("Bouton #"+(value+1));
		switch (value) {
			case 0: 
				BoardManager._PieceIndex = 0;
				break;
			case 1:
				BoardManager._PieceIndex = 1;
				break;
			case 2: 
				BoardManager._PieceIndex = 2;
				break;
			case 3:
				BoardManager._PieceIndex = 3;
				break;
			default:
				Debug.Log("Erreur...");
				break;
		}
	}
}
