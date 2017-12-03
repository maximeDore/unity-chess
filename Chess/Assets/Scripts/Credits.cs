using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("Menu",15.5f);	// Retour au menu à la fin de l'animation
	}
	
	// Retourner au menu
	void Menu () {
		SceneManager.LoadSceneAsync("menu");
	}
}
