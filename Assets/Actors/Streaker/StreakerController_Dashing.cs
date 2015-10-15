/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                 StreakerController_Dashing.cs               </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    21-May-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak.Characters
{
    using System;
    using UnityEngine;


    public partial class StreakerController : IDashingCharacter
    {
        //..............................................................
        #region                  //  FIELDS  //
        //--------------------------------------------------------------
        [SerializeField, Tooltip
        (   "The speed which this character gains while running."     )]
        //--------------------------------------
        float _runSpeed = 7.0f;


        //--------------------------------------------------------------
        [SerializeField, Tooltip
        (   "The speed boost this character obtains when dashing."    )]
        //--------------------------------------
        float _dashSpeed        = 2.5f;


        //--------------------------------------------------------------
        [SerializeField, Tooltip
        (   "The duration of the dash boost."                         )]
        //--------------------------------------
        Countdown _dashDuration = new Countdown(1.0f);


        //--------------------------------------------------------------
        [SerializeField, Tooltip
        (   "The dash cooldown before it can be used again."          )]
        //--------------------------------------
        Countdown _dashCooldown = new Countdown(5.0f);


        private bool _isDashing;
        private bool _isRunning;
        #endregion
        //......................................



        //--------------------------------------------------------------
        /// <summary> Called when the character starts dashing. </summary>
        //--------------------------------------
        public event EventHandler StartedDashing;



        //--------------------------------------------------------------
        /// <summary> Called when the character stops dashing. </summary>
        //--------------------------------------
        public event EventHandler StoppedDashing;



        //..............................................................
        #region                 //  PROPERTIES  //
        //--------------------------------------------------------------
        /// <summary> Gets or sets a value indicating whether this
        /// <see cref="BaseCharacterController" /> is dashing. </summary>
        /// <value>
        ///   <c>true</c> if dashing; otherwise, <c>false</c>.</value>
        //--------------------------------------
        public bool isDashing
        {
            get { return _isDashing; }
            set { if( value ) Dash(); else StopDashing(); }
        }



        //--------------------------------------------------------------
        /// <summary> Gets or sets a value indicating whether this
        /// <see cref="StreakerController" /> is running. </summary>
        /// <value>
        ///   <c>true</c> if running; otherwise, <c>false</c>.</value>
        //--------------------------------------
        public bool isRunning { get { return _isRunning; } }



        //--------------------------------------------------------------
        /// <summary> Gets the modified movement
        ///           speed of the streaker </summary>
        //--------------------------------------
        public override float movementSpeed
        {
            get
            {
                float speed = base.movementSpeed;

                if( isRunning )
                {
                    speed = _runSpeed;
                }
                else if( isDashing )
                {
                    speed = _dashSpeed;
                }

                return speed;
            }
        }
        #endregion
        //......................................



        //..............................................................
        #region                  //  METHODS  //
        //--------------------------------------------------------------
        /// <summary> Sets dashing state for the charater </summary>
        //--------------------------------------
        public void Dash()
        {
            if( !isKnockedDown && !_isDashing )
            {
                if( _dashCooldown.isStarted )
                {
                    StartRunning();
                }
                else
                {
                    StartDashing();
                }

                _dashCooldown.Restart();
            }
        }



        //--------------------------------------------------------------
        /// <summary> The character starts dashing </summary>
        //--------------------------------------
        private void StartDashing()
        {
            StopRunning();
            _isDashing = true;
            _dashDuration.Restart();
        }



        //--------------------------------------------------------------
        /// <summary> Cancels the dashing state </summary>
        //--------------------------------------
        private void StopDashing()
        {
            _isDashing = false;
            _isRunning = false;
            _dashDuration.Stop();
        }



        //--------------------------------------------------------------
        /// <summary> The character starts running </summary>
        //--------------------------------------
        private void StartRunning()
        {
            StopDashing();
            _isRunning = true;
        }



        //--------------------------------------------------------------
        /// <summary> The character stops running </summary>
        //--------------------------------------
        private void StopRunning()
        {
            _isRunning = false;
        }



        //--------------------------------------------------------------
        /// <summary> Called when the streaker's dashing and 
        ///           the duration of the dash ends.       </summary>
        //--------------------------------------
        private void OnDashDurationEnded( object sender, EventArgs e )
        {
            StopDashing();
            StartRunning();
        }
        


        //--------------------------------------------------------------
        /// <summary> Initializes the dashing. </summary>
        //--------------------------------------
        partial void Setup_Dashing()
        {
            // Setup alarm callbacks 
            _dashDuration.AlarmRaised += OnDashDurationEnded;

            // Install timers
            _dashCooldown.InstallCoroutine( this );
            _dashDuration.InstallCoroutine( this );
        }



        //--------------------------------------------------------------
        /// <summary> Processes the dashing. </summary>
        //--------------------------------------
        partial void Process_Dashing()
        {
            // Cooldown (counts down only while walking)
            _dashCooldown.isPaused = _isDashing || _isRunning;
        }
        #endregion
        //......................................
    }
}
