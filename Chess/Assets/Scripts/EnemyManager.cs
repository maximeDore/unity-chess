using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	[SerializeField]
	private Transform[] _spawn;			// Point de départ des ennemis, position d'instanciation (0-4)
	[SerializeField]
	private Transform[] _enemyRef;		// Prefabs des ennemis	(0 = Pawn / 1 = Knight / 2 = King)
	[SerializeField]
	private int[] _enemyHealth;			// Points de vie des ennemis (0 = 10 / 1 = 15 / 2 = 20)
	public int[] _EnemyHealth {
		get { return _enemyHealth; }
	}
	[SerializeField]
	private int _enemyIndex = 0;		// Index qui définit le type d'ennemi à instancier à partir de _enemyRef[]
	public int _EnemyIndex {
		get { return _enemyIndex; }
		set { _enemyIndex = value; }
	}
	private int _phase = 0;				// Section du niveau qui définit la difficulté (0 = 0% / 1 = 1-25% / 2 = 26-50% / 3 = 51-75% / 4 = 76-99% / 5 = 100%)
	private int _enemyRow;				// Index de la rangée où l'ennemi est instancié à partir de _spawn[]
	private bool _finalWave;			// Est-ce qu'il s'agit de la vague finale? (Utilisée pour éviter la répétition de la vague pendant la pause du 100%)

	// Use this for initialization
	void Start () {
		StartCoroutine(StartWave(10f));
	}
	
	// Démarrage de la vague avec un délai
	private IEnumerator StartWave(float delay) {
		yield return new WaitForSeconds(delay);	// Délai avant l'arrivée des ennemis
		StartCoroutine(SpawnQueue());
	}

	// Définition des différentes phases de la vague
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
					GameManager.FinalPopUp();
					yield return new WaitForSeconds(10f);
					StartCoroutine(FinalCountdown());
					_finalWave = true;
				}
				_phase = 5;
			}
			if(_phase!=0){
				Debug.Log(_phase);
			}
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

	// Instancier les différents ennemis selon la phase
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

	// Instancier le roi
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
