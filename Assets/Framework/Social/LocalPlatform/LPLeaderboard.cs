/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                       LPLeaderboard.cs                      </file> * 
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
using UnityEngine.SocialPlatforms;
using System.Collections.Generic;
using System;

using RoaringSnail.PersistenceSystems;


namespace RoaringSnail.SocialPlatforms.LocalImpl
{
    //--------------------------------------------------------------
    /// <summary> A local leadeboard implementation</summary>
    //--------------------------------------
    public class Leaderboard : ILeaderboard
    {
        //--------------------------------------------------------------
        #region Private references
        //--------------------------------------
        /// <summary> The social platform this board belongs to </summary>
        private SocialPlatform socialPlatform;


        //--------------------------------------------------------------
        /// <summary> Returns the persistance used for storing </summary>
        private IPersistence persistance
        {
            get { return socialPlatform.GetPersistance(); }
        }

        //--------------------------------------------------------------
        /// <summary> Returns the configuration </summary>
        private SocialConfiguration configuration
        {
            get { return socialPlatform.configuration; }
        }
        #endregion



        //--------------------------------------------------------------
        #region Private state
        //--------------------------------------
        /// <summary> A list of score data. </summary>
        List<Score> scoreData;
        #endregion



        //--------------------------------------------------------------
        #region Private properties
        //--------------------------------------
        private string boardKey { get { return "lp_LB:" + id; } }
        #endregion



        //--------------------------------------------------------------
        #region Public properties
        //--------------------------------------
        /// <summary> Gets or sets the identifier of the board. </summary>
        /// <value>The identifier.</value>
        public string id { get; set; }



        //--------------------------------------------------------------
        /// <summary> Returns the human readable 
        /// 	      title of the leader board. </summary>
        /// <value>The title.</value>
        public string title { get { return configuration.GetLeaderboardTitle(id); } }



        //--------------------------------------------------------------
        /// <summary> Gets a value indicating whether this
        /// 	      <see cref="LocalPlatform.Leaderboard"/> is loading.
        /// </summary>
        /// <value><c>true</c> if loading; otherwise, <c>false</c>.</value>
        public bool loading { get { throw new NotImplementedException(); } }



        //--------------------------------------------------------------
        /// <summary> Returns the score of the local user. </summary>
        /// <value>The local user score.</value>
        public IScore localUserScore { get { throw new NotImplementedException(); } }



        //--------------------------------------------------------------
        /// <summary> The scores in this leaderboard. </summary>
        /// <value>The scores.</value>
        public IScore[] scores
        {
            get
            {
                List<IScore> iScores = scoreData.ConvertAll(
                    scoreDatum => (IScore)scoreDatum
                );

                return iScores.ToArray();
            }
        }



        //--------------------------------------------------------------
        /// <summary> Returns the maximal range 
        /// 	      this leaderboard can handle. </summary>
        /// <value>The max range.</value>
        public uint maxRange { get { throw new NotImplementedException(); } }



        //--------------------------------------------------------------
        /// <summary> Gets or sets the range of this leaderboard. </summary>
        /// <value>The range.</value>
        public Range range
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }



        //--------------------------------------------------------------
        /// <summary> Gets or sets the time scope
        ///	      of this leaderboard. </summary>
        /// <value>The time scope.</value>
        public TimeScope timeScope
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }



        //--------------------------------------------------------------
        /// <summary> Gets or sets the user scope
        ///	      of this leaderboard. </summary>
        /// <value>The user scope.</value>
        public UserScope userScope
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        #endregion



        //--------------------------------------------------------------
        /// <summary> Initializes a new instance of the 
        /// 	      <see cref="LocalPlatform.Leaderboard"/> class.
        /// </summary>
        /// <param name="social">Social.</param>
        //--------------------------------------
        public Leaderboard(SocialPlatform social)
        {
            socialPlatform = social;
        }



        //--------------------------------------------------------------
        /// <summary> Loads the scores with regard
        /// 	      to the specified properties. </summary>
        /// <description> callback </description>
        /// <param name="callback">Callback.</param>
        //--------------------------------------
        public void LoadScores(System.Action<bool> callback)
        {
            bool boardExists = configuration.ContainsLeaderboard(id);

            if (boardExists)
            {
                if (persistance.HasKey(boardKey))
                {
                    scoreData = persistance.GetObject<List<Score>>(boardKey);
                }


                if (scoreData == null)
                {
                    scoreData = new List<Score>();
                }
            }

            if (callback != null)
                callback(boardExists);
        }



        //--------------------------------------------------------------
        /// <summary> Sets the user filter. </summary>
        /// <param name="userIDs">User identifiers.</param>
        //--------------------------------------
        public void SetUserFilter(string[] userIDs)
        {
            throw new NotImplementedException();
        }



        //--------------------------------------------------------------
        /// <summary>Sets the score for the <code>user</code>.</summary>
        /// <param name="user"> The user </param>
        /// <param name="date"> When the the score was set </param>
        /// <param name="score"> Score to report </param>
        //--------------------------------------
        public void SetScore(IUserProfile user, DateTime date, long score)
        {
            Score scoreObj = scoreData.Find(theScore => theScore.userID == user.id);

            // If the score for the user doesn't exist
            // create it and add it to the board
            if (scoreObj == null)
            {
                scoreObj = new Score();
                scoreData.Add(scoreObj);
            }

            // Fill info
            scoreObj.leaderboardID = this.id;
            scoreObj.userID = user.id;
            scoreObj.value = score;
            scoreObj.date = date;

            // Store the board
            SaveScores();
        }



        //--------------------------------------------------------------
        /// <summary> Stores the leaderboard locally. </summary>
        //--------------------------------------
        public void SaveScores()
        {
            persistance.SetObject(boardKey, scoreData);
        }
    }
}
