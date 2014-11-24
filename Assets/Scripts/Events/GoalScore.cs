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
		ScoreManager.AddScore(100);
		ScoreManager.AddMultPoint(5);

//		Commentator commentator = Commentator.GetInstance();
//		if( commentator )
//		{
//			commentator.Comment( CommentatorEvent.SCORED_GOAL );
//		}

		//if(soundManager == null)
		//	soundManager = GameObject.FindGameObjectWithTag("SoundManager");

		Debug.Log("GOAL");
		soundManager.GetComponent<AudioMan>().PlayCelebrate();
		soundManager.GetComponent<AudioMan>().PlayGoalSpeech();
		//commentator.GetComponent<Commentator>().Comment( CommentatorEvent. );
		}
	//
	}

}

