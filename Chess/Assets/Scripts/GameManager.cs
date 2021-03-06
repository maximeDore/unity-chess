﻿/*
Classe de gestion du jeu
S'occupe des changements de scène, de la gestion de l'argent et du pourcentage de la vague
*/

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
	private static int _lvl = 1;				// Vague actuelle /3
	public static int _Lvl {
		get { return _lvl; }
		set { _lvl = value; }
	}
	private static string[] _statusMessage = {"survécue","échouée"};
	public static string[] _StatusMessage {
		get { return _statusMessage; }
	}
	private static string _status;				// String qui représente le texte affiché dans l'interlude selon la victoire ou la défaite ("échouée" , "survécue")
	public static string _Status {
		get { return _status; }
	}
	[SerializeField]
	private AudioClip[] _clap;					// Clip audio de rétroaction
	public AudioClip[] _Clap {
		get { return _clap; }
	}
	[SerializeField]
	private Text _waveCount;					// Affichage du chiffre correspondant à la vague en cours + /3
	private static Text _wavePercentageDisplay; // Affichage du pourcentage du niveau de complété
	private static Animator _finalPopUp;		// Animation du compte à rebours de la vague finale
	private static Text _moneytor;				// Affichage d'argent sur la scène
	private int _startingMoney = 50;			// Valeur qui représente la quantité d'argent de départ
	private static int _money;					// Quantité d'argent possédé par le joueur
	public static int _Money {
		get { return _money; }
		set { _money = value; GameManager.ShowMoney(); }	// Mise à jour de l'affichage de l'argent
	}
	private static float _wavePercentage;	// Pourcentage du niveau de complété
	public static float _WavePercentage {
		get { return _wavePercentage; }
		set { _wavePercentage = Mathf.Clamp(value,0,100); ShowPercentage(); }	// Mise à jour de l'affichage du pourcentage
	}
	private static bool _play;					// Est-ce que la partie est commencée? (true quand le joueur clique sur PRÊT)
	public static bool _Play {
		get { return _play; }
		set { _play = value; }
	}
	public static Vector2 _CAMERASIZE {			// Taille de la caméra 
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

	// Démarrage de l'affichage dans l'interface et réinitialisation des variables _money et _wavePercentage
	void Start () {
		// Debug.Log(_lvl);
		if(GameObject.Find("ButtonManager")!=null){
			GameObject.Find("ButtonManager").GetComponent<ButtonManager>().MuteButton();	// Affiche le statut du bouton mute
		}
		var moneytor = GameObject.Find("Money");
		var percentageText = GameObject.Find("WavePercentage");
		if(GameObject.Find("FinalPopUp")!=null){
			_finalPopUp = GameObject.Find("FinalPopUp").GetComponent<Animator>();
		}
		if(_waveCount!=null){
			_waveCount.text = _Lvl+"/3";
		}
		if(percentageText!=null){
			_wavePercentageDisplay = percentageText.GetComponent<Text>();
			_WavePercentage=0;
		}
		if(moneytor!=null){
			_moneytor = moneytor.GetComponent<Text>();
			_Money=_startingMoney;
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
		_play = false;
		_wavePercentage = 0;
		if(scene != "end"){
            SceneManager.LoadScene(scene);
		} else {
			_Lvl=1;
			SceneManager.UnloadSceneAsync("interlude");
			SceneManager.LoadScene(scene,LoadSceneMode.Additive);
		}
	}

	public void Instructions() {
		Animator cam = Camera.main.GetComponent<Animator>();
		bool instructions = cam.GetBool("instructions");
		cam.SetBool("instructions", !instructions);
	}

	// Affichage de l'argent dans l'afficheur
	static public void ShowMoney() {
		_moneytor.text = _money+"$";
	}

	// Affichage du pourcentage de la vague en cours
	static public void ShowPercentage() {
		_wavePercentageDisplay.text = Mathf.Floor(_wavePercentage)+"%";
	}

	// Quand la partie est gagnée, on change de scène
	static public void Win(){
		if(_play){
            _wavePercentage = 0;
			_status = _statusMessage[0];				// Définit le message de rétroaction
			Debug.Log("=====>Fin de la partie!");
			_play = false;
			SceneManager.LoadScene("interlude", LoadSceneMode.Additive);
		}
	}

	// Quand la partie est perdue, on change de scène
	static public void Fail(GameObject piece){
		if(_play){
            Camera.main.GetComponent<Animator>().SetTrigger("Failure");
			Transform whiteKing = GameObject.Find("wKing").transform;	// Va chercher la position du roi blanc
			piece.transform.position = new Vector3(piece.transform.position.x, whiteKing.transform.position.y, piece.transform.position.z);	//Déplace le premier ennemi devant le roi blanc
			_status = _statusMessage[1];				// Définit le message de rétroaction
			Debug.Log("=====>Échec de la partie!");
			_play = false;
			instance.StartCoroutine(DelayInterlude(6f));
		}
		Destroy(piece,10);
	}

	private static IEnumerator DelayInterlude(float delay) {
		yield return new WaitForSeconds(delay);
		_wavePercentage = 0;
		SceneManager.LoadScene("interlude", LoadSceneMode.Additive);
	}

	// Débute le compteur de la vague en fonction du niveau
	public IEnumerator StartWaveCount() {
		yield return new WaitForSeconds(25f);
		while(true){
			_WavePercentage = _WavePercentage+Time.deltaTime/2.5f*_Lvl;
			_wavePercentageDisplay.text = Mathf.Floor(_wavePercentage)+"%";
			yield return null;
		}
	}

	static public void FinalPopUp() {
		_finalPopUp.SetTrigger("Final");
	}

	public void Quit() {
		Application.Quit();
	}
}
