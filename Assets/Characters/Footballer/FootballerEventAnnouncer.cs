using UnityEngine;
using System.Collections;

public class FootballerEventAnnouncer : MonoBehaviour
{
	public BaseCharacterController controller;
	public float dodgeScore = 0.3f;

	[Range(0,1)]
	public float announcementChance = 0.3f;

	public	float tackleMissTime = 5.0f;

	private float tackleMissTimer;
	
	// Update is called once per frame
	void Update ()
	{
		if( controller.isTackling && tackleMissTimer<=0 )
		{
			tackleMissTimer = tackleMissTime;
		}

		if( tackleMissTimer>0 )
		{
			tackleMissTimer-= Time.deltaTime;

			if( tackleMissTimer<=0 )
			{
				ResolveTackle();
			}
		}
	}

	void ResolveTackle()
	{
		BaseCharacterController player = Player.characterController;
		ScoreManager score = GameSystem.score;
		Commentator commentator = GameSystem.commentator;

		if( !player.isKnockedDown )
		{
			var scoringEvent = GameSystem.scoringEvent;
			scoringEvent.Fire( ScoringEvent.DODGE_TACKLE );
		}
	}
}
