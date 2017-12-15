/*
Objet d'attaque qui lance des projectiles
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece {

	[SerializeField]
	private LayerMask _lm;
	private bool _isAttacking = false;

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Infinity, _lm);
		if (hit.collider != null && hit.collider.tag == "Enemy") {
			// Debug.Log("hit : "+hit.collider.name);
			if(_isAttacking==false){
				_isAttacking=true;
				_animator.SetTrigger("isAttacking");
				Invoke("PawnThrow", 0.5f);
			}
		}
	}

	void PawnThrow() {
		Vector2 posThrow = new Vector2(transform.position.x+1,transform.position.y + 0.6f);
		Instantiate(ProjectileRef, posThrow, Quaternion.Euler(new Vector3(0, 0, 170)));
		Invoke("Cooldown",Random.Range(0.55f,0.65f));
	}

	void Cooldown(){
		_isAttacking=false;
	}
}
