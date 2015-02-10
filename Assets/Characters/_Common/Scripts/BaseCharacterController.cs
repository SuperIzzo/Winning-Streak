﻿using UnityEngine;
using System.Collections;

[AddComponentMenu("Character/Base Character Controller")]
public class BaseCharacterController : MonoBehaviour
{
	private static readonly float MIN_TURN_VECTOR_MAGNITUDE = 0.1f;

	// Public settings / properties
	public float	movementSpeed	= 5.0f;
	public float	turningSpeed	= 6.0f;
	public float	dashBoost		= 3.0f;
	public float 	dashDuration	= 1.0f;
	public float	dashCooldown	= 5.0f;
	public float	grabRadius		= 1.0f;
	public float 	maxChargeTime	= 3.0f;
	public float 	throwPower		= 20.0f;
	public float	tackleDuration	= 0.3f;
	public float	tackleSpeed		= 4.0f;
	public float	reviveTimeMin	= 10.0f;
	public float	reviveTimeMax	= 15.0f;

	public Transform propSlot;

	public bool		isKnockedDown	 {get{return _isKnockedDown;} set{if(value) KnockDown(); else Revive();} }
	public bool		isDancing		 {get{return _isDancing;} set{Dance(value);} }
	public bool		isDashing		 {get{return _isDashing;} set{if(value) Dash();} }
	public bool		isTackling		 {get{return _tackleTimer>0;} set{Tackle(value);} }
	public bool		isCharging		 {get{return _isCharging;} set{if(value) ChargeThrow(); else Throw();} }
	public Vector2	relativeVelocity {get; private set;}
	public Vector3	lookDirection 	 {get; private set;}

	public GameObject heldObject	 {get; private set;}

	// Private state
	private bool	_isKnockedDown;
	private bool	_isDancing;
	private bool	_isDashing;
	private bool	_isCharging;
	private float	_dashCooldownTimer;
	private float	_dashDurationTimer;
	private float	_chargeTimer;
	private float	_tackleTimer;
	private float	_reviveTimer;


	//--------------------------------------------------------------
	/// <summary> Knocks down the character. </summary>
	//--------------------------------------
	public void KnockDown()
	{
		if( !_isKnockedDown )
		{
			_tackleTimer = 0.0f;
			_reviveTimer = Random.Range(reviveTimeMin, reviveTimeMax);
			_isKnockedDown = true;
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
		}
	}

	//--------------------------------------------------------------
	/// <summary> Move in the specified direction with the set
	/// relative speed. </summary>
	/// <param name="dir"> relative normalized velocity </param>
	//--------------------------------------
	public void Move( Vector2 vel, bool turn=true )
	{
		this.relativeVelocity = vel;
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
		if( dir.magnitude >= MIN_TURN_VECTOR_MAGNITUDE )
		{
			lookDirection = new Vector3( dir.x, 0, dir.y );
			lookDirection.Normalize();
		}
	}
	
	//--------------------------------------------------------------
	/// <summary> Sets the dancing state for the charater </summary>
	/// <param name="dance">dancing state</param>
	//--------------------------------------
	public void Dance( bool dance = true )
	{
		if( !isKnockedDown && !isTackling )
		{
			_isDancing = dance;

			if( dance )
			{
				CancelDash();
			}
		}
	}

	//--------------------------------------------------------------
	/// <summary> Sets dashing state for the charater </summary>
	//--------------------------------------
	public void Dash()
	{
		if( !isKnockedDown && !_isDashing && _dashCooldownTimer <= 0 )
		{
			_isDashing = true;
			_dashDurationTimer = dashDuration;
			_dashCooldownTimer = dashCooldown;
		}
	}
	
	//--------------------------------------------------------------
	/// <summary> Cancels the dashing state </summary>
	//--------------------------------------
	private void CancelDash()
	{
		_isDashing = false;
		_dashDurationTimer = 0;
	}
	
