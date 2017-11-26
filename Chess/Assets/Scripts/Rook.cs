using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : MonoBehaviour {

	private bool isTriggered;
	private float _speed = 5f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator IsTriggered() {
		while(true){
			isTriggered=true;
			transform.Translate(Vector3.right * _speed * Time.deltaTime);
			if(transform.position.x > GameManager._CAMERASIZE.x){
				DestroyRook();
			}
			yield return null;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Enemy"){
			other.GetComponent<Enemy>().Kill();
			StartCoroutine(IsTriggered());
			IsTriggered();
		}
	}
	
	void DestroyRook() {
		Destroy(gameObject);
	}
}
