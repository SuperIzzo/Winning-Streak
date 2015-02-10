using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BaseCharacterController))]
public class StreakerEventAnnouncer : MonoBehaviour
{
	BaseCharacterController controller;
	ScoringEventManager 	scoringEvent;

	bool	pickedBallEvent 	= false;
	bool	isDancingEvent		= false;

	// Use this for initialization
	void Start ()
	{
		controller = GetComponent<BaseCharacterController>();
		scoringEvent = GameSystem.scoringEvent;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( scoringEvent && controller )
		{
			// Ball grab scoring
			if( controller.heldObject!=null ^ pickedBallEvent )
			{
				pickedBallEvent = !pickedBallEvent;

				if( pickedBallEvent )
					scoringEvent.Fire( ScoringEvent.PICKED_BALL );
			}

			// Dance scoring
			if( controller.isDancing ^ isDancingEvent)
			{
				isDancingEvent = !isDancingEvent;
				scoringEvent.Fire( ScoringEvent.WIGGLE, isDancingEvent );
			}
		}
	}
}
