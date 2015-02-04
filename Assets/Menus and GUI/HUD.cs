using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : GUIWindow
{
	public float startupShowTime = 5.0f;
	public Text scoreText;
	public Text timeText;

	private float startTime = 0;
	
	// Use this for initialization
	void Start ()
	{
		startTime = Time.time;
		Invoke("Show", startupShowTime);
	}
	
	// Update is called once per frame
	void Update ()
	{
		ScoreManager score = GameSystem.score;

		scoreText.text = "SCORE: " + Mathf.FloorToInt(score.baseScore) +
							 "\tx" + Mathf.FloorToInt(score.multPoints);

		float timeSinceStart = Time.time - startTime;
		int seconds = Mathf.FloorToInt(timeSinceStart) % 60;
		int minutes = Mathf.FloorToInt(timeSinceStart) / 60;

		string timeStr = "";

		if( minutes < 10 )
			timeStr += "0";
		timeStr += minutes + ":";

		if( seconds < 10 )
			timeStr += "0";
		timeStr += seconds;

		timeText.text = timeStr;
	}
}
