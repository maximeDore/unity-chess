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
	private bool _coinInstance;
	public bool _CoinInstance {
		get { return _coinInstance; }
		set { _coinInstance = value; }
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
			case -1:
				break;
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
				Hand();
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
			if(!_coinInstance){
				yield return new WaitForSeconds(10);
				Transform coin = Instantiate(moneyRef, transform.position, Quaternion.identity, transform);
				_coinInstance = true;
				if(transform.childCount>=1){
					StartCoroutine(BishopCoinLife(coin));
				}
			}
			yield return null;
		}
	}

	private IEnumerator BishopCoinLife(Transform coin) {
		int coinTimer = 5;
		yield return new WaitForSeconds(coinTimer);
		if(coin!=null){
			Destroy(coin.gameObject);
			_CoinInstance = false;
		}
	}

	private IEnumerator Pawn() {
		while(GameManager._Play){
			_animator.SetBool("isAttacking", false);
			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, Mathf.Infinity, _lm);
			if (hit.collider != null && hit.collider.tag == "Piece") {
				Debug.Log("hit : "+hit.collider.name);
				StartCoroutine(PawnAttack());
				yield return new WaitForSeconds(0.99f);				
			}
			yield return null;
		}
	}

	private IEnumerator PawnAttack() {
		_animator.SetBool("isAttacking", true);
		yield return new WaitForSeconds(0.5f);
		Vector2 posThrow = new Vector2(transform.position.x+1,transform.position.y + 0.5f);
		Transform projectile = Instantiate(projectileRef, posThrow, Quaternion.Euler(new Vector3(0, 0, 170)));
	}

	void Shield() {

	}

	void Bomb() {
		_animator.SetTrigger("isExploding");
	}

	void Hand() {

	}
}
