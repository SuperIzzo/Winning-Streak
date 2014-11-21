using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {
	
	AudioSource soundPlayer;

	public AudioClip menuMusic;
	public AudioClip gameMusic;

	public List<AudioClip> soundEffects;
	public List<AudioClip> commentatorDialogue;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void TriggerEvent(string eventTrigger)
	{
		switch(eventTrigger)
		{
		case "goal":
			//goal sound effect
			//commentators comment on goal
			//increase score
			break;


		}
	}

	public void Play(string effect)
	{
		foreach(AudioClip ac in soundEffects)
		{
			if(ac.name == effect)
			{
				//play
			}
		}

		foreach(AudioClip ac in commentatorDialogue)
		{
			if(ac.name == effect)
			{
				//play
			}
		}

		if(menuMusic.name == effect)
		{
			//play
		}

		if(gameMusic.name == effect)
		{
			//play
		}
	}

	public void PlayInLoop(string effect)
	{
		foreach(AudioClip ac in soundEffects)
		{
			if(ac.name == effect)
			{
				//start loop
			}
		}
		
		foreach(AudioClip ac in commentatorDialogue)
		{
			if(ac.name == effect)
			{
				//start loop
			}
		}
		
		if(menuMusic.name == effect)
		{
			//start loop
		}
		
		if(gameMusic.name == effect)
		{
			//start loop
		}
	}
}
