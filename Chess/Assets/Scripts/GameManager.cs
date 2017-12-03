using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private static GameManager instance = null;
	public static GameManager Instance {
		get { return instance; }
	}
	private static int _lvl = 1;	// Vague actuelle /3
	public static int _Lvl {
		get { return _lvl; }
		set { _lvl = value; }
	}
	[SerializeField]
	private Text _waveCount;
	[SerializeField]
	private Text _wavePercentageDisplay; // Affichage du pourcentage du niveau de complété
	private static Text _moneytor;	// Affichage d'argent sur la scène

	private static int _money = 50;	// Quantité d'argent possédé par le joueur
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
	private static bool _play;	// Partie commencée ou non
	public static bool _Play {
		get { return _play; }
		set { _play = value; }
	}
	public static Vector2 _CAMERASIZE {	// Taille de la caméra 
		get { return new Vector2(Camera.main.orthographicSize * Screen.width / Screen.height, Camera.main.orthographicSize * 2); }
	}
	
	//Si le GameManager est en double, détruire celui de la scène précédente
	void Awake() {
		if (instance != null && instance != this) {
			Destroy(instance);
			return;
		} else {
			instance = this;
		}
	}

	// Débute la partie
	public void Play(GameObject bouton) {
		_play = true;
		StartCoroutine(StartWaveCount());
		Destroy(bouton);
	}

	// Permet de changer de scène vers celle passée en paramètre (string)
	public void ChangerScene(string scene){
		if(scene != "end"){
			SceneManager.LoadScene(scene);
		} else {
			_Lvl=1;
			SceneManager.UnloadSceneAsync("interlude");
			SceneManager.LoadScene(scene,LoadSceneMode.Additive);
		}
	}

	// Use this for initialization
	void Start () {
		Debug.Log(_lvl);
		if(GameObject.Find("ButtonManager")!=null){
			GameObject.Find("ButtonManager").GetComponent<ButtonManager>().MuteButton();	// Affiche le statut du bouton mute
		}
		var moneytor = GameObject.Find("Money");
		if(_waveCount!=null){
			_waveCount.text = _Lvl+"/3";
		}
		if(moneytor!=null){
			_moneytor = moneytor.GetComponent<Text>();
			ShowMoney();
		}
	}

	// Affichage de l'argent dans l'afficheur
	static public void ShowMoney() {
		_moneytor.text = _money+"$";
	}

	// Quand la partie est gagnée, on change de scène
	static public void Win(){
		if(_play){
			Debug.Log("=====>Fin de la partie!");
			_play=false;
			SceneManager.LoadScene("interlude", LoadSceneMode.Additive);
		}
	}

	// Quand la partie est perdue, on change de scène
	static public void Fail(GameObject piece){
		if(_play){
			Debug.Log("=====>Échec de la partie!");
			_play=false;
			SceneManager.LoadScene("interlude", LoadSceneMode.Additive);
		} else {
			Destroy(piece,1);
		}
	}

	// Débute le compteur de la vague
	public IEnumerator StartWaveCount() {
		yield return new WaitForSeconds(20f);
		while(true){
			_WavePercentage = _WavePercentage+Time.deltaTime/3*_Lvl;
			_wavePercentageDisplay.text = Mathf.Floor(_wavePercentage)+"%";
			yield return null;
		}
	}
}
