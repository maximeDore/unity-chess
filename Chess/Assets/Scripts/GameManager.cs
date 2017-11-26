using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	[SerializeField]
	private Text _wavePercentageDisplay; // Affichage du pourcentage du niveau de complété
	private static Text _moneytor;	// Affichage d'argent sur la scène

	private static int _money = 1000;	// Quantité d'argent possédé par le joueur
	public static int _Money {
		get { return _money; }
		set { _money = value; GameManager.ShowMoney(); }
	}
	private static float _wavePercentage = 0;	// Pourcentage du niveau de complété
	public static float _WavePercentage {
		get { return _wavePercentage; }
		set {
			 if(_wavePercentage>100){
				 _wavePercentage=100;
			 } else { 
			 	_wavePercentage = value;
			 }
		}
	}
	private static bool _play;	// Partie en cours ou non
	public static bool _Play {
		get { return _play; }
		set { _play = value; }
	}
	public static Vector2 _CAMERASIZE {
		get { return new Vector2(Camera.main.orthographicSize * Screen.width / Screen.height, Camera.main.orthographicSize * 2); }
	}

	public void Play() {
		_Play = true;
		Time.timeScale = 1;
	}

	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
		_moneytor = GameObject.Find("Money").GetComponent<Text>();
		ShowMoney();
	}
	
	// Update is called once per frame
	void Update () {
		_WavePercentage = _WavePercentage+Time.deltaTime;
		_wavePercentageDisplay.text = Mathf.Floor(_wavePercentage)+"%";
	}

	// Affichage de l'argent dans l'afficheur
	static public void ShowMoney() {
		_moneytor.text = _money+"$";
	}
}
