/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                    SocialConfiguration.cs                   </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    14-May-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
using UnityEngine;
using System.Collections.Generic;
using System;

namespace RoaringSnail.SocialPlatforms.LocalImpl
{
    //--------------------------------------------------------------
    /// <summary> Configuration data for the
    ///           local social platform </summary>
    //--------------------------------------
    public class SocialConfiguration : ScriptableObject
    {
        //--------------------------------------------------------------
        /// <summary> Leaderboard config data structure. </summary>
        //--------------------------------------
        [Serializable]
        public class LeaderboardsEntry
        {
            public string id;
            public string title;
            public string format;
        }



        //--------------------------------------------------------------
        #region Public settings
        //--------------------------------------
        public Texture2D defaultUserImage;
        public List<LeaderboardsEntry> leaderboards;
        #endregion



        //--------------------------------------------------------------
        /// <summary> Returns whether leaderboard with the
        ///           given <c>id</c> exists. </summary>
        /// <param name="id"> the id of the leaderboard </param>
        /// <returns> <c>true</c> if the leaderboard exists;
        ///           <c>false</c> otherwise </returns>
        //--------------------------------------
        public bool ContainsLeaderboard(string id)
        {
            LeaderboardsEntry entry = GetLeaderboardEntry(id);
            return (entry != null);
        }



        //--------------------------------------------------------------
        /// <summary> Returns the title of the leaderboard with
        ///           the given <c>id</c> </summary>
        /// <param name="id">the id of the leaderboard</param>
        /// <returns>the title of the leaderboard</returns>
        //--------------------------------------
        public string GetLeaderboardTitle(string id)
        {
            LeaderboardsEntry entry = GetLeaderboardEntry(id);

            if (entry != null)
            {
                return entry.title;
            }
            else
            {
                LeaderboardDoesNotExist(id);
                return null;
            }
        }



        //--------------------------------------------------------------
        /// <summary> Throws an exception if the leaderboard with the
        ///           given id does not exist. </summary>
        /// <param name="id">the id of the leaderboard</param>
        //--------------------------------------
        public void AssertLeaderboardExists(string id)
        {
            if (!ContainsLeaderboard(id))
                LeaderboardDoesNotExist(id);
        }



        //--------------------------------------------------------------
        /// <summary> Returns an internal leaderboard entry for the
        ///           leaderboard with the given <c>id</c>. </summary>
        /// <param name="id">the id of the leaderboard</param>
        /// <returns>the leaderoard entrie</returns>
        //--------------------------------------
        private LeaderboardsEntry GetLeaderboardEntry(string id)
        {
            return leaderboards.Find(entry => (entry.id == id));
        }



        //--------------------------------------------------------------
        /// <summary> An utility function to construct and throw 
        ///           an exception for inexistent leaderboards. </summary>
        /// <param name="id">the inexistent leaderboard id</param>
        //--------------------------------------
        private void LeaderboardDoesNotExist(string id)
        {
            throw new Exception("Leaderboard \"" + id + "\" does not exist");
        }
    }
}
