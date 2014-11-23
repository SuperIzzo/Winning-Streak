using UnityEngine;
using System.Collections;

public class GoalScore : MonoBehaviour {

	public GameObject soundManager;

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
		if(other.tag == "weapon")
		{
			ScoreManager.AddScore(100);

			//if(soundManager == null)
			//	soundManager = GameObject.FindGameObjectWithTag("SoundManager");

			Debug.Log("GOAL");
			soundManager.GetComponent<AudioMan>().PlayCelebrate();
		}
	}
}
