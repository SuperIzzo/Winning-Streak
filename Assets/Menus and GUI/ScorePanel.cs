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

	// Update is called once per frame
	void Update ()
	{
		Score scoreMan = Player.p1.score;
		float highScore = Persistence.GetFloat("HighScore", 0);

		System.TimeSpan timeSpan = System.TimeSpan.FromSeconds( GameSession.current.timePlayed );  

		timePlayed.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
		score.text		= ""+ (int)scoreMan.baseScore;
		multiplier.text 	= ""+ (int)scoreMan.multiplier;
		total.text		= ""+ (int)scoreMan.total;
		best.text		= ""+ (int)highScore;
	}
}
