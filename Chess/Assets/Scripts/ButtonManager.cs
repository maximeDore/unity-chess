using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

	private int money;
	[SerializeField]
	private BoardManager _bm;
	private int[] prices;

	[SerializeField]
	private Button[] _btnRefs;

	// Fonction appelée au clic d'un des boutons de l'interface
	public void SelectButton(int value) {
		// Debug.Log("Bouton #"+(value+1));
		BoardManager._PieceIndex = value;
	}

	void Update() {
		money = GameManager._Money;
		prices = _bm._piecesPrice;
		for(int i=0;i<_btnRefs.Length;i++){
			if(money<prices[i]){
				_btnRefs[i].interactable = false;
			} else {
				_btnRefs[i].interactable = true;
			}
		}
	}
}
