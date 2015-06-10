/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                        PlayerInput.cs                       </file> * 
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
    using UnityEngine;



    [AddComponentMenu( "Winning Streak/Character/Player Input", 100 )]
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary> Player input that controls a character. </summary>
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    public class PlayerInput : MonoBehaviour
    {
        //..............................................................
        #region            //  PRIVATE CONSTANTS  //
        //--------------------------------------------------------------
        private const float axesDeadzone = 0.1f;
        private const float slomoDeathDuration = 2;
        #endregion
        //......................................



        //..............................................................
        #region            //  PRIVATE REFERENCES  //
        //--------------------------------------------------------------
        private IKnockableCharacter _knockable;
        private IMobileCharacter    _mover;
        private IDancingCharacter   _dancer;
        private IThrowingCharacter  _thrower;
        private IDashingCharacter   _dasher;
        #endregion
        //......................................



        //..............................................................
        #region             //  PRIVATE STATE  //
        //--------------------------------------------------------------
        private Vector2 _controlVector;
        private bool _isDancing;
        private bool _isDashing;
        private bool _isGrabDown;
        private bool _isGrabUp;
        private bool _isSlowMoDown;
        private bool _isPauseDown;
        private bool _isScreenShotDown;

        private bool _isScreenshotAxisInUse = false;
        private bool _handledDeathSlowMo = false;
        private float _slowMoDeathRestoreTimer = -1;
        #endregion
        //......................................



        //..............................................................
        #region               //  METHODS  //
        //--------------------------------------------------------------
        /// <summary> Initiates the PlayerInput. </summary>
        //--------------------------------------
        protected void Start()
        {
            _knockable  = GetComponent<IKnockableCharacter>();
            _mover      = GetComponent<IMobileCharacter>();
            _dancer     = GetComponent<IDancingCharacter>();
            _thrower    = GetComponent<IThrowingCharacter>();
            _dasher     = GetComponent<IDashingCharacter>();
        }



        //--------------------------------------------------------------
        /// <summary> Updates the PlayerInput. </summary>
        //--------------------------------------
        protected void Update()
        {
            // Get Input            
            GetInput();
            UpdateCharacter();
        }



        //--------------------------------------------------------------
        /// <summary> Retrieves input from devices and filters it. </summary>
        //--------------------------------------
        private void GetInput()
        {
            GetRawInput();
            FilterInput();
        }



        //--------------------------------------------------------------
        /// <summary> Retrieves "raw" input from devices. </summary>
        /// <remarks>
        ///     Unity filters the input but it's raw in a sense
        ///     that there's no custom game custom logic that filters it.
        /// </remarks>
        //--------------------------------------
        private void GetRawInput()
        {
            float x             = Input.GetAxis( InputControls.horizontal );
            float y             = Input.GetAxis( InputControls.vertical);
            _controlVector      = new Vector2( x, y );
            _isDancing          =Input.GetButton( InputControls.wiggle );
            _isDashing          =Input.GetButton( InputControls.dash );
            _isGrabDown         =Input.GetButtonDown( InputControls.grab );
            _isGrabUp           =Input.GetButtonUp( InputControls.grab );
            _isSlowMoDown       =Input.GetButtonDown( InputControls.sloMo );
            _isPauseDown        =Input.GetButtonDown( InputControls.pause );
            _isScreenShotDown   =Input.GetButtonDown( InputControls.screenshot );

            // Special case for dashDown as XBox triggers are axes
            _isDashing |= (Input.GetAxis( InputControls.dash ) > 0.5f);

            // And another special case for screenshots
            if( Input.GetAxis( InputControls.screenshot ) > 0.5f )
            {
                if( !_isScreenshotAxisInUse )
                {
                    _isScreenshotAxisInUse = true;
                    _isScreenShotDown = true;
                }
            }
            else
            {
                _isScreenshotAxisInUse = false;
            }
        }



        //--------------------------------------------------------------
        /// <summary> Applies custom filters to the input. </summary>
        //--------------------------------------
        private void FilterInput()
        {
            _controlVector = FilterInputAxes( _controlVector );
        }



        //--------------------------------------------------------------
        /// <summary> Applies updates to the character in question. </summary>
        //--------------------------------------
        private void UpdateCharacter()
        {
            UpdateMover();
            UpdateDancer();
            UpdateDasher();
            UpdateThrower();
            UpdateSloMo();
            UpdateScreenshots();
        }



        //--------------------------------------------------------------
        /// <summary> Updates the mover character part.  </summary>
        //--------------------------------------
        private void UpdateMover()
        {
            if( _mover!=null )
            {
                _mover.Move( _controlVector );
            }
        }



        //--------------------------------------------------------------
        /// <summary> Updates the dancer character part.  </summary>
        //--------------------------------------
        private void UpdateDancer()
        {
            if( _dancer!=null )
            {
                if( _isDancing )
                {
                    _dancer.StartDancing();
                }
                else
                {
                    _dancer.StopDancing();
                }
            }
        }



        //--------------------------------------------------------------
        /// <summary> Updates the dasher character part.  </summary>
        //--------------------------------------
        private void UpdateDasher()
        {
            if( _dasher!=null )
            {
                _dasher.isDashing = _isDashing;
            }
        }



        //--------------------------------------------------------------
        /// <summary> Updates the thrower character part.  </summary>
        //--------------------------------------
        private void UpdateThrower()
        {
            if( _thrower!=null )
            {
                if( _isGrabDown )
                {
                    if( _thrower.heldObject )
                    {
                        _thrower.ChargeThrow();
                    }
                    else
                    {
                        _thrower.Grab();
                    }
                }

                if( _isGrabUp && _thrower.isCharging )
                {
                    _thrower.Throw();
                }
            }
        }



        //--------------------------------------------------------------
        /// <summary> Updates the slow-motion (non-character).  </summary>
        //--------------------------------------
        private void UpdateSloMo()
        {
            TimeFlow timeFlow = GameSystem.timeFlow;
            if( timeFlow )
            {
                bool slow = timeFlow.isSlowed;

                if( !_knockable.isKnockedDown )
                {
                    if( _isSlowMoDown )
                        slow = !slow;
                }
                else // this entire "else" block doesn't belong here
                {
                    if( !_handledDeathSlowMo )
                    {
                        _handledDeathSlowMo = true;
                        slow = true;
                        _slowMoDeathRestoreTimer = slomoDeathDuration;
                    }

                    if( _slowMoDeathRestoreTimer > 0 )
                    {
                        _slowMoDeathRestoreTimer -= Time.unscaledDeltaTime;

                        if( _slowMoDeathRestoreTimer <= 0 )
                            slow = false;
                    }
                }


                timeFlow.isSlowed = slow;

                if( _isPauseDown )
                    timeFlow.isStopped = !timeFlow.isStopped;
            }
        }



        //--------------------------------------------------------------
        /// <summary> Updates the screenshotter (non-character).  </summary>
        //--------------------------------------
        private void UpdateScreenshots()
        {
            if( _isScreenShotDown )
                GameUtils.CaptureScreenshot( 2 );
        }



        //--------------------------------------------------------------
        /// <summary> An utility function that filters 2D input axes.  </summary>
        /// <remarks>
        ///     It applies a deadzone to remove noise from sticks; and
        ///     It remaps square axes to circular. 
        ///     e.g the upper right corner is by default (1, 1), 
        ///     but that is distance distance is greater than 1 (diagonals are longer)
        ///     instead we want all points on the perimeter of the stick to be at the
        ///     same distance.
        /// </remarks>
        //--------------------------------------
        private static Vector2 FilterInputAxes( Vector2 inputAxes )
        {
            Vector2 outputAxes = Vector2.zero;

            if( inputAxes.magnitude > axesDeadzone )
            {
                // By default axes are separate and map to a unit square
                // However we want the speed along the diagonals to be the same
                // as the speed along the main axes, so we remap the input to
                // a unit sphere (by normalizing and scaling accordingly) 

                Vector2 unitCircleRemap = inputAxes.normalized;
                outputAxes.x = inputAxes.x * Mathf.Abs( unitCircleRemap.x );
                outputAxes.y = inputAxes.y * Mathf.Abs( unitCircleRemap.y );
            }

            return outputAxes;
        }
        #endregion
        //......................................
    }
}