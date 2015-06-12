/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                     CharacterAnimator.cs                    </file> * 
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



    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary> Character animator. </summary>
    /// <description> 
    ///     Animates the character 3D model based on the character 
    ///     controller.
    /// </description>
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    [RequireComponent( typeof( Animator ) )]
    [AddComponentMenu( "Winning Streak/Character/Character Animator", 200 )]
    public class CharacterAnimator : MonoBehaviour
    {
        //..............................................................
        #region            //  INSPECTOR SETTINGS  //
        //--------------------------------------------------------------
        [SerializeField, Range(0,1), Tooltip
        (   "The goofiness paramater for the animation."              )]
        //--------------------------------------
        float _goofiness;


        //--------------------------------------------------------------
        [SerializeField, Tooltip
        (   "The ragdoll component."                                  )]
        //--------------------------------------
        Ragdoll _ragdoll;


        //--------------------------------------------------------------
        [SerializeField, Tooltip
        (   "The ragdoll component."                                  )]
        //--------------------------------------
        Rigidbody _rootBone;
        #endregion
        //......................................



        //..............................................................
        #region             //  PRIVATE FIELDS  //
        //--------------------------------------------------------------
        private IKnockableCharacter     _knockable;
        private IMobileCharacter        _mover;
        private IDancingCharacter       _dancer;
        private IThrowingCharacter      _thrower;
        private ITacklingCharacter      _tackler;
        private ACCharacterController   _characterAnim;
        #endregion
        //......................................



        //..............................................................
        #region                 //  METHODS  //
        //--------------------------------------------------------------
        /// <summary> Initiates internal references </summary>
        //--------------------------------------
        protected void Start()
        {
           
            _knockable  = GetComponentInParent<IKnockableCharacter>();
            _mover      = GetComponentInParent<IMobileCharacter>();
            _dancer     = GetComponentInParent<IDancingCharacter>();
            _thrower    = GetComponentInParent<IThrowingCharacter>();
            _tackler    = GetComponentInParent<ITacklingCharacter>();
            
            var animator = GetComponent<Animator>();
            _characterAnim = new ACCharacterController( animator );
        }



        //--------------------------------------------------------------
        /// <summary> Update is called once per frame </summary>
        //--------------------------------------
        protected void Update()
        {
            // Only update the animation if time is running
            if( TimeIsRunning() )
            {
                UpdateSpeed();
                UpdateWiggle();
                UpdateTackle();
                UpdateChargeThrow();
                UpdateGoofiness();
                UpdateRagdoll();
            }
        }



        //--------------------------------------------------------------
        /// <summary> Update the animation speed </summary>
        //--------------------------------------
        private void UpdateSpeed()
        {
            float speed = 0;
            if( _mover != null )
            {
                speed =  _mover.movementSpeed / 8.0f;
                speed *= _mover.relativeVelocity.magnitude;
            }
            _characterAnim.speed        = speed;
        }



        //--------------------------------------------------------------
        /// <summary> Update the animation wiggle state </summary>
        //--------------------------------------
        private void UpdateWiggle()
        {
            if( _dancer != null )
            {
                _characterAnim.wiggle = _dancer.isDancing;
            }
        }



        //--------------------------------------------------------------
        /// <summary> Updates the animation tackle state </summary>
        //--------------------------------------
        private void UpdateTackle()
        {
            if( _tackler != null )
            {
                _characterAnim.tackle = _tackler.isTackling;
            }
        }



        //--------------------------------------------------------------
        /// <summary> Update the aniation charge state </summary>
        //--------------------------------------
        private void UpdateChargeThrow()
        {
            if( _thrower != null )
            {
                _characterAnim.chargeThrow = _thrower.isCharging;
            }
        }



        //--------------------------------------------------------------
        /// <summary> Update the animation goofines </summary>
        //--------------------------------------
        private void UpdateGoofiness()
        {
            _characterAnim.goofiness    = _goofiness;
        }



        //--------------------------------------------------------------
        /// <summary> Update the animation ragdoll state </summary>
        //--------------------------------------
        private void UpdateRagdoll()
        {
            bool isKnockedDown = (_knockable != null) && _knockable.isKnockedDown;

            // HACK: Fix the character position based on the ragdoll simulation
            if( _ragdoll.activated && !isKnockedDown && _rootBone )
            {
                Vector3 position = _rootBone.transform.position;
                position.y = transform.root.position.y;
                transform.root.position = position;
            }

            // ragdoll activates when the character is knocked down
            _ragdoll.activated = isKnockedDown;
        }



        //--------------------------------------------------------------
        /// <summary> Returns true only of the time is running </summary>
        //--------------------------------------
        private static bool TimeIsRunning()
        {
            return Mathf.Abs( Time.deltaTime ) > float.Epsilon;
        }
        #endregion
        //......................................
    }
}