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
        (   "The time required for the character to grab an object."  )]
        //--------------------------------------
        Countdown _grabTime = new Countdown(1.0f);


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
        private GameObject _grabTarget;
        private GameObject _heldObject;

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
        public GameObject heldObject
        {
            get { return _heldObject;  }
            set
            {
                grabTarget = null;
                _heldObject = value;                
            }
        }



        //--------------------------------------------------------------
        /// <summary> Gets the held object. </summary>
        /// <value>The held object.</value>
        //--------------------------------------
        public GameObject grabTarget
        {
            get { return _grabTarget; }
            private set
            {
                if( value!=null && heldObject!=null )
                {
                    Debug.LogError( "Trying to grab an object while" +
                                    " holding another." );
                }

                _grabTarget = value;

                if( _grabTarget != null )
                {
                    _grabTime.Restart();
                }
                else
                {
                    _grabTime.Stop();
                }
            }
        }



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
        /// <summary> Charges a throw the object the character is holdign </summary>
        //--------------------------------------
        private void CancelThrow()
        {
            _isCharging = false;
            _chargeTime.Stop();
        }



        //--------------------------------------------------------------
        /// <summary> Grabs the nearest pickable object </summary>
        //--------------------------------------
        public void TargetToGrab()
        {
            if( !grabTarget && !heldObject && !isDancing )
            {
                GameObject closestProp = null;
                IThrowableObject throwable = null;
                float minDistance = Mathf.Infinity;

                // 1. Get all objects within a radius
                Collider[] envProps = Physics.OverlapSphere(transform.position, _grabRadius);

                // 2. Pick the closest one
                foreach( Collider prop in envProps )
                {
                    throwable = prop.GetComponent<IThrowableObject>();
                    // TODO: Add "Usable" items here the same way
                    //		 a game object has to be either throwable or usable
                    //		 (or both) to be grabbed and later thrown/used.

                    if( throwable!=null )
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
                    grabTarget = closestProp;
                }
            }
        }



        //--------------------------------------------------------------
        /// <summary> Grabs the targeted object. </summary>
        //--------------------------------------
        public void Grab()
        {
            heldObject = grabTarget;
            //heldObject.transform.parent = _propSlot;

            var throwable = heldObject.GetComponent<IThrowableObject>();
            if( throwable!=null )
            {
                throwable.OnGrabbed( this, _propSlot );
            }
        }



        //--------------------------------------------------------------
        /// <summary> Throws the object the character is holding </summary>
        //--------------------------------------
        public void Throw()
        {
            if( heldObject && _isCharging )
            {
                var throwable = heldObject.GetComponent<IThrowableObject>();
                if( throwable!=null )
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
        /// <summary> Processes the grabbing. </summary>
        //--------------------------------------
        private void OnGrabTimeEnded( object sender, EventArgs e )
        {
            grabTarget = null; // We give up and stand up
        }


        
        //--------------------------------------------------------------
        /// <summary> Sets up the character throwing. </summary>
        //--------------------------------------
        partial void Setup_Throwing()
        {
            _chargeTime.AlarmRaised += OnChargeTimeEnded;
            _chargeTime.InstallCoroutine( this );

            _grabTime.AlarmRaised += OnGrabTimeEnded;
            _grabTime.InstallCoroutine( this );
        }
    }
}
