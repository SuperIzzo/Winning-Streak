/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                         TimeFlow.cs                         </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    03-Mar-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak
{
    using UnityEngine;


    //-------------------------------------------------------------
    /// <summary> Time flow - controls the flow of time. </summary>
    //-------------------------------------- 
    public class TimeFlow : MonoBehaviour
    {

        //--------------------------------------------------------------
        #region  Public properties
        //--------------------------------------
        /// <summary> Gets or sets of the time flow is slowed down. </summary>
        /// <value><c>true</c> if is slowed; otherwise, <c>false</c>.</value>
        public bool isSlowed
        {
            get { return _isSlowed; }
            set { _isSlowed = value; UpdateTimeScale(); }
        }

        /// <summary> Gets or sets of the time flow is stopped. </summary>
        /// <value><c>true</c> if is paused; otherwise, <c>false</c>.</value>
        public bool isStopped
        {
            get { return _isStopped; }
            set { _isStopped = value; UpdateTimeScale(); }
        }
        #endregion


        //--------------------------------------------------------------
        #region Private state
        //--------------------------------------
        private bool _isSlowed = false;
        private bool _isStopped = false;
        #endregion


        //-------------------------------------------------------------
        /// <summary> Sets the correct time scale based on 
        ///           properties. </summary>
        //--------------------------------------
        void UpdateTimeScale()
        {
            float timeScale = 1.0f;

            if (_isStopped)
                timeScale = 0.0f;
            else if (_isSlowed)
                timeScale = 0.2f;

            Time.timeScale = timeScale;
        }
    }
}