	//--------------------------------------------------------------
	/// <summary> Sets the tackling state for the charater </summary>
	/// <param name="tackle">tackling state</param>
	//--------------------------------------
	public void Tackle( bool tackle )
	{
		if( !isKnockedDown && !_isDancing && !isTackling )
		{
			_tackleTimer = tackleDuration;
		}
	}

	//--------------------------------------------------------------
	/// <summary> Grabs the nearest pickable object </summary>
	//--------------------------------------
	public void Grab()
	{
		if( !heldObject && !isDancing && !isTackling )
		{
			GameObject		closestProp = null;
			ThrowableObject throwable = null;
			float 			minDistance = Mathf.Infinity; 

			// 1. Get all objects within a radius
			Collider[] envProps = Physics.OverlapSphere(transform.position, grabRadius);

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
					throwable.OnGrabbed( this, propSlot );
				}
			}

		}
	}

	//--------------------------------------------------------------
	/// <summary> Charges a throw the object the character is holdign </summary>
	//--------------------------------------
	public void ChargeThrow()
	{
		if( heldObject && !isDancing && !isTackling && !_isCharging )
		{
			_isCharging  = true;
			_chargeTimer = maxChargeTime;
		}
	}

	//--------------------------------------------------------------
	/// <summary> Throws the object the character is holding </summary>
	//--------------------------------------
	public void Throw()
	{
		if( heldObject && _isCharging )
		{
			ThrowableObject throwable = heldObject.GetComponent<ThrowableObject>();
			if( throwable )
			{
				float 	charge = 1 - (_chargeTimer/maxChargeTime);
                Vector3 throwForce = lookDirection + (Vector3.up*0.5f);
				throwForce.Normalize();
				throwForce *= charge * throwPower;

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
	private void Update ()
	{
		if( !isKnockedDown )
		{
			ProcessDashing();
			ProcessThrowing();
			ProcessTackling();

			if( !isDancing && !isTackling )
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
		// Normal movement
		Vector3 moveVel = new Vector3( relativeVelocity.x, 0, relativeVelocity.y );
				moveVel *= movementSpeed * Time.deltaTime;

		// Dashing
		Vector3 dashVel = Vector3.zero; 
		if( isDashing )
		{
			dashVel = lookDirection;
			dashVel *= dashBoost * Time.deltaTime;
		}

		// Final result
		transform.position = transform.position + moveVel + dashVel;
	}

	//--------------------------------------------------------------
	/// <summary> Processes the turning. </summary>
	//--------------------------------------
	private void ProcessTurning()
	{
		if( lookDirection.magnitude > 0 )
		{
			Quaternion targetRot = Quaternion.LookRotation( lookDirection, Vector3.up );
			
			//smooth transitioning for rotation, also makes the rotation and movement more human like
			transform.rotation = Quaternion.Slerp( transform.rotation,	targetRot, 
			                                       Time.deltaTime * turningSpeed );
		}
	}

	//--------------------------------------------------------------
	/// <summary> Processes the dashing. </summary>
	//--------------------------------------
	private void ProcessDashing()
	{
		// Cooldown
		if( _dashCooldownTimer> 0 )
			_dashCooldownTimer -= Time.deltaTime;

		// Duration
		if( _dashDurationTimer> 0 )
		{
			_dashDurationTimer -= Time.deltaTime;
			if( _dashDurationTimer<=0 )
			{
				CancelDash();
			}
		}
	}

	//--------------------------------------------------------------
	/// <summary> Processes the throwing and charging. </summary>
	//--------------------------------------
	private void ProcessThrowing()
	{
		// Charge timer
		if( _chargeTimer > 0 )
		{
			_chargeTimer -= Time.deltaTime;
			if( _chargeTimer<=0 )
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
		if( _tackleTimer > 0 )
		{
			_tackleTimer -= Time.deltaTime;
			transform.position += transform.forward * (tackleSpeed * Time.deltaTime);

			// Knock down this character at the end of the tackle
			if( _tackleTimer <=0 )
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
		if( _reviveTimer > 0 )
		{
			_reviveTimer -= Time.deltaTime;

			if( _reviveTimer<=0 )
			{
				Revive();
			}
		}
	}
}
