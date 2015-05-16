/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                            HUD.cs                           </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    15-May-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
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
		Score score = Player.p1.score;

		scoreText.text = "SCORE: " + Mathf.FloorToInt(score.baseScore) +
				     "\tx" + Mathf.FloorToInt(score.multiplier);

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
