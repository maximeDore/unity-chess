using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	[SerializeField]
	private Text _wavePercentageDisplay;
	private static Text _moneytor;

	private static int _money = 1000;
	public static int _Money {
		get { return _money; }
		set { _money = value; }
	}
	private static float _wavePercentage = 0;
	public static float _WavePercentage {
		get { return _wavePercentage; }
		set { _wavePercentage = value; }
	}
	private static bool _play = true;
	public static bool _Play {
		get { return _play; }
		set { _play = value; }
	}

	// Use this for initialization
	void Start () {
		_moneytor = GameObject.Find("Money").GetComponent<Text>();
		ShowMoney();
	}
	
	// Update is called once per frame
	void Update () {
		_wavePercentageDisplay.text = Mathf.Floor(_wavePercentage)+"%";
	}

	static public void ShowMoney() {
		_moneytor.text = _money+"$";
	}
}
