using UnityEngine;
using System.Collections;

public class GameSystem
{
	// Internal variables
	private static SloMoManager _slowMotion;
	private static ScoreManager _score;
	private static Commentator  _commentator;
	private static AudioMan		_audio;

	// Globally accessible readonly properties
	public static SloMoManager slowMotion
	{
		get
		{
			if( !_slowMotion )
			{
				_slowMotion = GameObject.FindObjectOfType<SloMoManager>();
			}

			return _slowMotion;
		}
	}

	public static ScoreManager score
	{
		get 
		{
			if( !_score )
			{
				_score = GameObject.FindObjectOfType<ScoreManager>();
			}

			return _score;
		}
	}

	public static Commentator commentator
	{
		get 
		{
			if( !_commentator )
			{
				_commentator = GameObject.FindObjectOfType<Commentator>();
			}
			
			return _commentator;
		}
	}

	public static AudioMan audio
	{
		get 
		{
			if( !_audio )
			{
				_audio = GameObject.FindObjectOfType<AudioMan>();
			}
			
			return _audio;
		}
	}
}
