using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	[SerializeField]
	private Transform[] _spawn;
	[SerializeField]
	private Transform[] _enemyRef;
	[SerializeField]
	private int[] _enemyHealth;
	public int[] _EnemyHealth {
		get { return _enemyHealth; }
	}
	[SerializeField]
	private int _enemyIndex = 0;
	public int _EnemyIndex {
		get { return _enemyIndex; }
		set { _enemyIndex = value; }
	}

	// Use this for initialization
	void Start () {
		// StartCoroutine(SpawnEnemy());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator SpawnEnemy(){
		while(GameManager._Play){
			yield return new WaitForSeconds(5);
			Instantiate(_enemyRef[_enemyIndex],_spawn[1].position,Quaternion.identity, transform);
		}
	}
}
