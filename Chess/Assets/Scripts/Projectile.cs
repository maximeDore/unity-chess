﻿/*
Objet lancé par les Pawn
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	private BoxCollider2D _collider;
	private SpriteRenderer _renderer;
	private AudioSource _audio;

	private float _speed = 12f;
	private float _delay = 1f;

	void Start() {
		_collider = GetComponent<BoxCollider2D>();
		_renderer = GetComponent<SpriteRenderer>();
		_audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(-1, -0.17f, 0) * _speed * Time.deltaTime);
		if(transform.position.x >= GameManager._CAMERASIZE.x){
			Destroy(gameObject,_delay);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Enemy"){
			_renderer.enabled = false;
			_collider.enabled = false;
			_audio.Play();
			other.GetComponent<Enemy>().Damage();
			Destroy(gameObject,_delay);
		}
	}
}
