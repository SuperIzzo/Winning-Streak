using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScorePanel : GUIWindow
{
	public Text timePlayed;
	public Text score;
	public Text multiplier;
	public Text total;
	public Text best;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		ScoreManager scoreMan = GameSystem.score;

		System.TimeSpan timeSpan = System.TimeSpan.FromSeconds( scoreMan.timePlayed );  

		timePlayed.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
		score.text		= ""+ (int)scoreMan.baseScore;
		multiplier.text = ""+ (int)scoreMan.multPoints;
		total.text		= ""+ (int)scoreMan.totalScore;
		best.text		= ""+ (int)scoreMan.highScore;
	}
}
