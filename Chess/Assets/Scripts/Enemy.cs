using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField]
	private AudioClip _deadSfx;			// Clip audio de mort
	private Animator _animator;
	private BoxCollider2D _collider;
	private AudioSource _sfx;
	private bool _isAttacking;			// Est-ce que l'ennemi attaque?
	public bool _IsAttacking {
		get { return _isAttacking; }
	}
	private bool _isDead;				// Est-ce que l'ennemi est mort?
	private int _index;
	public int _Index {
		get { return _index; }
		set { _index = value; }
	}
	private int _health;				// Vie actuelle de l'ennemi
	public int _Health {
		get { return _health; }
		set { _health = value; }
	}

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
		_collider = GetComponent<BoxCollider2D>();
		_sfx = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!_isAttacking && !_isDead) {	// Si l'ennemi n'attaque pas et qu'il n'est pas mort, il avance
			Move();
		}
	}

	// Fait avancer l'ennemi
	void Move() {
		transform.Translate(Vector3.left * Time.deltaTime);
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
		_animator.SetTrigger(animation);
		_isDead = true;
		if(animation=="isKilled"){
			_sfx.clip = _deadSfx;
			_sfx.Play();
		}
		_collider.enabled = false;
		if(_index==2){
			GameManager.Win();
		}
		DestroyEnemy();
	}

	// Attaque de l'ennemi sur une piece blanche
	private IEnumerator Attack(Piece piece) {
		while (_isAttacking) {
			yield return new WaitForSeconds(1);
			if(_isAttacking){
				_sfx.Play();
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
		} else if(other.gameObject.tag == "Finish"){
			GameManager.Fail(gameObject);				// Si l'ennemi atteint l'autre extrémité de l'écran, il provoque l'échec de la partie
		}
	}

	// Si l'ennemi ne touche plus une pièce (la piece est morte), il n'attaque plus
	void OnCollisionExit2D(Collision2D other) {
		if(other.gameObject.tag == "Piece"){
			_isAttacking = false;
			_animator.SetBool("isAttacking", false);
		}
	}

	// Détruire l'ennemi avec un délai d'une seconde
	void DestroyEnemy() {
		Destroy(gameObject,1);
	}
}
