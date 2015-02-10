using UnityEngine;
using System.Collections;

public class GoalScore : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if( other.CompareTag(Tags.ball) )
		{
			ScoringEventManager scoringEvent = GameSystem.scoringEvent;
			scoringEvent.Fire( ScoringEvent.SCORED_GOAL );
		}
	}

}

