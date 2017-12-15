/*
Classe qui gère l'utilisation des boutons dans l'interface
*/

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
	[SerializeField]
	private Button _muteBtn;	
	[SerializeField]
	private Sprite[] _muteBtnsSprite;

	// Fonction appelée au clic d'un des boutons de l'interface
	public void SelectButton(int value) {
		// Debug.Log("Bouton #"+(value+1));
		BoardManager._PieceIndex = value;
	}

	public void MuteButton() {
		if(!MusicManager._IsMuted){
			_muteBtn.GetComponent<Image>().sprite = _muteBtnsSprite[0];
		} else {
			_muteBtn.GetComponent<Image>().sprite = _muteBtnsSprite[1];
		}
	}

	void Update() {
		if(_bm!=null){
			if(GameManager._Play){	// Si la vague n'est pas commencée, les boutons sont désactivés
				money = GameManager._Money;
			} else {
				money = 0;
			}
			prices = _bm._piecesPrice;
			for(int i=0;i<_btnRefs.Length;i++){
				if(money<prices[i]){
					_btnRefs[i].interactable = false;
				} else {
					_btnRefs[i].interactable = true;
				}
			} // for
		} // if
	} // Update
}
