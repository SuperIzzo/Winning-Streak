/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                  StreakerEventAnnouncer.cs                  </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    01-Dec-2014                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak
{
    using UnityEngine;


    [RequireComponent(typeof(BaseCharacterController))]
    public class StreakerEventAnnouncer : MonoBehaviour
    {
        BaseCharacterController controller;
        ScoringEventManager scoringEvent;

        bool pickedBallEvent = false;
        bool isDancingEvent = false;

        // Use this for initialization
        void Start()
        {
            controller = GetComponent<BaseCharacterController>();
            scoringEvent = GameSystem.scoringEvent;
        }

        // Update is called once per frame
        void Update()
        {
            if (scoringEvent && controller)
            {
                // Ball grab scoring
                if (controller.heldObject != null ^ pickedBallEvent)
                {
                    pickedBallEvent = !pickedBallEvent;

                    if (pickedBallEvent)
                        scoringEvent.Fire(ScoringEventType.PICKED_BALL);
                }

                // Dance scoring
                if (controller.isDancing ^ isDancingEvent)
                {
                    isDancingEvent = !isDancingEvent;
                    scoringEvent.Fire(ScoringEventType.WIGGLE, isDancingEvent);
                }
            }
        }
    }
}