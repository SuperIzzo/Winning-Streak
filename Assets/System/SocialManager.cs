#define DEBUG_SOCIAL

using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;

public class SocialManager : MonoBehaviour
{
	private readonly string boardID = "Highscores01";

	// Use this for initialization
	void Start ()
	{
		//TODO: Social stuff
		/*
		LocalPlatform.SocialPlatform.Activate();

		Debug.Log( Social.Active );
		Social.localUser.Authenticate( SocialAuthentication );
		*/
	}
	
	void SocialAuthentication( bool success )
	{
		if( success )
		{
			#if DEBUG_SOCIAL
			Debug.Log( "Social authentication successful" );
			#endif

			ILeaderboard board = Social.CreateLeaderboard();
			board.id = boardID;
			board.LoadScores( result => { Debug.Log("ScoreLoading: " + result); } );

			Social.LoadScores( boardID, ScoresLoaded ); 
		}
		#if DEBUG_SOCIAL
		else
		{
			Debug.Log( "Social authentication failed" );
		}
		#endif
	}

	void ScoresLoaded( IScore[] scores )
	{
		foreach( IScore score in scores )
		{
			if( score.userID == Social.localUser.id )
			{
				Debug.Log( "Score: " + score.value + "   Date: " + score.date );
			}
		}
	}

	public void ReportHighScore( long score )
	{
		//TODO: Social stuff
		//Social.ReportScore( score, boardID, result => {Debug.Log( "Score reported: " + result ); } );
	}
}
