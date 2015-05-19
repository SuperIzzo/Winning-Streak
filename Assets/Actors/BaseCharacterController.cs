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
namespace RoaringSnail.WinningStreak
{
    using UnityEngine;



    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary> Base character controller encapsulates the core 
    /// game logic of a game characters. </summary>
    /// <description> This class is purely mechanical. It maintains 
    /// the character's state and manages it internally.
    /// In MVC terms this is the Model. </description>
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-= 
    [AddComponentMenu("Winning Streak/Character/Base Character Controller")]
    public class BaseCharacterController : MonoBehaviour
    {
        private static readonly float turningThreshold = 0.1f;


        //..............................................................
        #region            //  INSPECTOR SETTINGS  //
        //--------------------------------------------------------------
        [Tooltip("This character's movement speed")]
        //--------------------------------------
        [SerializeField] float _movementSpeed = 4.5f;


        //--------------------------------------------------------------
        [Tooltip("This character's turning speed")]
        //--------------------------------------
        [SerializeField] float _turningSpeed = 6.0f;


        //--------------------------------------------------------------
        [Tooltip("The speed this character obtains when running.")]
        //--------------------------------------
        [SerializeField] float _runSpeed = 7.0f;


        //--------------------------------------------------------------
        [Tooltip("The speed boost this character obtains when dashing.")]
        //--------------------------------------
        [SerializeField] float _dashBoost = 2.5f;


        //--------------------------------------------------------------
        [Tooltip("The duration of the dash boost.")]
        //--------------------------------------
        [SerializeField] float _dashDuration = 1.0f;


        //--------------------------------------------------------------
        [Tooltip("The dash cooldown before it can be used again.")]
        //--------------------------------------
        [SerializeField] float _dashCooldown = 5.0f;


        //--------------------------------------------------------------
        [Tooltip("The reach radius of this character.")]
        //--------------------------------------
        [SerializeField] float _grabRadius = 1.0f;


        //--------------------------------------------------------------
        [Tooltip("The maximal time the character will " +
                 "charge before throwing an object.")]
        //--------------------------------------
        [SerializeField] float _maxChargeTime = 3.0f;


        //--------------------------------------------------------------
        [Tooltip("The magniture of the force applied " +
                  "to thrown objects.")]
        //--------------------------------------
        [SerializeField] float _throwPower = 20.0f;


        //--------------------------------------------------------------
        [Tooltip("The duration of the tackle before " + 
                 "the character falls knocked down.")]
        //--------------------------------------
        [SerializeField]  float _tackleDuration = 0.3f;


        //--------------------------------------------------------------
        [Tooltip("The speed at which this character tackles.")]
        //--------------------------------------
        [SerializeField]  float _tackleSpeed = 4.0f;


        //--------------------------------------------------------------
        [Tooltip("The minimal revival time.")]
        //--------------------------------------
        [SerializeField]  float _reviveTimeMin = 10.0f;


        //--------------------------------------------------------------
        [Tooltip("The maximal revival time.")]
        //--------------------------------------
        [SerializeField]  float _reviveTimeMax = 15.0f;


        //--------------------------------------------------------------
        [Tooltip("The transform grabbed objects go to when held.")]
        //--------------------------------------
        [SerializeField]  Transform _propSlot;
        #endregion
        //......................................


        //..............................................................
        #region            //  PRIVATE STATE  //
        //--------------------------------------------------------------
        private bool _isKnockedDown;
        private bool _isDancing;
        private bool _isDashing;
        private bool _isRunning;
        private bool _isCharging;
        private float _dashCooldownTimer;
        private float _dashDurationTimer;
        private float _chargeTimer;
        private float _tackleTimer;
        private float _reviveTimer;
        #endregion
        //......................................



        //..............................................................
        #region              //  PUBLIC PROPERTIES  //
        //--------------------------------------------------------------
        /// <summary> Gets or sets a value indicating whether this 
        /// <see cref="BaseCharacterController"/> is knocked down. </summary>
        /// <value><c>true</c> if knocked down; otherwise, <c>false</c>.</value>
        //--------------------------------------
        public bool isKnockedDown { get { return _isKnockedDown; } set { if (value) KnockDown(); else Revive(); } }


        //--------------------------------------------------------------
        /// <summary> Gets or sets a value indicating whether this
        /// <see cref="BaseCharacterController"/> is dancing. </summary>
        /// <value><c>true</c> if dancing; otherwise, <c>false</c>.</value>
        //--------------------------------------
        public bool isDancing { get { return _isDancing; } set { Dance(value); } }


        //--------------------------------------------------------------
        /// <summary> Gets or sets a value indicating whether this
        /// <see cref="BaseCharacterController"/> is dashing. </summary>
        /// <value><c>true</c> if dashing; otherwise, <c>false</c>.</value>
        //--------------------------------------
        public bool isDashing { get { return _isDashing; } set { if (value) Dash(); else StopDash(); } }


        //--------------------------------------------------------------
        /// <summary> Gets or sets a value indicating whether this
        /// <see cref="BaseCharacterController"/> is running. </summary>
        /// <value><c>true</c> if running; otherwise, <c>false</c>.</value>
        //--------------------------------------
        public bool isRunning { get { return _isRunning; } }


