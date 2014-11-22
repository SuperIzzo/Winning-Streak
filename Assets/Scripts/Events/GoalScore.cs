using UnityEngine;
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
			ScoreManager.AddScore(100);
			Debug.Log("GOOOOOOOAL!");
			//SoundManager.TriggerEvent("goal");
		}
	}
}
