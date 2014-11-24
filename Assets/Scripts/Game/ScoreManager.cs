using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{

	public static int score
	{
		get{ return (int)(baseScore * multPoints); }
	}

	public static float baseScore = 0; 
	public static float multPoints = 1;
	static bool isActive = true;

	static float maxAccumulationTime = 5;
	static float accumulationTimer = 0;
	static float accumulatedMult = 0;

	static float multToAnnounce = 0;

	float timer = 0;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Application.loadedLevelName != "main-2" || !isActive)
			return;


		accumulationTimer += Time.deltaTime;
		if( accumulationTimer > maxAccumulationTime )
		{
			float randomBonus = 0.9 + Random.value * 0.6;
			accumulatedMult = Mathf.Clamp(accumulatedMult, 0.0f, 10f);
			AnnouncePoints( accumulatedMult * randomBonus );
			accumulatedMult = 0;
		}


		AttemptComment();


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
			Commentator commentator = Commentator.GetInstance();

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

	public static void AddScore(float toAdd)
	{
		baseScore += toAdd;
	}
	
	public static void AddMultPoint( float multPts )
	{
		accumulatedMult += multPts;
	}

	public static void StartTimer()
	{
		baseScore = 0;
		multPoints = 0;
		isActive = true;
	}

	public static void StopTimer()
	{
		baseScore = 0;
		multPoints = 0;
		isActive = false;
	}
}