        //--------------------------------------------------------------
        /// <summary> Gets or sets a value indicating whether this
        /// <see cref="BaseCharacterController"/> is tackling. </summary>
        /// <value><c>true</c> if is tackling; otherwise, <c>false</c>.</value>
        //--------------------------------------
        public bool isTackling { get { return _tackleTimer > 0; } set { Tackle(value); } }


        //--------------------------------------------------------------
        /// <summary> Gets or sets a value indicating whether this
        /// <see cref="BaseCharacterController"/> is charging.
        /// <value><c>true</c> if is charging; otherwise, <c>false</c>.</value>
        //--------------------------------------
        public bool isCharging { get { return _isCharging; } set { if (value) ChargeThrow(); else Throw(); } }


        //--------------------------------------------------------------
        /// <summary> Returns the relative velocity (readonly). </summary>
        /// <value>The relative velocity.</value>
        //--------------------------------------
        public Vector2 relativeVelocity { get; private set; }


        //--------------------------------------------------------------
        /// <summary> Returns the look direction (readonly). </summary>
        /// <value>The look direction.</value>
        //--------------------------------------
        public Vector3 lookDirection { get; private set; }


        //--------------------------------------------------------------
        /// <summary> Gets the held object. </summary>
        /// <value>The held object.</value>
        //--------------------------------------
        public GameObject heldObject { get; private set; }


        //--------------------------------------------------------------
        /// <summary> Gets or sets the character movement speed. </summary>
        /// <value>The movement speed.</value>
        //--------------------------------------
        public float movementSpeed
        {
            get { return _movementSpeed;  }
            set { _movementSpeed = value; }
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
            if (!_isKnockedDown)
            {
                _tackleTimer = 0.0f; // Cancel tackling
                _isKnockedDown = true;

                if (_reviveTimeMin >= 0 && _reviveTimeMax >= _reviveTimeMin)
                {
                    _reviveTimer = Random.Range(_reviveTimeMin, _reviveTimeMax);
                }
                else
                {
                    _reviveTimer = -1;
                }
            }
        }



        //--------------------------------------------------------------
        /// <summary> Revives a knocked down character. </summary>
        //--------------------------------------
        public void Revive()
        {
            if (_isKnockedDown)
            {
                _reviveTimer = -1;
                _isKnockedDown = false;
            }
        }



        //--------------------------------------------------------------
        /// <summary> Move in the specified direction with the set
        /// relative speed. </summary>
        /// <param name="dir"> relative normalized velocity </param>
        //--------------------------------------
        public void Move(Vector2 vel, bool turn = true)
        {
            this.relativeVelocity = vel;
            if (turn)
            {
                Turn(vel);
            }
        }



        //--------------------------------------------------------------
        /// <summary> Turn in the specified direction. </summary>
        /// <param name="dir">The look direction.</param>
        //--------------------------------------
        public void Turn(Vector2 dir)
        {
            if (dir.magnitude >= turningThreshold)
            {
                lookDirection = new Vector3(dir.x, 0, dir.y);
                lookDirection.Normalize();
            }
        }



        //--------------------------------------------------------------
        /// <summary> Sets the dancing state for the charater </summary>
        /// <param name="dance">dancing state</param>
        //--------------------------------------
        public void Dance(bool dance = true)
        {
            if (!isKnockedDown && !isTackling)
            {
                _isDancing = dance;

                if (dance)
                {
                    StopDash();
                }
            }
        }



        //--------------------------------------------------------------
        /// <summary> Sets dashing state for the charater </summary>
        //--------------------------------------
        public void Dash()
        {
            if (!isKnockedDown && !_isDashing)
            {
                if (_dashCooldownTimer <= 0)
                {
                    _isRunning = false;
                    _isDashing = true;
                    _dashDurationTimer = _dashDuration;
                    _dashCooldownTimer = _dashCooldown;
                }
                else
                {
                    _isRunning = true;
                    _isDashing = false;
                    _dashCooldownTimer = _dashCooldown;
                }
            }
        }



        //--------------------------------------------------------------
        /// <summary> Cancels the dashing state </summary>
        //--------------------------------------
        private void StopDash()
        {
            _isDashing = false;
            _isRunning = false;
            _dashDurationTimer = 0;
        }



        //--------------------------------------------------------------
        /// <summary> Sets the tackling state for the charater </summary>
        /// <param name="tackle">tackling state</param>
        //--------------------------------------
        public void Tackle(bool tackle)
        {
            if (!isKnockedDown && !_isDancing && !isTackling)
            {
                _tackleTimer = _tackleDuration;
            }
        }



