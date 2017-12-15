/*
Classe mère des ennemis
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	protected AudioClip[] _sfx;			// Clips audio de attaquePawn, attaqueKnight, hit, mort
	public AudioClip[] _Sfx {
		get { return _sfx; }
		set { _sfx = value; }
	}
	protected Animator _animator;
	protected BoxCollider2D _collider;
	protected AudioSource _audio;
	private bool _isAttacking;			// Est-ce que l'ennemi attaque?
	public bool _IsAttacking {
		get { return _isAttacking; }
	}
	protected bool _isDead;				// Est-ce que l'ennemi est mort?
	public bool _IsDead {
		get { return _isDead; }
	}

	private bool _isColliding;			// Est-ce que l'ennemi est superposé avec un autre ennemi?
	protected int _index;
	public int _Index {
		get { return _index; }
		set { _index = value; }
	}
	protected int _health;				// Vie actuelle de l'ennemi
	public int _Health {
		get { return _health; }
		set { _health = value; }
	}
	protected float _speed = 1f;

	// Use this for initialization
	protected void Start () {
		_animator = GetComponent<Animator>();
		_collider = GetComponent<BoxCollider2D>();
		_audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!_isAttacking && !_isDead && !_isColliding) {	// Si l'ennemi n'attaque pas, n'est pas superposé à un autre et qu'il n'est pas mort, il avance
			Move();
		}
	}

	// Fait avancer l'ennemi
	void Move() {
		transform.Translate(Vector3.left * Time.deltaTime * _speed);
	}

	// L'ennemi perd de la vie
	public void Damage() {
		_Health--;
		if(_health<=0){
			Kill();
		}
	}

	// Joue l'animation choisie avant d'appeler la destruction de l'objet
	// Choisir entre : "isKilled"(defaut), "isExploding", "isSplatted"
	public void Kill(string animation = "isKilled") {
		StopAllCoroutines();
		if(transform!=null){
			_animator.SetTrigger(animation);
			_isDead = true;
			if(animation=="isKilled"){
				_audio.clip = _Sfx[2];
				_audio.Play();
			}
			_collider.enabled = false;
			DestroyEnemy();
		}
	}

	// Attaque de l'ennemi sur une piece blanche
	private IEnumerator Attack(Piece piece) {
		while(_isAttacking){
			yield return new WaitForSeconds(1);
			if(_isAttacking){
				_audio.clip = _Sfx[1];
				_audio.Play();
				piece._Health--; 
			} else {
				yield break;
			}
		}
	}

	// // Collision entre un ennemi et une pièce blanche
	// void OnCollisionEnter2D(Collision2D other) {
	// 	if(other.gameObject.tag == "Piece" && transform.position.x>other.transform.position.x){
	// 		if(!_isAttacking){
	// 			_isAttacking = true;
	// 			_animator.SetBool("isAttacking", true);
	// 			StartCoroutine(Attack(other.gameObject.GetComponent<Piece>()));
	// 		}
	// 	} else if(other.gameObject.tag == "Finish"){
	// 		GameManager.Fail(gameObject);				// Si l'ennemi atteint l'autre extrémité de l'écran, il provoque l'échec de la partie
	// 	}
	// }

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Piece" && transform.position.x>other.transform.position.x){
			if(!_isAttacking){
				_isAttacking = true;
				_animator.SetBool("isAttacking", true);
				StartCoroutine(Attack(other.gameObject.GetComponent<Piece>()));
			}
		} else if(other.gameObject.tag == "Finish"){
			GameManager.Fail(gameObject);				// Si l'ennemi atteint l'autre extrémité de l'écran, il provoque l'échec de la partie
		} else if(other.tag == "Enemy") {				// Si l'ennemi est en collision avec un autre, il s'arrête
			float deltaX = other.transform.position.x - transform.position.x;
			if(deltaX <= 0.1f){
				_isColliding = true;
				_animator.SetBool("isColliding", _isColliding);
			} else {
				Debug.Log(deltaX);
				_isColliding = false;
				_animator.SetBool("isColliding", _isColliding);
			}
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if(other.gameObject.tag == "Enemy"){
			float deltaX = other.transform.position.x - transform.position.x;
			if(deltaX <= 0.15f){
				_isColliding = true;
				_animator.SetBool("isColliding", _isColliding);
			} else {
				_isColliding = false;
				_animator.SetBool("isColliding", _isColliding);				
			}
		} else if (other.gameObject.tag == "Piece"){
			_animator.SetBool("isAttacking", true);
			if(!_isAttacking){
				_isAttacking = true;
				StartCoroutine(Attack(other.gameObject.GetComponent<Piece>()));
			}
		} else if(other.gameObject.tag == "Respawn"){
			_isColliding = false;
			_animator.SetBool("isColliding", _isColliding);	
		}
	}

	// Si l'ennemi ne touche plus une pièce (la piece est morte), il n'attaque plus
	void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.tag == "Piece"){
			_isAttacking = false;
			_animator.SetBool("isAttacking", false);
		} if(other.gameObject.tag == "Enemy"){
			_isColliding = false;	
			_animator.SetBool("isColliding", _isColliding);
		}
	}

	// Détruire l'ennemi avec un délai d'une seconde
	void DestroyEnemy() {
		StopAllCoroutines();
		EnemyManager._EnemyCount--;
		Debug.Log(EnemyManager._EnemyCount);
		Destroy(gameObject,1);
	}
}
