/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                       CommentaryDB.cs                       </file> * 
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
    using System.Collections.Generic;


    public class CommentaryDB : ScriptableObject
    {
        //--------------------------------------------------------------
        /// <summary> A commentator conversation queue. </summary>
        //-------------------------------------- 
        [System.Serializable]
        public class CommentatorQueue
        {
            public string name;
            public CommentatorEvent commentEvent;
            public List<AudioClip> clipQueue;
        }

        #region Public settings
        public List<CommentatorQueue> commentQueues;
        #endregion
    }
}