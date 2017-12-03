using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("Menu",15.5f);
	}
	
	void Menu () {
		SceneManager.LoadSceneAsync("menu");
	}
}
