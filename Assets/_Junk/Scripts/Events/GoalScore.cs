using UnityEngine;
using System.Collections;

public class GoalScore : MonoBehaviour
{
	private AudioMan soundManager;

	// Use this for initialization
	void Start ()
	{
		soundManager = GameSystem.audio;
	}

	void OnTriggerEnter(Collider other)
	{
		if( other.CompareTag(Tags.ball) )
		{
			GameSystem.score.AddScore(100);
			GameSystem.score.AddMultPoint(5);

			// TODO: Scored with

			Debug.Log("GOAL");
			soundManager.PlayCelebrate();
			soundManager.PlayGoalSpeech();
		}
	}

}

