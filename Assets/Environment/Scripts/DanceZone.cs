using UnityEngine;
using System.Collections;

public class DanceZone: MonoBehaviour
{
	void OnTriggerStay(Collider other)
	{
		if( other.CompareTag(Tags.player) )
		{
			if(other.GetComponent<BaseCharacterController>().isDancing)
			{
				GameSystem.scoringEvent.Fire( ScoringEvent.ZONE_WIGGLE, true );
			}
			else
			{
				GameSystem.scoringEvent.Fire( ScoringEvent.ZONE_WIGGLE, false );
			}
		}
	}
}
