/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                          EndGame.cs                         </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    03-Dec-2014                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak
{
    using Characters;
    using UnityEngine;
    using System;

    // TODO: This file reaks of smelly code, it needs urgent sanitization

    public class EndGame : MonoBehaviour
    {
        private BaseCharacterController player;
        public GUIWindow endGameDialogue;
        public GUIWindow hud;

        private bool announced = false;
        private float endGameDialogCountdown = 0.0f;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (player)
            {
                if (endGameDialogCountdown > 0)
                {
                    endGameDialogCountdown -= Time.deltaTime;
                    if (endGameDialogCountdown <= 0)
                    {
                        endGameDialogue.Show();
                    }
                }
                else if (player.isKnockedDown)
                {
                    if (!announced)
                    {
                        announced = true;

                        hud.Hide();
                        endGameDialogCountdown = 2.0f;
                        //endGameDialogue.Show();

                        Score score = Player.p1.score;
                        float highScore = Persistence.GetFloat("HighScore", 0);

                        score.enabled = false;
                        GameSession.current.enabled = false;

                        if (score.total > highScore)
                        {
                            highScore = score.total;
                            TimeSpan time = TimeSpan.FromSeconds(GameSession.current.timePlayed);
                            Persistence.SetFloat("HighScore", highScore);

                            Social.ReportScore((long)highScore, LeaderboardID.TOTAL_SCORE, null);
                            Social.ReportScore((long)time.Ticks, LeaderboardID.TIME, null);
                        }



                        Commentator.Comment(CommentatorEvent.GAME_OVER);
                    }
                }
                else
                {
                    announced = false;
                }
            }
            else
            {
                player = Player.p1.characterController;
            }
        }
    }
}