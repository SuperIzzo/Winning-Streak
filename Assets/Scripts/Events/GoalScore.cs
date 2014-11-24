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

	void OnTriggerEnter()
	{
//		if(other.tag == "weapon")
//		{
		ScoreManager.AddScore(100);
		ScoreManager.AddMultPoint(5);

		Commentator commentator = Commentator.GetInstance();
		if( commentator )
		{
			commentator.Comment( CommentatorEvent.SCORED_GOAL );
		}

		//if(soundManager == null)
		//	soundManager = GameObject.FindGameObjectWithTag("SoundManager");

		Debug.Log("GOAL");
		soundManager.GetComponent<AudioMan>().PlayCelebrate();
		//commentator.GetComponent<Commentator>().Comment( CommentatorEvent. );
	//
	}
}
