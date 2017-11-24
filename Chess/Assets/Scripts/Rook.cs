using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : MonoBehaviour {

	private bool isTriggered;
	private Animator _animator;

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
		_animator.applyRootMotion = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.GetComponent<Collider>().tag == "Enemy"){
			// other.Kill();
			if(!isTriggered){
				_animator.applyRootMotion = false;
				isTriggered=true;
				_animator.SetTrigger("isTriggered");
			}
		}
	}
}
