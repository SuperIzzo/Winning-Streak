﻿using UnityEngine;
using System.Collections;

public class KillPlayerZone : MonoBehaviour {

	GameObject player;
	GameObject restart;

	bool end = false;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		restart = GameObject.FindGameObjectWithTag("dummyenemy");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(!end && other.tag == "Player")
		{
			player.GetComponentInChildren<GoRagdoll>().KillPlayer();
			restart.GetComponent<AIAttack>().ManualRestart();
		}
	}
}
