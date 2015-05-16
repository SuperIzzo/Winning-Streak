/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                         DanceZone.cs                        </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    05-Mar-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
using UnityEngine;
using System.Collections;

public class DanceZone: MonoBehaviour
{
	void OnTriggerStay(Collider other)
	{
		if( other.CompareTag(Tags.player) )
		{
			if(other.GetComponent<BaseCharacterController>().isDancing)
			{
				GameSystem.scoringEvent.Fire( ScoringEventType.ZONE_WIGGLE, true );
			}
			else
			{
				GameSystem.scoringEvent.Fire( ScoringEventType.ZONE_WIGGLE, false );
			}
		}
	}
}
