using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

	[SerializeField]
	private Button[] _btnRefs;

	public void SelectButton(int value) {
		Debug.Log("Bouton #"+(value+1));
		BoardManager._PieceIndex = value;
	}
}