        //--------------------------------------------------------------
        /// <summary> Grabs the nearest pickable object </summary>
        //--------------------------------------
        public void Grab()
        {
            if (!heldObject && !isDancing && !isTackling)
            {
                GameObject closestProp = null;
                ThrowableObject throwable = null;
                float minDistance = Mathf.Infinity;

                // 1. Get all objects within a radius
                Collider[] envProps = Physics.OverlapSphere(transform.position, _grabRadius);

                // 2. Pick the closest one
                foreach (Collider prop in envProps)
                {
                    throwable = prop.GetComponent<ThrowableObject>();
                    // TODO: Add "Usable" items here the same way
                    //		 a game object has to be either throwable or usable
                    //		 (or both) to be grabbed and later thrown/used.

                    if (throwable)
                    {
                        Vector3 distance = prop.transform.position - transform.position;
                        if (distance.magnitude < minDistance)
                        {
                            minDistance = distance.magnitude;
                            closestProp = prop.gameObject;
                        }
                    }
                }

                // 3. Profit (link together and announce the grabbing)
                if (closestProp)
                {
                    heldObject = closestProp;
                    throwable = closestProp.GetComponent<ThrowableObject>();

                    if (throwable)
                    {
                        throwable.OnGrabbed(this, _propSlot);
                    }
                }

            }
        }



        //--------------------------------------------------------------
        /// <summary> Charges a throw the object the character is holdign </summary>
        //--------------------------------------
        public void ChargeThrow()
        {
            if (heldObject && !isDancing && !isTackling && !_isCharging)
            {
                _isCharging = true;
                _chargeTimer = _maxChargeTime;
            }
        }



        //--------------------------------------------------------------
        /// <summary> Throws the object the character is holding </summary>
        //--------------------------------------
        public void Throw()
        {
            if (heldObject && _isCharging)
            {
                ThrowableObject throwable = heldObject.GetComponent<ThrowableObject>();
                if (throwable)
                {
                    float charge = 1 - (_chargeTimer / _maxChargeTime);
                    Vector3 throwForce = lookDirection + (Vector3.up * 0.5f);
                    throwForce.Normalize();
                    throwForce *= charge * _throwPower;

                    throwable.OnThrown(this, throwForce);
                }

                _isCharging = false;
                _chargeTimer = 0.0f;
                heldObject = null;
            }
        }



        //--------------------------------------------------------------
        /// <summary> Update this instance. </summary>
        //--------------------------------------
        private void Update()
        {
            if (!isKnockedDown)
            {
                ProcessDashing();
                ProcessThrowing();
                ProcessTackling();

                if (!isDancing && !isTackling)
                {
                    ProcessMovement();
                    ProcessTurning();
                }
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
            float speed = _movementSpeed;

            if (isRunning || isDashing)
                speed = _runSpeed;

            // Normal movement
            Vector3 moveVel = new Vector3(relativeVelocity.x, 0, relativeVelocity.y);
            moveVel *= speed * Time.deltaTime;

            // Dashing
            Vector3 dashVel = Vector3.zero;
            if (isDashing)
            {
                dashVel = lookDirection;
                dashVel *= _dashBoost * Time.deltaTime;
            }

            // Final result
            transform.position = transform.position + moveVel + dashVel;
        }



        //--------------------------------------------------------------
        /// <summary> Processes the turning. </summary>
        //--------------------------------------
        private void ProcessTurning()
        {
            if (lookDirection.magnitude > 0)
            {
                Quaternion targetRot = Quaternion.LookRotation(lookDirection, Vector3.up);

                //smooth transitioning for rotation, also makes the rotation and movement more human like
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot,
                                                       Time.deltaTime * _turningSpeed);
            }
        }



        //--------------------------------------------------------------
        /// <summary> Processes the dashing. </summary>
        //--------------------------------------
        private void ProcessDashing()
        {
            // Cooldown (counts down only while walking)
            if (!_isDashing && !_isRunning && _dashCooldownTimer > 0)
                _dashCooldownTimer -= Time.deltaTime;

            // Duration
            if (_dashDurationTimer > 0)
            {
                _dashDurationTimer -= Time.deltaTime;
                if (_dashDurationTimer <= 0)
                {
                    StopDash();
                    _isRunning = true;
                }
            }
        }



        //--------------------------------------------------------------
        /// <summary> Processes the throwing and charging. </summary>
        //--------------------------------------
        private void ProcessThrowing()
        {
            // Charge timer
            if (_chargeTimer > 0)
            {
                _chargeTimer -= Time.deltaTime;
                if (_chargeTimer <= 0)
                {
                    Throw();
                }
            }
        }



        //--------------------------------------------------------------
        /// <summary> Processes the throwing and charging. </summary>
        //--------------------------------------
        private void ProcessTackling()
        {
            if (_tackleTimer > 0)
            {
                _tackleTimer -= Time.deltaTime;
                transform.position += transform.forward * (_tackleSpeed * Time.deltaTime);

                // Knock down this character at the end of the tackle
                if (_tackleTimer <= 0)
                {
                    KnockDown();
                }
            }
        }



        //--------------------------------------------------------------
        /// <summary> Processes the revival of a knocked down character. </summary>
        //--------------------------------------
        private void ProcessRevival()
        {
            if (_reviveTimer > 0)
            {
                _reviveTimer -= Time.deltaTime;

                if (_reviveTimer <= 0)
                {
                    Revive();
                }
            }
        }
        #endregion
    }
}
