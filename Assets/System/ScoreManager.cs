﻿using UnityEngine;
using System.Collections;

//--------------------------------------------------------------
/// <summary> Score manager. Deals with scores and stuff. </summary>
//--------------------------------------
public class ScoreManager : MonoBehaviour
{
	public float baseScore = 0; 
	public float multPoints = 1;
	public float totalScore{ get{ return (int)(baseScore * multPoints); } }
	public float timePlayed = 0;

	static bool isActive = true;

	static float maxAccumulationTime = 3;
	static float accumulationTimer = 0;
	static float accumulatedMult = 0;

	static float multToAnnounce = 0;

	float timer = 0;

	void Start()
	{
		StartTimer();
	}

	// Update is called once per frame
	void Update () 
	{
		// TODO: Hacky!
		if(Application.loadedLevelName != "main-2" || !isActive)
			return;

		accumulationTimer += Time.deltaTime;
		if( accumulationTimer > maxAccumulationTime )
		{
			float randomBonus = 0.9f + Random.value * 0.6f;
			accumulatedMult = Mathf.Clamp(accumulatedMult, 0.0f, 10f);
			AnnouncePoints( accumulatedMult * randomBonus );
			accumulatedMult = 0;
		}

		AttemptComment();

		timePlayed += Time.deltaTime;
		timer += Time.deltaTime;

		if(timer > 0.1f)
		{
			baseScore++;
			timer = 0;
		}
	}

	void AnnouncePoints( float pts )
	{
		multToAnnounce += pts;
	}


	void AttemptComment()
	{
		if( multToAnnounce >=1 )
		{
			Commentator commentator = GameSystem.commentator;

			if( commentator )
			{
				if( commentator.Comment( GetPtsEvent(multToAnnounce) ) )
				{
					multPoints += multToAnnounce;
					multToAnnounce = 0;
				}
			}
		}
	}

	CommentatorEvent GetPtsEvent( float pts )
	{
		if( pts>=10 )
			return CommentatorEvent.PTS_10;
		if( pts>=9 )
			return CommentatorEvent.PTS_9;
		if( pts>=8 )
			return CommentatorEvent.PTS_8;
		if( pts>=7 )
			return CommentatorEvent.PTS_7;
		if( pts>=6 )
			return CommentatorEvent.PTS_6;
		if( pts>=5 )
			return CommentatorEvent.PTS_5;
		if( pts>=4 )
			return CommentatorEvent.PTS_4;
		if( pts>=3 )
			return CommentatorEvent.PTS_3;
		if( pts>=2 )
			return CommentatorEvent.PTS_2;
		if( pts>=1 )
			return CommentatorEvent.PTS_1;

		return CommentatorEvent.NONE;
	}

	void OnGUI()
	{
		GUI.Label(new Rect(10,10,200,50), "Score: " + ((int)baseScore) + " x" + ((int)multPoints) );
	}

	public void AddScore(float toAdd)
	{
		if( !isActive )
			return;

		baseScore += toAdd;
	}
	
	public void AddMultPoint( float multPts )
	{
		if( !isActive )
			return;

		accumulatedMult += multPts;
		accumulationTimer = 0;
	}

	private void ResetScore()
	{
		baseScore = 0;
		multPoints = 1;
		multToAnnounce = 0;
		accumulatedMult = 0;

		timePlayed = 0;
	}

	public void StartTimer()
	{
		ResetScore();
		isActive = true;
	}

	public void StopTimer()
	{
		isActive = false;
	}
}
