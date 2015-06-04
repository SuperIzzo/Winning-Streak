/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                         Touchdown.cs                        </file> * 
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
namespace RoaringSnail.WinningStreak
{
    using Characters;
    using UnityEngine;


    public class Touchdown : MonoBehaviour
    {
        private static FactionID playerEndZone;

        private Faction endZoneFaction;

        // Use this for initialization
        void Start()
        {
            playerEndZone = FactionID.STREAKER;

            endZoneFaction = GetComponent<Faction>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            // A bunch of rules and conditions the collider has to satisfy: 
            // * firstly it must be a ball
            // * it must be owned by the player and the player must be
            //	 holding it while passing (be in full possesion)
            // * the player must be coming from the opposite end zone
            var ball = other.GetComponent<ThrowableObject>();
            if (ball != null && ball.CompareTag(Tags.ball))
            {
                BaseCharacterController owner = ball.owner;
                if (!ball.isThrown && owner.CompareTag(Tags.player))
                {
                    if (endZoneFaction.IsEnemy(playerEndZone))
                    {
                        TouchDown();
                    }
                }
            }
        }

        void TouchDown()
        {
            playerEndZone = endZoneFaction.id;
            GameSystem.scoringEvent.Fire(ScoringEventType.TOUCH_DOWN);
        }
    }
}