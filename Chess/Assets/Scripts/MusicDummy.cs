using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDummy : MonoBehaviour {

	private MusicManager _mm;

	// Use this for initialization
	void Start() {
		_mm = GameObject.Find("MusicManager").GetComponent<MusicManager>();
	}
	
	// Appeler la fonction Mute() du MusicManager
	public void CallMute() {
		_mm.Mute();
	}
}
