/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                        GameSystem.cs                        </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    27-Jan-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
using UnityEngine;
using System.Collections;
using RoaringSnail.PersistenceSystems;

//--------------------------------------------------------------
/// <summary> A static global access to game systems. </summary>
//--------------------------------------
public class GameSystem : MonoBehaviour
{
	void Awake()
	{
		// Initialise the Persistence system...
		if( Persistence.Active == null )
		{
			Persistence.Active = new PrefsPersistence();
		}
	}


	//--------------------------------------------------------------
	#region		Internal variables
	//--------------------------------------
	private static TimeFlow 		_timeFlow;
	private static ScoringEventManager	_scoringEvent;
	private static DifficultyManager	_difficulty;
	private static SocialManager		_social;
	//private static AchievementsManager	_achievements;
	#endregion


	// Globally accessible readonly properties
	//--------------------------------------------------------------
	#region Public properties
	//--------------------------------------

	/// <summary> Gets a single instance to the 
	/// <see cref="TimeFlow"/>. </summary>
	/// <value> The time flow manager. </value>
	public static TimeFlow timeFlow
	{
		get
		{
			if( !_timeFlow )
			{
				_timeFlow = GameObject.FindObjectOfType<TimeFlow>();
			}

			return _timeFlow;
		}
	}

	/// <summary> Gets a single instance to the 
	/// <see cref="ScoringEventManager"/>. </summary>
	/// <value> The scoring event manager. </value>
	public static ScoringEventManager scoringEvent
	{
		get 
		{
			if( !_scoringEvent )
			{
				_scoringEvent = GameObject.FindObjectOfType<ScoringEventManager>();
			}
			
			return _scoringEvent;
		}
	}

	/// <summary> Gets a single instance to the 
	/// <see cref="AchievementsManager"/>. </summary>
	/// <value> The achievements manager. </value>
    //public static AchievementsManager achievements
    //{
    //    get 
    //    {
    //        //if( !_achievements )
    //        //{
    //        //    _achievements = GameObject.FindObjectOfType<AchievementsManager>();
    //        //}
			
    //        return _achievements;
    //    }
	//}

	/// <summary> Gets a single instance to the 
	/// <see cref="DifficultyManager"/>. </summary>
	/// <value> The difficulty manager. </value>
    public static DifficultyManager difficulty
    {
        get
        {
            if (!_difficulty)
            {
                _difficulty = GameObject.FindObjectOfType<DifficultyManager>();
            }

            return _difficulty;
        }
    }
	
	/// <summary> Gets a single instance to the 
	/// <see cref="SocialManager"/>. </summary>
	/// <value> The social manager. </value>
    public static SocialManager social
    {
        get 
        {
            if( !_social )
            {
                _social = GameObject.FindObjectOfType<SocialManager>();
            }
			
            return _social;
        }
    }
	#endregion
}
