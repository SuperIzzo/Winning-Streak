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
    using Characters;


    [RequireComponent(typeof(BaseCharacterController))]
    public class StreakerEventAnnouncer : MonoBehaviour
    {
        IThrowingCharacter   _thrower;
        IDancingCharacter    _dancer;
        ScoringEventManager scoringEvent;

        bool pickedBallEvent = false;
        bool isDancingEvent = false;

        // Use this for initialization
        void Start()
        {
            _thrower = GetComponent<IThrowingCharacter>();
            _dancer =  GetComponent<IDancingCharacter>();
            scoringEvent = GameSystem.scoringEvent;
        }

        // Update is called once per frame
        void Update()
        {
            if (scoringEvent && _thrower!=null)
            {
                // Ball grab scoring
                if (_thrower.heldObject != null ^ pickedBallEvent)
                {
                    pickedBallEvent = !pickedBallEvent;

                    if (pickedBallEvent)
                        scoringEvent.Fire(ScoringEventType.PICKED_BALL);
                }

                // Dance scoring
                if (_dancer.isDancing ^ isDancingEvent)
                {
                    isDancingEvent = !isDancingEvent;
                    scoringEvent.Fire(ScoringEventType.WIGGLE, isDancingEvent);
                }
            }
        }
    }
}