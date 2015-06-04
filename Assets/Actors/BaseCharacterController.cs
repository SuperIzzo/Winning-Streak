/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                  BaseCharacterController.cs                 </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    28-Nov-2014                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak.Characters
{
    using System;
    using UnityEngine;


 
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary>  Base character controller encapsulates the core 
    ///            game logic of a game characters.          </summary>
    /// <description> 
    ///     This class is purely mechanical. It maintains 
    ///     the character's state and manages it internally.
    ///     In MVC terms this is the Model.
    /// </description>
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-= 
    public abstract class BaseCharacterController : MonoBehaviour,
                                                    IMobileCharacter,
                                                    IKnockableCharacter
    {
        private static readonly float turningThreshold = 0.1f;



        //..............................................................
        #region            //  INSPECTOR SETTINGS  //
        //--------------------------------------------------------------
        [SerializeField, Tooltip
        (   "This character's base movement speed"                    )]
        //--------------------------------------
        float _baseMovementSpeed = 4.5f;


        //--------------------------------------------------------------
        [SerializeField, Tooltip
        (   "This character's turning speed"                          )]
        //--------------------------------------
        float _turningSpeed = 6.0f;
        

        //--------------------------------------------------------------
        [SerializeField, Tooltip
        (   "The minimal revival time. Use negative range " +
            "to disable revive."                                      )]
        //--------------------------------------
        MinMax _reviveTime = new MinMax{  min = 10.0f, max = 15.0f  };
        #endregion
        //......................................



        //..............................................................
        #region            //  PRIVATE STATE  //
        //--------------------------------------------------------------
        private bool _isKnockedDown;
        private float _reviveTimer;
        #endregion
        //......................................



        public event EventHandler KnockedDown;
        public event EventHandler Revived;



        //..............................................................
        #region              //  PUBLIC PROPERTIES  //
        //--------------------------------------------------------------
        /// <summary> Gets or sets a value indicating whether this 
        /// <see cref="BaseCharacterController"/> is knocked down. </summary>
        /// <value><c>true</c> if knocked down; otherwise, <c>false</c></value>
        //--------------------------------------
        public bool isKnockedDown
        {
            get { return _isKnockedDown; }
            set { if( value ) KnockDown(); else Revive(); }
        }



        //--------------------------------------------------------------
        /// <summary> Returns the relative velocity (readonly). </summary>
        /// <value> The relative velocity. </value>
        //--------------------------------------
        public Vector2 relativeVelocity { get; private set; }



        //--------------------------------------------------------------
        /// <summary> Returns the look direction (readonly). </summary>
        /// <value> The look direction. </value>
        //--------------------------------------
        public Vector3 lookDirection { get; private set; }



        //--------------------------------------------------------------
        /// <summary> Gets or sets the character base movement speed.</summary>
        /// <value> The base movement speed. </value>
        //--------------------------------------
        public float baseMovementSpeed
        {
            get { return _baseMovementSpeed; }
            set { _baseMovementSpeed = value; }
        }



        //--------------------------------------------------------------
        /// <summary> Gets the character movement speed 
        ///           after modifications.          </summary>
        /// <value> The movement speed. </value>
        //--------------------------------------
        public virtual float movementSpeed
        {
            get { return baseMovementSpeed; }
        }
        #endregion
        //......................................



        //..............................................................
        #region                  //  METHODS  //
        //--------------------------------------------------------------
        /// <summary> Knocks down the character. </summary>
        //--------------------------------------
        public void KnockDown()
        {
            if( !_isKnockedDown )
            {
                _isKnockedDown = true;
                _reviveTimer = _reviveTime.Random();
                OnKnockedDown();
            }
        }



        //--------------------------------------------------------------
        /// <summary> Revives a knocked down character. </summary>
        //--------------------------------------
        public void Revive()
        {
            if( _isKnockedDown )
            {
                _reviveTimer = -1;
                _isKnockedDown = false;
                OnRevived();
            }
        }



        //--------------------------------------------------------------
        /// <summary> Move in the specified direction with the set
        /// relative speed. </summary>
        /// <param name="dir"> relative normalized velocity </param>
        //--------------------------------------
        public void Move( Vector2 vel, bool turn = true )
        {
            relativeVelocity = vel;

            if( turn )
            {
                Turn( vel );
            }
        }



        //--------------------------------------------------------------
        /// <summary> Turn in the specified direction. </summary>
        /// <param name="dir">The look direction.</param>
        //--------------------------------------
        public void Turn( Vector2 dir )
        {
            if( dir.magnitude >= turningThreshold )
            {
                lookDirection = new Vector3( dir.x, 0, dir.y );
                lookDirection.Normalize();
            }
        }



        //--------------------------------------------------------------
        /// <summary> Fires KnockedDown event </summary>
        //--------------------------------------
        protected virtual void OnKnockedDown()
        {
            if( KnockedDown != null )
                KnockedDown( this, null );
        }



        //--------------------------------------------------------------
        /// <summary> Fires Revived event </summary>
        //--------------------------------------
        protected virtual void OnRevived()
        {
            if( Revived != null )
                Revived( this, null );
        }



        //--------------------------------------------------------------
        /// <summary> Update this instance. </summary>
        //--------------------------------------
        protected virtual void Update()
        {
            if( !isKnockedDown )
            {
                ProcessMovement();
                ProcessTurning();
            }
            else
            {
                ProcessRevival();
            }
        }



        //--------------------------------------------------------------
        /// <summary> Processes the movement. </summary>
        //--------------------------------------
        private void ProcessMovement()
        {
            float speed = movementSpeed;

            // Normal movement
            var moveVel = new Vector3(  relativeVelocity.x,
                                        0,
                                        relativeVelocity.y );

            moveVel *= speed * Time.deltaTime;

            // Final result
            transform.position = transform.position + moveVel;
        }



        //--------------------------------------------------------------
        /// <summary> Processes the turning. </summary>
        //--------------------------------------
        private void ProcessTurning()
        {
            if( lookDirection.magnitude > 0 )
            {
                Quaternion targetRot =
                    Quaternion.LookRotation( lookDirection, Vector3.up );

                // Smooth transitioning for rotation, 
                // also makes the rotation and movement more human like.
                transform.rotation =
                    Quaternion.Slerp(   transform.rotation, 
                                        targetRot,
                                        Time.deltaTime * _turningSpeed );
            }
        }



        //--------------------------------------------------------------
        /// <summary> Processes the revival of a knocked down character. </summary>
        //--------------------------------------
        private void ProcessRevival()
        {
            if( _reviveTimer > 0 )
            {
                _reviveTimer -= Time.deltaTime;

                if( _reviveTimer <= 0 )
                {
                    Revive();
                }
            }
        }
        #endregion
        //......................................
    }
}
