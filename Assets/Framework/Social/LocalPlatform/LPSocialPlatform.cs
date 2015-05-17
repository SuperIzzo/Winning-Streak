/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                     LPSocialPlatform.cs                     </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    04-Feb-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections.Generic;

using RoaringSnail.PersistenceSystems;

namespace RoaringSnail.SocialPlatforms.LocalImpl
{
    //--------------------------------------------------------------
    /// <summary> Social platform. </summary>
    /// <description> This is a local (offline) implementation
    /// of the ISocialPlatform interface. It is mainly used for
    /// testing purposes.
    /// </description>
    //--------------------------------------
    public class SocialPlatform : ISocialPlatform
    {
        private class LeaderboardLoadResponse
        {
            private ILeaderboard leaderboard;
            System.Action<IScore[]> callback;

            public LeaderboardLoadResponse(ILeaderboard theLeaderboard,
                                           System.Action<IScore[]> theCallback)
            {
                leaderboard = theLeaderboard;
                callback = theCallback;
            }

            public void OnLeaderboardLoaded(bool loaded)
            {
                if (loaded)
                {
                    callback(leaderboard.scores);
                }
                else
                {
                    Debug.LogWarning("Could not load leadeboard\""
                                       + leaderboard.id + "\"");
                }
            }

        }



        //--------------------------------------------------------------
        #region Private state
        //--------------------------------------
        private static SocialPlatform _instance;
        private LocalUser _localUser;
        private SocialConfiguration _configuration;
        private Dictionary<string, Leaderboard> _leaderboards;
        #endregion



        //--------------------------------------------------------------
        /// <summary> Returns the local user of the social system. </summary>
        /// <value>The local user.</value>
        //--------------------------------------
        public ILocalUser localUser
        {
            get
            {
                if (_localUser == null)
                {
                    _localUser = new LocalUser("Local fellow");
                }

                return _localUser;
            }
        }



        //--------------------------------------------------------------
        /// <summary> Returns the setup of the social system. </summary>
        /// <value>The local user.</value>
        //--------------------------------------
        public SocialConfiguration configuration { get { return _configuration; } }



        //--------------------------------------------------------------
        /// <summary> Activates the the local social platform. </summary>
        /// <param name="data"> Setup data </param>
        //--------------------------------------
        public static void Activate(SocialConfiguration data)
        {
            if (_instance == null)
            {
                _instance = new SocialPlatform(data);
            }

            Social.Active = _instance;
        }



        //--------------------------------------------------------------
        /// <summary> Creates a new instance of the 
        /// 	      LocalPlatform.SocialPlatform class. </summary>
        /// <param name="data"> Setup data </param>
        //--------------------------------------
        public SocialPlatform(SocialConfiguration data)
        {
            _configuration = data;
            _leaderboards = new Dictionary<string, Leaderboard>();
        }



        //--------------------------------------------------------------
        /// <summary> Authenticates the local user calls the provided
        /// 	      delegate.</summary>
        /// <param name="user">User.</param>
        /// <param name="callback">Callback.</param>
        //--------------------------------------
        public void Authenticate(ILocalUser user, System.Action<bool> callback)
        {
            user.Authenticate(callback);

            Debug.LogWarning("SocialPlatform.Authenticate not implemented");
            //Application.persistentDataPath
        }



        //--------------------------------------------------------------
        /// <summary> Loads the user profiles and calls back.</summary>
        /// <param name="userIds">User identifiers.</param>
        /// <param name="callback">Callback.</param>
        //--------------------------------------
        public void LoadUsers(string[] userIds, System.Action<IUserProfile[]> callback)
        {
            throw new System.NotImplementedException();
        }



        //--------------------------------------------------------------
        /// <summary> Loads the user profiles for the friends
        /// 	      of the specified user. </summary>
        /// <param name="user">User.</param>
        /// <param name="callback">Callback.</param>
        //--------------------------------------
        public void LoadFriends(ILocalUser user, System.Action<bool> callback)
        {
            user.LoadFriends(callback);
        }



        //--------------------------------------------------------------
        /// <summary> Loads the scores from the specified board. </summary>
        /// <param name="boardID">Board.</param>
        /// <param name="callback">Callback.</param>
        //--------------------------------------
        public void LoadScores(string boardID, System.Action<IScore[]> callback)
        {
            ILeaderboard board = CreateLeaderboard();
            board.id = boardID;

            var loadResponse = new LeaderboardLoadResponse(board, callback);

            board.LoadScores(loadResponse.OnLeaderboardLoaded);
        }



