using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interlude : MonoBehaviour {

	[SerializeField]
	private Text _waveCount;
	private GameManager _gm;
	private Animator _animator;

	// Use this for initialization
	void Start () {
		_gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		if(_waveCount!=null){
			_waveCount.text = GameManager._Lvl+"/3";
		}
		_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Retour() {
		_animator.SetTrigger("isSliding");
		Invoke("RetourAuMenu",1);
	}

	public void RetourAuMenu() {
		GameManager._Money=50;
		GameManager._WavePercentage=0;
		_gm.ChangerScene("credits");
	}


	public void Suivant() {
		_animator.SetTrigger("isSliding");
		Invoke("VagueSuivante",1);
	}

	void VagueSuivante() {
		GameManager._Money=50;
		GameManager._WavePercentage=0;
		GameManager._Lvl++;
		if(GameManager._Lvl<=3){
			_gm.ChangerScene("main");
		} else {
			_gm.ChangerScene("end");
		}
	}
}
