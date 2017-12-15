/*
Générateur de grille
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour {

	[SerializeField]
	private int _nbCol;
	[SerializeField]
	private int _nbRow;
	[SerializeField]
	private SpriteRenderer[] _board;
	private BoardManager _bm;
	private TileInfo[,] _tileInfo;
	private static GridGenerator instance = null;
	public static GridGenerator Instance {
		get { return instance; }
	}
	
	//Si l'échiquier est en double, détruire les éléments répétitifs
	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		_bm = GetComponent<BoardManager>();
		if(_board.Length!=0){
			GenerateGrid();
		}
	}

	void GenerateGrid() {
		//Instantiation du tableau des tuiles
		_tileInfo = new TileInfo[_nbRow,_nbCol];

		float tileWidth = (_board[0].bounds.size.x);
		float tileHeight = tileWidth*0.85f;
		int altTile = 0;
		Vector2 gridSize = new Vector2(tileWidth*_nbCol,tileHeight*_nbRow);
		for(int r=0;r<_nbRow;r++){
			Vector2 posRook = new Vector2(tileHeight/1.5f - gridSize.x/2, r * tileHeight - gridSize.y/2);
			Transform rook = Instantiate(_bm._PiecesRef[6],posRook, Quaternion.identity, transform);
			rook.GetComponent<SpriteRenderer>().sortingOrder = _nbRow-r;
			for(int c=0;c<_nbCol;c++){
				Vector2 pos = new Vector2(tileWidth*1.5f + c*tileWidth - gridSize.x/2,tileHeight*-1 - r*tileHeight + gridSize.y/2);
				SpriteRenderer tileRenderer = Instantiate(_board[altTile], pos, Quaternion.identity, transform);
				if(altTile==1){
					altTile--;
				} else {
					altTile++;
				}
				//Écriture des données d'une tuile
				TileInfo myTile = new TileInfo();
				myTile.cellX = c;
				myTile.cellY = r;
				myTile.tileRef = tileRenderer.transform;
				//Envoi des données d'une tuile vers le tableau de toutes les tuiles
				_tileInfo[r,c] = myTile;
			}
		}
	}

	//Structure servant à contenir un tableau qui contient les informations de toutes les tuiles
	public struct TileInfo {
		public int cellX;
		public int cellY;
		public Transform tileRef;
	}
}
