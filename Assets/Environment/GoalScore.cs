/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                         GoalScore.cs                        </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    14-Feb-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
using UnityEngine;
using System.Collections;

public class GoalScore : MonoBehaviour
{
	private static GoalScore lastGoalScore = null;
	private static float goalLockTime = 10.0f;
	private static float goalLockTimer = 0.0f;


	void Update()
	{
		// After 10 (or so) seconds unlock the goal post
		if( goalLockTimer>0 )
		{
			goalLockTimer -= Time.deltaTime;
			if( goalLockTimer<=0 )
			{
				lastGoalScore = null;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if( other.CompareTag(Tags.ball) )
		{
			// You cannot score two goals in a row
			// in the same goal post (you have to alternate)
			if( lastGoalScore!=this )
			{
				ScoringEventManager scoringEvent = GameSystem.scoringEvent;
				scoringEvent.Fire( ScoringEventType.SCORED_GOAL );

				lastGoalScore = this;
				goalLockTimer = goalLockTime;
			}
		}
	}

}

