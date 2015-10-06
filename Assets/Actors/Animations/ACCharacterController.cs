/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                   ACCharacterController.cs                  </file> * 
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
namespace RoaringSnail.WinningStreak
{
    using UnityEngine;



    //--------------------------------------------------------------
    /// <summary>   AnimationController wrapper to access animation
    ///             specific variables in a safe manner.  </summary>
    //--------------------------------------
    public class ACCharacterController
    {
        private const string avSpeed         = "speed";
        private const string avGoofiness     = "goofiness";
        private const string avWiggle        = "wiggle";
        private const string avTackle        = "tackle";
        private const string avChargeThrow   = "charge_throw";

        private Animator _animator;

        public ACCharacterController( Animator animator )
        {
            _animator = animator;
        }

        public float speed
        {
            get { return _animator.GetFloat( avSpeed ); }
            set { if (enabled) _animator.SetFloat( avSpeed, value ); }
        }

        public float goofiness
        {
            get { return _animator.GetFloat( avGoofiness ); }
            set { if( enabled ) _animator.SetFloat( avGoofiness, value ); }
        }

        public bool wiggle
        {
            get { return _animator.GetBool( avWiggle ); }
            set { if( enabled ) _animator.SetBool( avWiggle, value ); }
        }

        public bool tackle
        {
            get { return _animator.GetBool( avTackle ); }
            set { if( enabled ) _animator.SetBool( avTackle, value ); }
        }

        public bool chargeThrow
        {
            get { return _animator.GetBool( avChargeThrow ); }
            set { if( enabled ) _animator.SetBool( avChargeThrow, value ); }
        }

        public bool enabled
        {
            get { return _animator.enabled; }
            set { _animator.enabled = value; }
        }
    }
}