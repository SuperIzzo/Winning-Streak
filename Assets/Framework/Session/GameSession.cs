/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                        GameSession.cs                       </file> * 
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

public class GameSession : MonoBehaviour
{
	public static GameSession current
	{
		get
		{
			if( !_current )
			{
				_current = GameObject.FindObjectOfType<GameSession>();
			}
			
			return _current;
		}
	}
	private static GameSession _current;


	public float timePlayed {get; private set;}

	// Use this for initialization
	void Start ()
	{
		timePlayed = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timePlayed += Time.deltaTime;
	}
}