        //--------------------------------------------------------------
        /// <summary> Loads the scores from the specified board. </summary>
        /// <param name="board">Board.</param>
        /// <param name="callback">Callback.</param>
        //--------------------------------------
        public void LoadScores(ILeaderboard board, System.Action<bool> callback)
        {
            board.LoadScores(callback);
        }



        //--------------------------------------------------------------
        /// <summary> Returns whether the specified board is loading </summary>
        /// <returns> <c>true</c>, if loading the board is currently 
        ///           loading, <c>false</c> otherwise.</returns>
        /// <param name="board">Board.</param>
        //--------------------------------------
        public bool GetLoading(ILeaderboard board)
        {
            return board.loading;
        }



        //--------------------------------------------------------------
        /// <summary> Loads the achievements and calls back. </summary>
        /// <param name="callback">Callback.</param>
        //--------------------------------------
        public void LoadAchievements(System.Action<IAchievement[]> callback)
        {
            throw new System.NotImplementedException();
        }


		
        //--------------------------------------------------------------
        /// <summary> Loads the achievement descriptions
        /// 	      and calls back. </summary>
        /// <param name="callback">Callback.</param>
        //--------------------------------------
        public void LoadAchievementDescriptions(System.Action<IAchievementDescription[]> callback)
        {
            throw new System.NotImplementedException();
        }



        //--------------------------------------------------------------
        /// <summary> Creates a new leader board </summary>
        /// <returns>The leaderboard.</returns>
        //--------------------------------------
        public ILeaderboard CreateLeaderboard()
        {
            return new Leaderboard(this);
        }



        //--------------------------------------------------------------
        /// <summary> Creates a new achievement. </summary>
        /// <returns>The achievement.</returns>
        //--------------------------------------
        public IAchievement CreateAchievement()
        {
            throw new System.NotImplementedException();
        }



        //--------------------------------------------------------------
        /// <summary> Reports the user's score. And updates the
        ///           specified board if necessary. </summary>
        /// <param name="score">Score.</param>
        /// <param name="board">Board.</param>
        /// <param name="callback">Callback.</param>
        //--------------------------------------
        public void ReportScore(long score, string boardID, System.Action<bool> callback)
        {
            Leaderboard board = GetLeaderboard(boardID);
            board.SetScore(localUser, System.DateTime.Now, score);

            if (callback != null)
                callback(true);
        }



        //--------------------------------------------------------------
        /// <summary> Reports the progress on an achievement </summary>
        /// <param name="id">The achievement identifier.</param>
        /// <param name="progress">Progress.</param>
        /// <param name="callback">Callback.</param>
        //--------------------------------------
        public void ReportProgress(string id, double progress, System.Action<bool> callback)
        {
            throw new System.NotImplementedException();
        }



        //--------------------------------------------------------------
        /// <summary> Shows the leaderboard UI </summary>
        //--------------------------------------
        public void ShowLeaderboardUI()
        {
            throw new System.NotImplementedException();
        }



        //--------------------------------------------------------------
        /// <summary> Shows the achievements UI. </summary>
        //--------------------------------------
        public void ShowAchievementsUI()
        {
            throw new System.NotImplementedException();
        }



        //--------------------------------------------------------------
        /// <summary> Returns the persistance method used to store
        ///           data locally. </summary>
        /// <returns> the persistance used by SocialPlatform </returns>
        //--------------------------------------
        public IPersistence GetPersistance()
        {
            return Persistence.Active;
        }



        //--------------------------------------------------------------
        /// <summary> Returns the leaderboard associated
        ///           with the given id. </summary>
        /// <param name="id"> the id of the board </param>
        /// <returns> the leaderboard instance </returns>
        //--------------------------------------
        private Leaderboard GetLeaderboard(string id)
        {
            if (!_leaderboards.ContainsKey(id))
            {
                configuration.AssertLeaderboardExists(id);

                Leaderboard board = new Leaderboard(this);
                board.id = id;
                board.LoadScores(null);
                _leaderboards[id] = board;
            }

            return _leaderboards[id];
        }
    }
}