/*
Objet à usage unique qui écrase un ennemi et se supprime
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : Piece {

	private TileManager _tm;
	private string _animationName = "isSplatted";
	private bool _isUsed;
	private Enemy _enemy;

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
		_tm = transform.parent.GetComponent<TileManager>();
	}

	void Attack() {
		if(!_isUsed){
			_enemy.Kill(_animationName);
			_animator.SetTrigger("isTriggered");
			_isUsed = true;
			Invoke("DestroyPiece",1);
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		float deltaX = other.transform.position.x - transform.position.x;
		if(other.tag == "Enemy") {
			if(deltaX <= 0.1f && deltaX >= -0.1f){
				_enemy = other.GetComponent<Enemy>();
				Attack();
			}
		}
	}

	void OnDestroy() {
		_tm._PieceOnTile = false;
	}
}
