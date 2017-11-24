using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	private float _speed = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.right * _speed * Time.deltaTime);
		if(transform.position.x >= 15){	// ////////////////////////////////////////////////////HARDCODE\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ \\
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Enemy"){
			// other.GetComponent<Enemy>().Damage();
		}
	}
}
