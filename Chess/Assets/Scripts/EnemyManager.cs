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
	private float _randIndex;

	// Use this for initialization
	void Start () {
		StartCoroutine(SpawnEnemy());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator SpawnEnemy(){
		while(true){
			_enemyIndex = Random.Range(0,2);
			yield return new WaitForSeconds(4.75f);
			Transform newEnemy = Instantiate(_enemyRef[_enemyIndex],_spawn[1].position,Quaternion.identity, transform);
			newEnemy.GetComponent<Enemy>()._Health = _enemyHealth[_enemyIndex];
		}
	}
}
