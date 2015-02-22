using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ScoringEvent
{
	NONE = 0,
	PICKED_BALL,
	DODGE_TACKLE,
	HIT_PLAYER,
	WIGGLE,
	TOUCH_DOWN,
	SCORED_GOAL
}

//--------------------------------------------------------------
/// <summary> 	Scoring event manager is an utility class that
/// 			deals with "scoring events". </summary>
/// <description> 
/// Scoring events are certain events that happen in the game due 
/// to the player's interaction with the game world. These events
/// can be detected from anywhere but must be registered here.
/// 
/// ScoringEventManager is responsible for awarding players with
/// score points and audio-visual feedback (such as commentary).
/// </description>
//--------------------------------------
public class ScoringEventManager : MonoBehaviour
{
	[System.Serializable]
	public class ScoreData
	{
		[System.Serializable]
		public struct Comment
		{
			public CommentatorEvent comment;
			public float chance;
			public float delay;
			public bool	 mustBeOn;
		}

		public enum EventIgnition
		{
			ONE_TIME,
			CONTINUOUS
		}

		public string description;
		public ScoringEvent scoringEvent;
		public EventIgnition ignition;
		public float baseScore; 
		public float multPoints;
		public float hypeEffect;
		public Comment comment;
	}

	public ScoreData[] scoresList;
	private Dictionary<ScoringEvent, ScoreData> scoresMap;

	private Dictionary<ScoringEvent, bool> ongoingScores;

	// Use this for initialization
	void Start ()
	{
		// Create and populate the scores map
		scoresMap = new Dictionary<ScoringEvent, ScoreData>();
		ongoingScores = new Dictionary<ScoringEvent, bool>();

		foreach( ScoreData data in scoresList )
		{
			scoresMap[ data.scoringEvent ] = data;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	public void Fire( ScoringEvent eventType, bool on = true )
	{
		if( scoresMap.ContainsKey(eventType) )
		{
			ScoreManager 	score		= GameSystem.score;
			CrowdManager	crowd		= GameSystem.crowd;
			ScoreData 		scoreData 	= scoresMap[eventType];

			if( scoreData.ignition == ScoreData.EventIgnition.ONE_TIME )
			{
				score.AddScore(		scoreData.baseScore  );
				score.AddMultPoint(	scoreData.multPoints );
				crowd.hype += scoreData.hypeEffect;
			}
			else
			{
				bool prevOn = ongoingScores.ContainsKey(eventType) && ongoingScores[eventType];
				ongoingScores[eventType] = on;

				if( on && !prevOn )
				{
					StartCoroutine( ContinuousPoints(eventType) );
				}
			}

			// Comment queue
			if(   scoreData.comment.comment != CommentatorEvent.NONE
			   && scoreData.comment.chance > Random.value )
			{
				StartCoroutine( Comment( eventType, scoreData.comment ) );
			}
		}
	}

	IEnumerator ContinuousPoints( ScoringEvent eventType )
	{
		ScoreManager score = GameSystem.score;
		CrowdManager crowd = GameSystem.crowd;
		ScoreData scoreData = scoresMap[eventType];

		while( ongoingScores[eventType] )
		{
			yield return 0;

			// Add points per second
			score.AddScore(		scoreData.baseScore  * Time.deltaTime );
			score.AddMultPoint( scoreData.multPoints * Time.deltaTime );
			crowd.hype += scoreData.hypeEffect * Time.deltaTime;
		}
	}

	IEnumerator Comment( ScoringEvent eventType, ScoreData.Comment comment )
	{
		for( float time = comment.delay; time>0; time-=Time.deltaTime )
		{
			yield return 0;
		}

		if( !comment.mustBeOn || ongoingScores[eventType] )
		{
			Commentator commentator	= GameSystem.commentator;
			commentator.Comment( comment.comment );
		}
	}
}
