﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Appelée lorsqu'on clique sur l'objet
	void OnMouseEnter() {
		GameManager._Money += 25;
		Destroy(gameObject);
		transform.parent.GetComponent<Bishop>()._CoinExists = false;
	}
}
