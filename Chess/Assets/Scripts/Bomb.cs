using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Piece {

	private Animator _animator;
	private BoxCollider2D _collider;
	private TileManager _tm;
	private string _animationName = "isExploding";
	private bool _isUsed;

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
		_collider = GetComponent<BoxCollider2D>();
		_collider.enabled = false;
		_tm = transform.parent.GetComponent<TileManager>();
		Invoke("Explode",1);	
	}

	void Explode() {
		_collider.enabled = true;
		_animator.SetTrigger("isTriggered");
		Invoke("RemoveCollider",0.1f);
		Invoke("DestroyPiece",1);
	}

	void RemoveCollider() {
		_collider.enabled = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Enemy") {
			other.GetComponent<Enemy>().Kill(_animationName);
		}
	}

	void OnDestroy() {
		_tm._PieceOnTile = false;
	}
}
