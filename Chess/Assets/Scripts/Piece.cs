using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

	private bool _isDead;
	protected TileManager _tile;
	public TileManager _Tile {
		get { return _tile; }
		set { _tile = value; }
	}
	protected Animator _animator;
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
	protected int _health;
	public int _Health {
		get { return _health; }
		set {
			_health = value;
			if(_health<=0){
				Kill();
			}
		}
	}

	protected void Kill() {
		if(!_isDead){
			_isDead=true;
			_animator.SetTrigger("isKilled");
			Invoke("DestroyPiece",1);
		}
	}

	protected void DestroyPiece() {
		Destroy(gameObject);
	}

	void OnDestroy() {
		_Tile._PieceOnTile = false;
	}
}
