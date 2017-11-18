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

	private TileInfo[,] _tileInfo;

	// Use this for initialization
	void Start () {
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
	
	// Update is called once per frame
	void Update () {
		
	}

	//Structure servant à contenir un tableau qui contient les informations de toutes les tuiles
	public struct TileInfo {
		public int cellX;
		public int cellY;
		public Transform tileRef;
	}
}
