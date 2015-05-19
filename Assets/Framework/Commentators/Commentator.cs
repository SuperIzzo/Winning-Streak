/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                        Commentator.cs                       </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    26-Mar-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak
{
    using UnityEngine;

    //--------------------------------------------------------------
    /// <summary> An enumerationn of all commentator event. </summary>
    //--------------------------------------
    public enum CommentatorEvent
    {
        NONE = 0,
        RANDOM,
        PICKED_BALL,
        DODGE_TACKLE,
        GAME_START,
        GAME_OVER,
        HIT_PLAYER,
        WIGGLE,
        TOUCH_DOWN,
        PTS_1,
        PTS_2,
        PTS_3,
        PTS_4,
        PTS_5,
        PTS_6,
        PTS_7,
        PTS_8,
        PTS_9,
        PTS_10,
        SCORED_GOAL
    }

    //--------------------------------------------------------------
    /// <summary> Commentator. </summary>
    //--------------------------------------
    public static class Commentator
    {
        //-------------------------------------------------------------
        /// <summary> This is the currently active
        ///           Commentator. </summary>
        /// <value>The active commentator.</value>
        //--------------------------------------
        public static CommentatorSystem Active
        {
            get
            {
                if (_active == null)
                {
                    _active = GameObject.FindObjectOfType<CommentatorSystem>();
                }

                return _active;
            }

            set
            {
                _active = value;
            }
        }
        private static CommentatorSystem _active;


        //--------------------------------------------------------------
        #region Public properties
        //--------------------------------------
        /// <summary> Returns the time since the last comment.</summary>
        public static float timeSinceLastComment
        {
            get
            {
                CheckActive();
                return Active.timeSinceLastComment;
            }
        }
        #endregion

        //--------------------------------------------------------------
        /// <summary> Play a random comment of the specified 
        /// <see cref="CommentatorEvent"/> </summary>
        /// <param name="evt">the commentator event.</param>
        /// <returns> Returns <c>true</c> if a comment was successfully 
        /// queued; Otherwise <c>false</c>.</returns>
        //--------------------------------------
        public static bool Comment(CommentatorEvent evt)
        {
            CheckActive();
            return Active.Comment(evt);
        }


        //-------------------------------------------------------------
        /// <summary> Tests if the Active instance is valid </summary>
        //--------------------------------------
        private static void CheckActive()
        {
            if (Active == null)
                throw new MissingReferenceException(
                                "Commentator.Active is not set to a valid " +
                                "instance.\nTry adding a CommentatorSystem " +
                                "component to the scene.");
        }
    }
}