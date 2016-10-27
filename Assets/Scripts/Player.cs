﻿using UnityEngine;
using System.Collections;

public class Player : Photon.MonoBehaviour {

	private Vector3 targetPos;
	public float movespeed = 5.0f;
	public float maxHealth = 10.0f;
	private float health;
	// Use this for initialization
	void Start () {
		health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		//Only control my player object
		if (photonView.isMine) {
			// Update target location
			if (Input.GetMouseButton (1)) {
				targetPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				targetPos.z = transform.position.z;
				Debug.Log (targetPos);
			}
			//Move toward target location
			transform.position = Vector3.MoveTowards (transform.position, targetPos, movespeed * Time.deltaTime);
		}
		//debug hp
		health -= Time.deltaTime;
		if (health < 0)
			health = maxHealth; 
	}
}
