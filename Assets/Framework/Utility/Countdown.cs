/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                            Timer.cs                         </file> * 
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
namespace RoaringSnail.WinningStreak
{
    using System;
    using System.Collections;
    using UnityEngine;


    [Serializable]
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary> An alarm clock that counts down 
    ///           and raises an event.                    </summary>
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-= 
    class Countdown
    {
        //..............................................................
        #region                   //  FIELDS  //
        //--------------------------------------------------------------
        [SerializeField, Tooltip
        (   "The alarm time (in seconds)."                            )]
        //--------------------------------------
        float  _setTime;


        //--------------------------------------------------------------
        [SerializeField, Tooltip
        (   "Whether this timer is paused."                           )]
        //--------------------------------------
        bool   _paused = false;


        //--------------------------------------------------------------
        /// <summary> Internal countdown time counter. </summary>
        //--------------------------------------
        float _currentTime = -1;
        #endregion
        //......................................



        //--------------------------------------------------------------
        /// <summary> An event called when the alarm goes off </summary>
        //--------------------------------------
        public EventHandler AlarmRaised;



        //..............................................................
        #region              //  PUBLIC PROPERTIES  //
        //--------------------------------------------------------------
        /// <summary> Gets or sets the default alarm time. </summary>
        //--------------------------------------
        public float setTime
        {
            get { return _setTime; }
            set { _setTime = value; }
        }



        //--------------------------------------------------------------
        /// <summary> Gets the current alarm time. </summary>
        //--------------------------------------
        public float currentTime { get { return _currentTime; } }



        //--------------------------------------------------------------
        /// <summary> Gets or sets a value indicating whether the alarm 
        ///           has been started.      </summary>
        /// <value>
        ///     <c>true</c> if the alarm has been started; 
        ///     <c>false</c> otherwise.
        /// </value>
        //--------------------------------------
        public bool isStarted
        {
            get { return _currentTime > 0; }
            set { if( value ) Start(); else Stop(); }
        }



        //--------------------------------------------------------------
        /// <summary> Gets or sets a value indicating whether the alarm
        ///           has been paused. </summary>
        /// <value> 
        ///     <c>true</c> if the alarm is paused; <c>false</c>c>otherwise.
        /// </value>
        //--------------------------------------
        public bool isPaused
        {
            get { return _paused; }
            set { if( value ) Pause(); else Resume(); }
        }



        //--------------------------------------------------------------
        /// <summary> Gets a value indicating whther the alarm has been
        ///           started and is not currently paused.    </summary>
        //--------------------------------------
        public bool isActive
        {
            get { return isStarted && !isPaused; }
        }
        #endregion
        //......................................



        //..............................................................
        #region                  //  METHODS  //
        //--------------------------------------------------------------
        /// <summary> Constructs a new alarm. </summary>
        //--------------------------------------
        public Countdown( float alarmTime = 0.0f )
        {
            _setTime = alarmTime;
        }



        //--------------------------------------------------------------
        /// <summary> Starts the alarm, if it is stopped. </summary>
        //--------------------------------------
        public void Start()
        {
            Start( setTime );
        }



        //--------------------------------------------------------------
        /// <summary> Starts the alarm, if it is stopped. </summary>
        /// <param name="resetTime"> The time to reset to. </param>
        //--------------------------------------
        public void Start( float resetTime )
        {
            if( !isStarted )
                Restart( resetTime );
        }



        //--------------------------------------------------------------
        /// <summary> Resets the internal counter. </summary>
        //--------------------------------------
        public void Restart()
        {
            Restart( setTime );
        }



        //--------------------------------------------------------------
        /// <summary> Resets the internal counter. </summary>
        /// <param name="resetTime"> The time to reset to. </param>
        //--------------------------------------
        public void Restart( float resetTime )
        {
            _currentTime = resetTime;
        }



        //--------------------------------------------------------------
        /// <summary> Pauses the alarm </summary>
        //--------------------------------------
        public void Pause()
        {
            _paused = true;
        }



        //--------------------------------------------------------------
        /// <summary> Upauses the alarm </summary>
        //--------------------------------------
        public void Resume()
        {
            _paused = false;
        }



        //--------------------------------------------------------------
        /// <summary> Stops the alarm. </summary>
        /// <param name="raiseAlarm"> 
        ///     Whether the alarm event should be raised
        /// </param>
        //--------------------------------------
        public void Stop( bool raiseAlarm = false )
        {
            if( _currentTime > 0 )
            {
                _currentTime = -1;
                if( raiseAlarm )
                {
                    OnAlarmRaised();
                }
            }
        }



        //--------------------------------------------------------------
        /// <summary>  Installs the alarm on a MonoBehaviour 
        ///            as a coroutine.                </summary>
        //--------------------------------------
        public void InstallCoroutine( MonoBehaviour component )
        {
            component.StartCoroutine( AlarmRoutine() );
        }



        //--------------------------------------------------------------
        /// <summary> The alarm update loop. </summary>
        //--------------------------------------
        private IEnumerator AlarmRoutine()
        {
            while( true )
            {
                yield return null;
                Update();
            }
        }



        //--------------------------------------------------------------
        /// <summary> A single alarm update step. </summary>
        //--------------------------------------
        private void Update()
        {
            if( !_paused && _currentTime > 0 )
            {
                _currentTime -= Time.deltaTime;
                if( _currentTime <= 0 )
                {
                    OnAlarmRaised();
                }
            }
        }



        //--------------------------------------------------------------
        /// <summary> Raises the alarm event </summary>
        //--------------------------------------
        private void OnAlarmRaised()
        {
            if( AlarmRaised != null )
                AlarmRaised( this, null );
        }
        #endregion
        //......................................
    }
}
