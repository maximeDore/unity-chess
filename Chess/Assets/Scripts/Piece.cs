using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

	private bool _isDead;
	protected Transform projectileRef;
	public Transform ProjectileRef {
		get { return projectileRef; }
		set { projectileRef = value; }
	}
	protected Transform[] piecesRef;
	protected int pieceIndex;
	public int PieceIndex {
		get { return pieceIndex; }
		set { pieceIndex = value; }
	}
	protected Transform moneyRef;
	public Transform MoneyRef {
		get { return moneyRef; }
		set { moneyRef = value; }
	}
	protected int health;
	public int Health {
		get { return health; }
		set {
			health = value;
			if(health<=0){
				Kill();
			}
		}
	}

	protected void Kill() {
		if(!_isDead){
			_isDead=true;
			Invoke("DestroyPiece",1);
		}
	}

	protected void DestroyPiece() {
		Destroy(gameObject);
	}
}
