﻿using UnityEngine;
using System.Collections;

public class RandomCharacterShouts : MonoBehaviour
{
	public AudioClip[] clips;
	public float minTime;
	public float maxTime;

	private BaseCharacterController controller;

	// Use this for initialization
	void Start ()
	{
		controller = GetComponent<BaseCharacterController>();
		StartCoroutine( PlayAudio() );
	}
	
	// Update is called once per frame
	IEnumerator PlayAudio ()
	{
		while( true )
		{
			for( float timer = Random.Range( minTime, maxTime ); timer>0; timer-=Time.deltaTime )
			{
				yield return 0;
			}

			// Play sounds only of the character's not dead
			if( controller==null || !controller.isKnockedDown )
			{
				audio.clip = clips[ Random.Range(0, clips.Length) ];
				audio.Play();
			}
		}
	}
}