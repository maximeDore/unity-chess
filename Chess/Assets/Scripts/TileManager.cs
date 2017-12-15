/*
Tuile
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

	private bool _pieceOnTile;
	public bool _PieceOnTile {
		get { return _pieceOnTile; }
		set { _pieceOnTile = value; }
	}
}
