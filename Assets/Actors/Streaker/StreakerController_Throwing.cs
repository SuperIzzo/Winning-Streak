/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                 StreakerController_Throwing.cs              </file> * 
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


    public partial class StreakerController : IThrowingCharacter
    {
        //--------------------------------------------------------------
        [SerializeField, Tooltip
        (  "The reach radius of this character."                      )]
        //--------------------------------------
        float _grabRadius = 1.0f;


        //--------------------------------------------------------------
        [SerializeField, Tooltip
        (   "The maximal time the character will charge before " +
            " throwing an object."                                    )]
        //--------------------------------------
        Countdown _chargeTime = new Countdown(3.0f);


        //--------------------------------------------------------------
        [SerializeField, Tooltip
        (   "The transform grabbed objects go to when held."          )]
        //--------------------------------------
        Transform _propSlot;


        //--------------------------------------------------------------
        [SerializeField, Tooltip
        (   "The magniture of the force applied to thrown objects."   )]
        //--------------------------------------
        float _throwPower = 20.0f;

        private bool _isCharging;

        public event EventHandler Grabbed;
        public event EventHandler Charged;
        public event EventHandler Thrown;



        //--------------------------------------------------------------
        /// <see cref="BaseCharacterController" />
        /// <value>
        ///   <c>true</c> if is charging; otherwise, <c>false</c>.</value>
        //--------------------------------------
        public bool isCharging
        {
            get { return _isCharging; }
            set { if( value ) ChargeThrow(); else Throw(); }
        }



        //--------------------------------------------------------------
        /// <summary> Gets the held object. </summary>
        /// <value>The held object.</value>
        //--------------------------------------
        public GameObject heldObject { get; private set; }



        //--------------------------------------------------------------
        /// <summary> Charges a throw the object the character is holdign </summary>
        //--------------------------------------
        public void ChargeThrow()
        {
            if( heldObject && !isDancing && !_isCharging )
            {
                _isCharging = true;
                _chargeTime.Restart();
            }
        }



        //--------------------------------------------------------------
        /// <summary> Grabs the nearest pickable object </summary>
        //--------------------------------------
        public void Grab()
        {
            if( !heldObject && !isDancing )
            {
                GameObject closestProp = null;
                ThrowableObject throwable = null;
                float minDistance = Mathf.Infinity;

                // 1. Get all objects within a radius
                Collider[] envProps = Physics.OverlapSphere(transform.position, _grabRadius);

                // 2. Pick the closest one
                foreach( Collider prop in envProps )
                {
                    throwable = prop.GetComponent<ThrowableObject>();
                    // TODO: Add "Usable" items here the same way
                    //		 a game object has to be either throwable or usable
                    //		 (or both) to be grabbed and later thrown/used.

                    if( throwable )
                    {
                        Vector3 distance = prop.transform.position - transform.position;
                        if( distance.magnitude < minDistance )
                        {
                            minDistance = distance.magnitude;
                            closestProp = prop.gameObject;
                        }
                    }
                }

                // 3. Profit (link together and announce the grabbing)
                if( closestProp )
                {
                    heldObject = closestProp;
                    throwable = closestProp.GetComponent<ThrowableObject>();

                    if( throwable )
                    {
                        throwable.OnGrabbed( this, _propSlot );
                    }
                }

            }
        }



        //--------------------------------------------------------------
        /// <summary> Throws the object the character is holding </summary>
        //--------------------------------------
        public void Throw()
        {
            if( heldObject && _isCharging )
            {
                var throwable = heldObject.GetComponent<ThrowableObject>();
                if( throwable )
                {
                    float charge = 1 - (_chargeTime.currentTime / _chargeTime.setTime);
                    Vector3 throwForce = lookDirection + (Vector3.up * 0.5f);
                    throwForce.Normalize();
                    throwForce *= charge * _throwPower;

                    throwable.OnThrown( this, throwForce );
                }

                _chargeTime.Stop();
                _isCharging = false;
                heldObject = null;
            }
        }



        //--------------------------------------------------------------
        /// <summary> Processes the throwing and charging. </summary>
        //--------------------------------------
        private void OnChargeTimeEnded( object sender, EventArgs e )
        {
            // Charge timer
            Throw();
        }



        //--------------------------------------------------------------
        /// <summary> Sets up the character throwing. </summary>
        //--------------------------------------
        partial void Setup_Throwing()
        {
            _chargeTime.Install( this );

            _chargeTime.AlarmRaised += OnChargeTimeEnded;
        }
    }
}
