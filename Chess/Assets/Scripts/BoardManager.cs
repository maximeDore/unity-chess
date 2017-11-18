using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	static private int _pieceIndex = 0;
	static public int _PieceIndex {
		get { return _pieceIndex; }
		set { _pieceIndex = value; }
	}

	[SerializeField]
	private Transform[] _piecesRef;

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			// Debug.Log("Clic gauche, raycasting");
			SelectTile();
		}
	}

	void SelectTile() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100)) {
			var obj = hit.collider;
			Tile script = obj.GetComponent<Tile>();
			// Debug.Log("Hit object: " + obj.gameObject.name);
			// obj.GetComponent<SpriteRenderer>().color = Color.red; //Test
			if(_pieceIndex!=-1 && script._PieceOnTile==false){
				Instantiate(_piecesRef[_pieceIndex], obj.GetComponent<Transform>());
				script._PieceOnTile = true;
				_PieceIndex = -1;
			}
		}
	}
}
