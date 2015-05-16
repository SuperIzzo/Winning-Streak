/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                      ScoreCommentary.cs                     </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    04-Apr-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
using UnityEngine;
using System.Collections;

//--------------------------------------------------------------
/// <summary> Score based commentary handler. </summary>
//-------------------------------------- 
[RequireComponent(typeof(Score))]
public class ScoreCommentary : MonoBehaviour
{
	//--------------------------------------------------------------
	/// <summary> Reference to score </summary>
	//--------------------------------------
	private Score score;

	//--------------------------------------------------------------
	/// <summary> Initializes this instance </summary>
	//--------------------------------------
	void Awake()
	{
		score = GetComponent<Score>();
	}

	//--------------------------------------------------------------
	/// <summary> Handles the OnEnable callback. </summary>
	//--------------------------------------
	void OnEnable()
	{
		score.OnComboCompleted += OnComboCompleted;
	}

	//--------------------------------------------------------------
	/// <summary> Handles the OnDisable callback. </summary>
	//--------------------------------------
	void OnDisable()
	{
		score.OnComboCompleted -= OnComboCompleted;
	}

	//--------------------------------------------------------------
	/// <summary> Handles on combo completed events. 
	/// 	      Invokes commentaries. </summary>
	//--------------------------------------
	private void OnComboCompleted(Score score, float comboPoints)
	{
		CommentatorEvent pointsEvent = GetPtsEvent( comboPoints );
		Commentator.Comment( pointsEvent );
	}

	//--------------------------------------------------------------
	/// <summary> Returns an appropriate event for
	/// 	      the given points. </summary>
	/// <returns>The commentator event.</returns>
	/// <param name="pts">Score points</param>
	//--------------------------------------
	static CommentatorEvent GetPtsEvent( float pts )
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
}
