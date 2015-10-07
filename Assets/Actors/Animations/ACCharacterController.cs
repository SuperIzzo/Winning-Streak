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
        private const string avGrab          = "grab";
        private const string avChargeThrow   = "charge_throw";
        

        public Animator animator { get; private set; }


        public ACCharacterController( Animator animator )
        {
            this.animator = animator;
        }

        public float speed
        {
            get { return animator.GetFloat( avSpeed ); }
            set { if (enabled) animator.SetFloat( avSpeed, value ); }
        }

        public float goofiness
        {
            get { return animator.GetFloat( avGoofiness ); }
            set { if( enabled ) animator.SetFloat( avGoofiness, value ); }
        }

        public bool wiggle
        {
            get { return animator.GetBool( avWiggle ); }
            set { if( enabled ) animator.SetBool( avWiggle, value ); }
        }

        public bool tackle
        {
            get { return animator.GetBool( avTackle ); }
            set { if( enabled ) animator.SetBool( avTackle, value ); }
        }

        public bool chargeThrow
        {
            get { return animator.GetBool( avChargeThrow ); }
            set { if( enabled ) animator.SetBool( avChargeThrow, value ); }
        }

        public bool grab
        {
            get { return animator.GetBool( avGrab ); }
            set { if( enabled ) animator.SetBool( avGrab, value ); }
        }

        public bool enabled
        {
            get { return animator.enabled; }
            set { animator.enabled = value; }
        }
    }
}