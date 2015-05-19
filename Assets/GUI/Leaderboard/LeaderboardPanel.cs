/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                     LeaderboardPanel.cs                     </file> * 
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
namespace RoaringSnail.WinningStreak
{
    using System;
    using UnityEngine;
    using UnityEngine.SocialPlatforms;
    using System.Collections.Generic;

    //--------------------------------------------------------------
    /// <summary> Leaderboard GUI Panel script</summary>
    //--------------------------------------
    public class LeaderboardPanel : MonoBehaviour
    {
        //--------------------------------------------------------------
        /// <summary> An internal score data structure used for
        ///           storing intermediate scores. </summary>
        //--------------------------------------
        private class ScoreData
        {
            public string userName;
            public TimeSpan time;
            public long score;
            public long rank;
        }



        //--------------------------------------------------------------
        #region private state
        //--------------------------------------
        ScoreListing scoreListing;
        List<IScore> totalScores;
        List<IScore> times;
        List<ScoreData> scoreData;
        #endregion



        //--------------------------------------------------------------
        /// <summary> Initializes the leaderboard panel </summary>
        //--------------------------------------
        void Start()
        {
            scoreListing = GetComponentInChildren<ScoreListing>();
            UpdateTable();
        }



        //--------------------------------------------------------------
        /// <summary> Initiates the score table update </summary>
        /// <description>
        ///     This function will first fetch scores from
        ///     the SocialAPI server and then rebuild the GUI.
        /// </description>
        //--------------------------------------
        void UpdateTable()
        {
            if (Social.localUser.authenticated)
            {
                Social.LoadScores(LeaderboardID.TOTAL_SCORE, TotalScoresLoaded);
                Social.LoadScores(LeaderboardID.TIME, TimeLoaded);
            }
        }



        //--------------------------------------------------------------
        /// <summary> Callback invoked when the total scores table has
        ///           been loaded </summary>
        /// <param name="scores">The loaded scores</param>
        //--------------------------------------
        void TotalScoresLoaded(IScore[] scores)
        {
            if (scores != null)
                totalScores = new List<IScore>(scores);

            if (times != null && totalScores != null)
                BuildPanel();
        }



        //--------------------------------------------------------------
        /// <summary> Callback invoked when the times table has
        ///           been loaded </summary>
        /// <param name="scores">The loaded times</param>
        //--------------------------------------
        void TimeLoaded(IScore[] scores)
        {
            if (scores != null)
                times = new List<IScore>(scores);

            if (times != null && totalScores != null)
                BuildPanel();
        }



        //--------------------------------------------------------------
        /// <summary> Rebuilds the ScorePanel GUI </summary>
        //--------------------------------------
        void BuildPanel()
        {
            // Sort in descending order
            totalScores.Sort((a, b) => (int)(a.value - b.value));

            // Create scoreData from totalScores and time
            scoreData = new List<ScoreData>();
            foreach (IScore score in totalScores)
            {
                ScoreData data = ConstructScoreData(score.userID);
                scoreData.Add(data);
            }

            // Fill up the score listing
            for (int i = 0; i < scoreListing.numberOfRecords; i++)
            {
                if (i < scoreData.Count)
                {
                    // Note that index scoreListing[0] is the header
                    // 1 .. numberOfRecords is the actual score records
                    scoreListing[i + 1].userName = scoreData[i].userName;
                    scoreListing[i + 1].score = scoreData[i].score.ToString();
                    scoreListing[i + 1].rank = "#" + scoreData[i].rank;

                    TimeSpan t = scoreData[i].time;
                    scoreListing[i + 1].time = String.Format(
                        "{0:###00}:{1:00}:{2:00}",
                        t.Minutes, t.Seconds, t.Milliseconds / 100);

                }
                else // empty the GUI score lines
                {
                    scoreListing[i + 1].userName = "";
                    scoreListing[i + 1].time = "";
                    scoreListing[i + 1].score = "";
                    scoreListing[i + 1].rank = "";
                }
            }
        }



        //--------------------------------------------------------------
        /// <summary> An utility function to create ScoreData structure
        ///           from individual Leaderboards. </summary>
        /// <param name="id">The user id</param>
        /// <returns>A constructed ScoreData object</returns>
        //--------------------------------------
        ScoreData ConstructScoreData(string id)
        {
            ScoreData data = new ScoreData();
            data.userName = id;

            IScore total = totalScores.Find(score => score.userID == id);
            IScore time = times.Find(score => score.userID == id);

            if (total != null)
            {
                data.score = total.value;
                data.rank = total.rank;
            }

            if (time != null)
            {
                data.time = new TimeSpan(time.value);
            }

            return data;
        }
    }
}
