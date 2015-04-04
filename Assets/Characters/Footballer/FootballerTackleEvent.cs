﻿using UnityEngine;
using System.Collections;

public class FootballerTackleEvent : MonoBehaviour
{
	public BaseCharacterController controller;
	public AIInput				   aiInput;

	public float 		tackleMissTime = 5.0f;
	public AudioClip[]	tackleFailSFX;
	public AudioClip[]	tackleSucceedSFX;

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

		if( !player.isKnockedDown && aiInput.target == player )
		{
			var scoringEvent = GameSystem.scoringEvent;
			scoringEvent.Fire( ScoringEventType.DODGE_TACKLE );

			if( GetComponent<AudioSource>() )
			{
				GetComponent<AudioSource>().clip = tackleFailSFX[ Random.Range(0, tackleFailSFX.Length) ];
				GetComponent<AudioSource>().Play();
			}
		}
		else if( GetComponent<AudioSource>() )
		{
			GetComponent<AudioSource>().clip = tackleFailSFX[ Random.Range(0, tackleFailSFX.Length) ];
			GetComponent<AudioSource>().Play();
		}
	}
}
