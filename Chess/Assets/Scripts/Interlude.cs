﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interlude : MonoBehaviour {

	[SerializeField]
	private Text _waveCount;
	[SerializeField]
	private Text _waveStatus;
	private GameManager _gm;
	private Animator _animator;

	// Use this for initialization
	void Start () {
		_gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		if(_waveCount!=null){
			_waveCount.text = GameManager._Lvl+"/3";
		}
		if(_waveStatus!=null){
			_waveStatus.text = GameManager._Status;
		}
		_animator = GetComponent<Animator>();
	}

	// Action du bouton X
	public void Retour() {
		_animator.SetTrigger("isSliding");
		Invoke("RetourAuMenu",1);
	}

	// Retourne au menu
	public void RetourAuMenu() {
		_gm.ChangerScene("menu");
	}

	public void Credits() {
		_gm.ChangerScene("credits");
	}

	// Action du bouton CONTINUER
	public void Suivant() {
		_animator.SetTrigger("isSliding");
		Invoke("VagueSuivante",1);
	}

	// Retourne au jeu et change de niveau
	void VagueSuivante() {
		if(GameManager._StatusMessage[0] == GameManager._Status){
			GameManager._Lvl++;
		}
		if(GameManager._Lvl<=3){
			_gm.ChangerScene("main");
		} else {
			_gm.ChangerScene("end");
		}
	}
}