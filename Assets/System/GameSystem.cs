﻿using UnityEngine;
using System.Collections;

//--------------------------------------------------------------
/// <summary> A static global access to game systems. </summary>
//--------------------------------------
public class GameSystem
{
	//--------------------------------------------------------------
	#region		Internal variables
	//--------------------------------------
	private static SloMoManager 		_slowMotion;
	private static ScoreManager 		_score;
	private static ScoringEventManager	_scoringEvent;
	private static SocialManager		_social;
	private static Commentator  		_commentator;
	private static CrowdManager			_crowd;
	#endregion


	// Globally accessible readonly properties
	//--------------------------------------------------------------
	#region Public properties
	//--------------------------------------

	/// <summary> Gets a single instance to the 
	/// <see cref="SloMoManager"/>. </summary>
	/// <value> The slow motion manager. </value>
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

	/// <summary> Gets a single instance to the 
	/// <see cref="ScoreManager"/>. </summary>
	/// <value> The score manager. </value>
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

	/// <summary> Gets a single instance to the 
	/// <see cref="ScoringEventManager"/>. </summary>
	/// <value> The scoring event manager. </value>
	public static ScoringEventManager scoringEvent
	{
		get 
		{
			if( !_scoringEvent )
			{
				_scoringEvent = GameObject.FindObjectOfType<ScoringEventManager>();
			}
			
			return _scoringEvent;
		}
	}

	/// <summary> Gets a single instance to the 
	/// <see cref="SocialManager"/>. </summary>
	/// <value> The social manager. </value>
	public static SocialManager social
	{
		get 
		{
			if( !_social )
			{
				_social = GameObject.FindObjectOfType<SocialManager>();
			}
			
			return _social;
		}
	}

	/// <summary> Gets a single instance to the 
	/// <see cref="Commentator"/> system. </summary>
	/// <value> The commentator system. </value>
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

	/// <summary> Gets a single instance to the 
	/// <see cref="CrowdManager"/>. </summary>
	/// <value> The crowd manager. </value>
	public static CrowdManager crowd
	{
		get 
		{
			if( !_crowd )
			{
				_crowd = GameObject.FindObjectOfType<CrowdManager>();
			}
			
			return _crowd;
		}
	}
	#endregion
}
