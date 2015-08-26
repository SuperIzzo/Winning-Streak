/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                    ScoringEventManager.cs                   </file> * 
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
namespace RoaringSnail.WinningStreak
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;



    public enum ScoringEventType
    {
        NONE = 0,
        PICKED_BALL,
        DODGE_TACKLE,
        HIT_PLAYER,
        WIGGLE,
        ZONE_WIGGLE,
        TOUCH_DOWN,
        SCORED_GOAL
    }



    //--------------------------------------------------------------
    /// <summary> 	Scoring event manager is an utility class that
    /// 			deals with "scoring events". </summary>
    /// <description> 
    /// Scoring events are certain events that happen in the game due 
    /// to the player's interaction with the game world. These events
    /// can be detected from anywhere but must be registered here.
    /// 
    /// ScoringEventManager is responsible for awarding players with
    /// score points and audio-visual feedback (such as commentary).
    /// </description>
    //--------------------------------------
    public class ScoringEventManager : MonoBehaviour
    {
        //..............................................................
        #region            //  PUBLIC TYPES  //
        //--------------------------------------------------------------
        ///<summary> Contains all data needed for scoring events. </summary>
        [System.Serializable]
        public class ScoreData
        {
            [System.Serializable]
            public struct Comment
            {
                public CommentatorEvent comment;
                public float chance;
                public float delay;
                public bool mustBeOn;
            }



            public enum EventIgnition
            {
                ONE_TIME,
                CONTINUOUS
            }



            public string description;
            public ScoringEventType scoringEvent;
            public EventIgnition ignition;
            public float baseScore;
            public float multPoints;
            public float hypeEffect;
            public Comment comment;
        }
        #endregion
        //......................................



        //..............................................................
        #region            //  PUBLIC SETTINGS  //
        //--------------------------------------------------------------
        [SerializeField, Tooltip
        (   "Database of different awardable scoring events"          )]
        //--------------------------------------
        private ScoreData[] _scoresList;
        #endregion
        //......................................



        //..............................................................
        #region            //  PRIVATE SETTINGS  //
        //--------------------------------------------------------------
        private Dictionary<ScoringEventType, ScoreData> scoresMap;
        private Dictionary<ScoringEventType, bool> ongoingScores;
        #endregion
        //......................................



        //..............................................................
        #region             //  METHODS  //
        //--------------------------------------------------------------
        /// <summary> Start callback. </summary>
        //--------------------------------------
        void Start()
        {
            // Create and populate the scores map
            scoresMap = new Dictionary<ScoringEventType, ScoreData>();
            ongoingScores = new Dictionary<ScoringEventType, bool>();

            foreach (ScoreData data in _scoresList)
            {
                scoresMap[data.scoringEvent] = data;
            }
        }



        //--------------------------------------------------------------
        /// <summary> Fires a scoring event. </summary>
        /// <description>
        /// This is how scoring events are reported. Other classes
        /// detect them and fire when the conditions are met.
        /// </description>
        //--------------------------------------
        public void Fire(ScoringEventType eventType, bool on = true)
        {
            if (scoresMap.ContainsKey(eventType))
            {
                Score score = Player.p1.score;
                ScoreData scoreData = scoresMap[eventType];

                if (scoreData.ignition == ScoreData.EventIgnition.ONE_TIME)
                {
                    score.baseScore += scoreData.baseScore;
                    score.comboBuilder += scoreData.multPoints;
                    Crowd.hype += scoreData.hypeEffect;
                }
                else
                {
                    bool prevOn = ongoingScores.ContainsKey(eventType) && ongoingScores[eventType];
                    ongoingScores[eventType] = on;

                    if (on && !prevOn)
                    {
                        StartCoroutine(ContinuousPoints(eventType));
                    }
                }

                // Comment queue
                if (scoreData.comment.comment != CommentatorEvent.NONE
                   && scoreData.comment.chance > Random.value)
                {
                    StartCoroutine(Comment(eventType, scoreData.comment));
                }
            }
        }



        //-------------------------------------------------------------
        /// <summary> A coroutine to update continous scoring. </summary>
        //--------------------------------------
        IEnumerator ContinuousPoints(ScoringEventType eventType)
        {
            Score score = Player.p1.score;
            ScoreData scoreData = scoresMap[eventType];

            while (ongoingScores[eventType])
            {
                yield return 0;

                // Add points per second
                score.baseScore += scoreData.baseScore * Time.deltaTime;
                score.comboBuilder += scoreData.multPoints * Time.deltaTime;
                Crowd.hype += scoreData.hypeEffect * Time.deltaTime;
            }
        }



        //-------------------------------------------------------------
        /// <summary> Delayed commentary. </summary>
        //-------------------------------------- 
        IEnumerator Comment(ScoringEventType eventType, ScoreData.Comment comment)
        {
            for (float time = comment.delay; time > 0; time -= Time.deltaTime)
            {
                yield return 0;
            }

            if (!comment.mustBeOn || ongoingScores[eventType])
            {
                Commentator.Comment(comment.comment);
            }
        }
        #endregion
        //......................................
    }
}