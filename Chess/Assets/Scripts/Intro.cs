/*
Classe de la scène d'introduction
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("Jeu",5.5f);
	}
	
	void Jeu () {
		SceneManager.LoadSceneAsync("main");
	}
}
