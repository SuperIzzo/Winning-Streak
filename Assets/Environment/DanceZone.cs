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
namespace RoaringSnail.WinningStreak
{
    using Characters;
    using UnityEngine;

    public class DanceZone : MonoBehaviour
    {
        void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(Tags.player))
            {
                if (other.GetComponent<IDancingCharacter>().isDancing)
                {
                    GameSystem.scoringEvent.Fire(ScoringEventType.ZONE_WIGGLE, true);
                }
                else
                {
                    GameSystem.scoringEvent.Fire(ScoringEventType.ZONE_WIGGLE, false);
                }
            }
        }
    }
}