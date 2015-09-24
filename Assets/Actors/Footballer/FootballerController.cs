/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                    FootballerController.cs                  </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    20-May-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak.Characters
{
    using System;
    using UnityEngine;


    [AddComponentMenu( "Winning Streak/Character/Footballer Controller" )]
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary> A fooballer character controller. </summary>
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-= 
    public class FootballerController : BaseCharacterController,
                                        ITacklingCharacter,
                                        ITackleAnimationListener
    {
        //--------------------------------------------------------------
        [SerializeField, Tooltip
        (   "The speed at which this character tackles."              )]
        //--------------------------------------
        float _tackleSpeed = 4.0f;



        //--------------------------------------------------------------
        /// <summary> Called when the footballer has tackled </summary>
        //--------------------------------------
        public event EventHandler Tackled;



        //--------------------------------------------------------------
        /// <summary> Gets or sets a value indicating whether this
        /// <see cref="BaseCharacterController" /> is tackling. </summary>
        /// <value>
        ///   <c>true</c> if is tackling; otherwise, <c>false</c>.</value>
        //--------------------------------------
        public bool isTackling { get; set; }



        //--------------------------------------------------------------
        /// <summary> Sets the tackling state for the charater </summary>
        /// <param name="tackle">tackling state</param>
        //--------------------------------------
        public void Tackle()
        {
            if( !isKnockedDown )
            {
                isTackling = true;
            }
        }



        //--------------------------------------------------------------
        /// <summary> Updates the footballer. </summary>
        //--------------------------------------
        protected override void Update()
        {
            base.Update();
            ProcessTackling();
        }



        //--------------------------------------------------------------
        /// <summary> Processes the tackling. </summary>
        //--------------------------------------
        private void ProcessTackling()
        {
            if( isTackling && !isKnockedDown )
            {
                transform.position += transform.forward * (_tackleSpeed * Time.deltaTime);
            }
        }



        //--------------------------------------------------------------
        /// <summary> Handles tackled event. </summary>
        //--------------------------------------
        protected virtual void OnTackled()
        {
            if( Tackled != null )
                Tackled( this, null );
        }



        //--------------------------------------------------------------
        /// <summary> Process the movement. </summary>
        //--------------------------------------
        protected override void ProcessMovement()
        {
            if( !isTackling )
            {
                base.ProcessMovement();
            }
        }



        //--------------------------------------------------------------
        /// <summary> Process the turning. </summary>
        //--------------------------------------
        protected override void ProcessTurning()
        {
            if( !isTackling )
            {
                base.ProcessTurning();
            }
        }


        public void OnTackleAnimationExit()
        {
            OnTackled();
            KnockDown();                        
            isTackling = false;                        
        }
    }

}