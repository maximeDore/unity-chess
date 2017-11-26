using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private bool _isAttacking;
	public bool _IsAttacking {
		get { return _isAttacking; }
	}
	private bool _isDead;
	private Animator _animator;
	private int _health;
	public int _Health {
		get { return _health; }
		set { _health = value; }
	}
	private EnemyManager _em;
	private BoxCollider2D _collider;

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
		_em = GetComponent<EnemyManager>();
		_collider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!_isAttacking && !_isDead) {
			Move();
		}
	}

	void Move() {
		transform.Translate(Vector3.left * Time.deltaTime);
	}

	public void Damage() {
		_Health--;
		if(_health<=0){
			Kill();
		}
	}

	// Joue l'animation choisie avant d'appeler la destruction de l'objet
	// Choisir entre : "isKilled"(defaut), "isExploding", "isSplatted"
	public void Kill(string animation = "isKilled") {
		_animator.SetTrigger(animation);
		_isDead = true;
		_collider.enabled = false;
		DestroyEnemy();
	}

	// Attaque de l'ennemi envers une piece
	private IEnumerator Attack(Piece piece) {
		while (_isAttacking) {
			yield return new WaitForSeconds(1);
			if(_isAttacking){
				piece._Health--; 
			}
		}
		yield return null;
	}

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Piece" && transform.position.x>other.transform.position.x){
			_isAttacking = true;
			_animator.SetBool("isAttacking", true);
			StartCoroutine(Attack(other.gameObject.GetComponent<Piece>()));
		}
	}

	void OnCollisionExit2D(Collision2D other) {
		if(other.gameObject.tag == "Piece"){
			_isAttacking = false;
			_animator.SetBool("isAttacking", false);
		}
	}

	void DestroyEnemy() {
		Destroy(gameObject,1);
	}
}
