using UnityEngine;
using System.Collections;

public class GoalScore : MonoBehaviour {

	public GameObject soundManager;
	public GameObject commentator;

	// Use this for initialization
	void Start () {

		//if(soundManager == null)
		soundManager = GameObject.FindGameObjectWithTag("SoundManager");
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "ball")
		{
			if(other.GetComponent<ItemStats>().scoredWith)
				return;
			ScoreManager.AddScore(100);
			ScoreManager.AddMultPoint(5);

			other.GetComponent<ItemStats>().scoredWith = true;

			Debug.Log("GOAL");
			soundManager.GetComponent<AudioMan>().PlayCelebrate();
			soundManager.GetComponent<AudioMan>().PlayGoalSpeech();
		}
	}

}

