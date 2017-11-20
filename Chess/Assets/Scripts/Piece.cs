using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

	[SerializeField]
	private LayerMask _lm;
	private Animator _animator;
	private Transform[] piecesRef;
	private int pieceIndex;
	public int PieceIndex {
		get { return pieceIndex; }
		set { pieceIndex = value; }
	}
	private Transform projectileRef;
	public Transform ProjectileRef {
		get { return projectileRef; }
		set { projectileRef = value; }
	}
	private Transform moneyRef;
	public Transform MoneyRef {
		get { return moneyRef; }
		set { moneyRef = value; }
	}

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
		switch (pieceIndex) {
			case 0:
				StartCoroutine(Bishop());
				break;
			case 1:
				StartCoroutine(Pawn());
				break;
			case 2:
				Shield();
				break;
			case 3:
				Bomb();
				break;
			case 4:
				Knight();
				break;
			default:
				Debug.Log("===> Erreur : index invalide.");
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator Bishop() {
		while(GameManager._Play){
			yield return new WaitForSeconds(5);
			Instantiate(moneyRef ,transform.position, Quaternion.identity);
		}
	}

	private IEnumerator Pawn() {
		while(GameManager._Play){
			_animator.SetBool("isAttacking", false);
			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, _lm);
			if (hit.collider != null) {
				Debug.Log("hit : "+hit.collider.name);
				PawnAttack();
			yield return new WaitForSeconds(0.5f);
			}
		}
	}

	private void PawnAttack() {
		_animator.SetBool("isAttacking", true);
		Vector2 posThrow = new Vector2(transform.position.x+1,transform.position.y + 0.5f);
		Instantiate(projectileRef, posThrow, Quaternion.identity);
	}

	void Shield() {

	}

	void Bomb() {
		_animator.SetTrigger("isExploding");
	}

	void Knight() {

	}
}
