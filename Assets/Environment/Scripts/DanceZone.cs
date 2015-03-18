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
				GameSystem.scoringEvent.Fire( ScoringEventType.ZONE_WIGGLE, true );
			}
			else
			{
				GameSystem.scoringEvent.Fire( ScoringEventType.ZONE_WIGGLE, false );
			}
		}
	}
}
