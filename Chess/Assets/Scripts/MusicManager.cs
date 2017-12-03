using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	[SerializeField]
	private AudioClip _final;
	[SerializeField]
	private AudioClip _normal;
	private AudioSource _audio;
	private bool _isFinal;
	static protected bool _isMuted;
	static public bool _IsMuted {
		get { return _isMuted; }
	}
	private static MusicManager instance = null;
	public static MusicManager Instance {
		get { return instance; }
	}
	
	//Si le son est en double, détruire les éléments répétitifs
	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);	//Conserve la musique sur la scène en tout temps
	}

	void Start() {
		_audio = GetComponent<AudioSource>();
	}

	void Update() {
		if(!_isMuted){
			if(GameManager._WavePercentage>=99 && !_isFinal){
				_isFinal=!_isFinal;
				StartCoroutine(FinalMusic());
			} else if(GameManager._WavePercentage<99 && _isFinal) {
				_isFinal=!_isFinal;
				StartCoroutine(NormalMusic());
			}
		}
	}

	private IEnumerator FinalMusic(){
		yield return new WaitForSeconds(10);
		_audio.clip = _final;
		_audio.Stop();
		_audio.Play();
	}

	private IEnumerator NormalMusic(){
		_audio.clip = _normal;
		_audio.Play();
		yield return null;
	}

	public void Mute(){
		if(!_isMuted){
			_isMuted=!_isMuted;
			_audio.Pause();
		} else {
			_isMuted=!_isMuted;
			_audio.Play();
		}
		
	}
}