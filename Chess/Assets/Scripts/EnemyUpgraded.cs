/*
Classe fille des ennemis avec une amélioration et une valeur déterminées au hasard
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUpgraded : Enemy {

    [SerializeField]
    private Sprite _sprite2;
    private SpriteRenderer _renderer;

	// Use this for initialization
	new void Start () {
		_animator = GetComponent<Animator>();
		_collider = GetComponent<BoxCollider2D>();
		_audio = GetComponent<AudioSource>();
		_renderer = GetComponentsInChildren<SpriteRenderer>()[0];
        int randType = Random.Range(0,2);
        if(randType==1){
            _Health = Random.Range(11,15);
            _renderer.sprite = _sprite2;
        } else {
            _speed = Random.Range(1.25f,1.5f);
        }
	}
	
}
