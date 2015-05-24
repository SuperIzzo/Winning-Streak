/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                 StreakerController_Dancing.cs               </file> * 
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


    public partial class StreakerController : IDancingCharacter
    {
        private bool _isDancing;


        public event EventHandler StartedDancing;
        public event EventHandler StoppedDancing;



        //--------------------------------------------------------------
        /// <summary> Gets or sets a value indicating whether this
        /// <see cref="BaseCharacterController" /> is dancing. </summary>
        /// <value>
        ///   <c>true</c> if dancing; otherwise, <c>false</c>.</value>
        //--------------------------------------
        public bool isDancing
        {
            get { return _isDancing; }
            set { if( value ) StartDancing(); else StopDancing(); }
        }



        //--------------------------------------------------------------
        /// <summary> Makes the streaker dance </summary>
        //--------------------------------------
        public void StartDancing()
        {
            if( !_isDancing && !isKnockedDown )
            {
                _isDancing = true;
                StopDashing();
                OnStartedDancing();
            }
        }



        //--------------------------------------------------------------
        /// <summary> Makes the streaker not dance </summary>
        //--------------------------------------
        public void StopDancing()
        {
            if( _isDancing )
            {
                _isDancing = false;
                OnStoppedDancing();
            }
        }



        //--------------------------------------------------------------
        /// <summary> Raises the dance start event </summary>
        //--------------------------------------
        protected virtual void OnStartedDancing()
        {
            if( StartedDancing != null )
                StartedDancing( this, null );
        }



        //--------------------------------------------------------------
        /// <summary> Raises the dance stop event </summary>
        //--------------------------------------
        protected virtual void OnStoppedDancing()
        {
            if( StoppedDancing != null )
                StoppedDancing( this, null );
        }
    }
}
