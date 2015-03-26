using UnityEngine;
using System.Collections;

//--------------------------------------------------------------
/// <summary> A static global access to game systems. </summary>
//--------------------------------------
public class GameSystem
{
	//--------------------------------------------------------------
	#region		Internal variables
	//--------------------------------------
	private static TimeFlow 		_timeFlow;
	private static ScoreManager 		_score;
	private static ScoringEventManager	_scoringEvent;
	private static DifficultyManager	_difficulty;
	private static SocialManager		_social;
	private static CrowdManager		_crowd;
	private static AchievementsManager	_achievements;
	#endregion


	// Globally accessible readonly properties
	//--------------------------------------------------------------
	#region Public properties
	//--------------------------------------

	/// <summary> Gets a single instance to the 
	/// <see cref="TimeFlow"/>. </summary>
	/// <value> The time flow manager. </value>
	public static TimeFlow timeFlow
	{
		get
		{
			if( !_timeFlow )
			{
				_timeFlow = GameObject.FindObjectOfType<TimeFlow>();
			}

			return _timeFlow;
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
	/// <see cref="AchievementsManager"/>. </summary>
	/// <value> The achievements manager. </value>
	public static AchievementsManager achievements
	{
		get 
		{
			if( !_achievements )
			{
				_achievements = GameObject.FindObjectOfType<AchievementsManager>();
			}
			
			return _achievements;
		}
	}

	/// <summary> Gets a single instance to the 
	/// <see cref="DifficultyManager"/>. </summary>
	/// <value> The difficulty manager. </value>
	public static DifficultyManager difficulty
	{
		get 
		{
			if( !_difficulty )
			{
				_difficulty = GameObject.FindObjectOfType<DifficultyManager>();
			}
			
			return _difficulty;
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
