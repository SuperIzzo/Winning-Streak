/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                         HypeScore.cs                        </file> * 
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

    //--------------------------------------------------------------
    /// <summary> Hype score. </summary>
    //--------------------------------------
    [RequireComponent(typeof(Score))]
    public class HypeScore : MonoBehaviour
    {
        //--------------------------------------------------------------
        /// <summary> Reference to score </summary>
        //--------------------------------------
        private Score score;

        //--------------------------------------------------------------
        /// <summary> Initialises this instance. </summary>
        //--------------------------------------
        void Start()
        {
            score = GetComponent<Score>();
        }

        //--------------------------------------------------------------
        /// <summary> Updates this instance. </summary>
        //--------------------------------------
        void Update()
        {
            score.baseScore += Time.deltaTime * Crowd.hype;
        }
    }
}