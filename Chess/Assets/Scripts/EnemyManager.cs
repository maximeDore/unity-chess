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
	private int _phase = 0;
	private int _enemyRow;
	private bool _finalWave;

	// Use this for initialization
	void Start () {
		StartCoroutine(StartWave());
	}
	
	// Gestionnaire de vague
	private IEnumerator StartWave () {
		yield return new WaitForSeconds(10f);	// Délai avant l'instantiation des ennemis
		StartCoroutine(SpawnQueue());
	}

	private IEnumerator SpawnQueue(){
		while(true){
			_enemyRow = Random.Range(0,_spawn.Length);
			float percentage = GameManager._WavePercentage;
			_enemyIndex = Random.Range(0,_enemyRef.Length-1);
			if(percentage == 0){
				_phase = 0;
			} else if(percentage < 25){
				_phase = 1;
			} else if(percentage < 50){
				_phase = 2;
			} else if(percentage < 75){
				_phase = 3;
			} else if(percentage < 99) {
				_phase = 4;
			} else {
				if(!_finalWave){
					yield return new WaitForSeconds(10f);
					StartCoroutine(FinalCountdown());
					_finalWave = true;
				}
				_phase = 5;
			}
			Debug.Log(_phase);
			if(_phase==0){
				yield return null;
			} else if(_phase==1){
				yield return new WaitForSeconds(10.75f);
			} else if(_phase==2 || _phase==3) {
				yield return new WaitForSeconds(5.75f);
			} else if(_phase==4) {
				yield return new WaitForSeconds(3.25f);
			} else {
				yield return new WaitForSeconds(1.25f);
			}
			if(_phase!=0){
				yield return StartCoroutine(SpawnEnemy());
			}
		}
	}

	private IEnumerator SpawnEnemy() {
		int i = 0;
		if(_phase>=3){
			i = _enemyIndex;
		} else if(_phase==5){
			i = 1;
		}
		Transform newEnemy = Instantiate(_enemyRef[i],_spawn[_enemyRow].position,Quaternion.identity, transform);
		Enemy newEnemyScript = newEnemy.GetComponent<Enemy>();
		newEnemyScript._Health = _enemyHealth[i];
		newEnemyScript._Index = i;
		yield return null;
	}

	private IEnumerator SpawnKing(){
		Transform newEnemy = Instantiate(_enemyRef[2],_spawn[_enemyRow].position,Quaternion.identity, transform);
		Enemy newEnemyScript = newEnemy.GetComponent<Enemy>();
		newEnemyScript._Health = _enemyHealth[2];
		newEnemyScript._Index = 2;
		yield return null;
	}

	private IEnumerator FinalCountdown(){
		yield return new WaitForSeconds(30f);
		yield return SpawnKing();
		StopAllCoroutines();
	}
}
