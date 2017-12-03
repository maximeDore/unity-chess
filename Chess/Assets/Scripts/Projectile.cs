using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	private float _speed = 12f;
	private float _delay = 0.1f;
	private AudioSource _sfx;

	// Use this for initialization
	void Start () {
		_sfx = GetComponent<AudioSource>();
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
			_sfx.Play();
			other.GetComponent<Enemy>().Damage();
			Destroy(gameObject,_delay);
		}
	}
}
