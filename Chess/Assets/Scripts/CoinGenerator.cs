using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour {

	[SerializeField]
	private Transform _coinRef;
	private bool _isGenerating;

	// Use this for initialization
	void Update () {
		if(!_isGenerating){
			_isGenerating=!_isGenerating;
			StartCoroutine(GenerateCoins());
		}
	}
	
	// Génère des Coins à toutes les 15s
	private IEnumerator GenerateCoins() {
		while(true){
			while(GameManager._Play){
				Vector2 pos = new Vector2(Random.Range(-5,10),Random.Range(3.1f,-5.3f));
				yield return new WaitForSeconds(15f);
				Instantiate(_coinRef,pos,Quaternion.identity,transform);
			}
			yield return null;
		}
	}
}
