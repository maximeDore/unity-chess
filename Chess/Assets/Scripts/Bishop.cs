using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece {

	private Transform _coinInstance;
	private bool _coinExists;
	public bool _CoinExists {
		get { return _coinExists; }
		set { _coinExists = value; }
	}

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
		StartCoroutine(SpawnCoin());
	}

	private IEnumerator SpawnCoin() {
		while(GameManager._Play){
			if(!_CoinExists){
				yield return new WaitForSeconds(10);
				Transform coin = Instantiate(moneyRef, transform.position, Quaternion.identity, transform);
				_CoinExists = true;
				if(transform.childCount>=1){
					// StartCoroutine(BishopCoinLife(coin));
				}
			}
			yield return null;
		}
	}

	private IEnumerator BishopCoinLife(Transform coin) {
		int coinTimer = 5;
		yield return new WaitForSeconds(coinTimer);
		if(coin!=null){
			Destroy(coin.gameObject);
			_CoinExists = false;
		}
	}
}
