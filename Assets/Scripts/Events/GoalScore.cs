using UnityEngine;
using System.Collections;

public class GoalScore : MonoBehaviour {

	public GameObject soundManager;

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

			if(soundManager == null)
				soundManager = GameObject.FindGameObjectWithTag("SoundManager");

			Debug.Log("GOAL");
			soundManager.GetComponent<AudioMan>().PlayCelebrate();
		}
	}
}
