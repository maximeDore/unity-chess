using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	private Bishop _bishop;

	// Use this for initialization
	void Start () {
		StartCoroutine(CoinLife());
		_bishop = transform.parent.GetComponent<Bishop>();
	}
	
	// Update is called once per frame
	private IEnumerator CoinLife() {
		int coinTimer = 5;
		yield return new WaitForSeconds(coinTimer);
		if(_bishop!=null){
			_bishop._CoinExists = false;
		}
		Destroy(gameObject);
	}

	//Appelée lorsqu'on clique sur l'objet
	void OnMouseEnter() {
		GameManager._Money += 25;
		Destroy(gameObject);
		if(_bishop!=null){
			_bishop._CoinExists = false;
		}
	}
}
