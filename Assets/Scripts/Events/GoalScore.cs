﻿using UnityEngine;
using System.Collections;

public class GoalScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.name == "GameBall")
		{
			Debug.Log("Goal!");
			//SoundManager.TriggerEvent("goal");
		}
	}
}
