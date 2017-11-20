﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	// index qui définit le type de pièce
	static private int _pieceIndex = -1;
	static public int _PieceIndex {
		get { return _pieceIndex; }
		set { _pieceIndex = value; }
	}

	// Tableau contenant toutes les différentes pièces
	[SerializeField]
	private Transform[] _piecesRef;
	public Transform[] _PiecesRef {
		get { return _piecesRef; }
	}
	[SerializeField]
	private Transform _projectileRef;
	public Transform _ProjectileRef {
		get { return _projectileRef; }
	}
	[SerializeField]
	private Transform _moneyRef;
	public Transform _MoneyRef {
		get { return _moneyRef; }
	}
	[SerializeField]
	public int[] _piecesPrice;

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			SelectTile();
		}
	}

	// Raycast 3D à partir de la caméra vers la position de la souris 
	// appelé par un clic gauche afin de détecter la tuile sélectionnée
	void SelectTile() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100)) {
			var obj = hit.collider;
			TileManager script = obj.GetComponent<TileManager>();
			// Si un bouton est cliqué et qu'il n'y a pas de pièce sur la tuile
			if(_pieceIndex!=-1 && script._PieceOnTile==false && GameManager._Money>=_piecesPrice[_PieceIndex]){
				Transform newPiece = Instantiate(_piecesRef[_pieceIndex], obj.GetComponent<Transform>());
				Piece pieceScript = newPiece.GetComponent<Piece>();
				pieceScript.PieceIndex = _PieceIndex;
				pieceScript.ProjectileRef = _ProjectileRef;
				pieceScript.MoneyRef = _MoneyRef;
				if(_pieceIndex!=3){
					script._PieceOnTile = true;
				}
				GameManager._Money -= _piecesPrice[_PieceIndex];
				GameManager.ShowMoney();
			}
		}
		_PieceIndex = -1;
	}
}