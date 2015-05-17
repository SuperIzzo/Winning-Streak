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
using UnityEngine;
using System.Collections;

//TODO: This script is messy

[AddComponentMenu("Winning Streak/Character/Player Input",100)]
[RequireComponent( typeof(BaseCharacterController) )]
public class PlayerInput : MonoBehaviour
{
	private BaseCharacterController controller;
	private static readonly float AXES_DEADZONE = 0.1f;
	private bool isScreenshotAxisInUse = false;

	private bool handledDeathSlowMo = false;
	private float slowMoDeathRestoreTimer = -1;
	private static float SLOW_MO_DEATH_DURATION = 2;

	// Use this for initialization
	void Start ()
	{
		controller = GetComponent<BaseCharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( !controller )
		{
			// Skip the update and log an error
			Debug.LogError( "No character controller found!" );
			return;
		}

		// Get Input
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		Vector2 controlVector = new Vector2(x,y);

		bool dancing		= Input.GetButton(     "Wiggle" );
		bool dashing		= Input.GetButton( "Dash"	);
		bool grabDown		= Input.GetButtonDown( "Grab" 	);
		bool grabUp			= Input.GetButtonUp(   "Grab" 	);
		bool slowMoDown		= Input.GetButtonDown(   "SlowMo" 	);
		bool pause			= Input.GetButtonDown(	 "Pause"	);
		bool screenShotDown	= Input.GetButtonDown(	 "Screenshot" );

		// Special case for dashDown as XBox triggers are axes
		dashing |= (Input.GetAxis("Dash")>0.5f);


		// And another special case for screenshots
		if( Input.GetAxis("Screenshot")>0.5f )
		{
			if( !isScreenshotAxisInUse )
			{
				isScreenshotAxisInUse = true;
				screenShotDown = true;
			}
		}
		else
		{
			isScreenshotAxisInUse = false;
		}



		// By default axes are separate and map to a unit square
		// However we want the speed along the diagonals to be the same
		// as the speed along the main axes, so we remap the input to
		// a unit sphere (by normalizing and scaling accordingly) 
		if( controlVector.magnitude > AXES_DEADZONE )
		{
			Vector2 unitCircleRemap = controlVector.normalized;
			controlVector.x *= Mathf.Abs( unitCircleRemap.x );
			controlVector.y *= Mathf.Abs( unitCircleRemap.y );
		}
		else
		{
			// Deadzone
			controlVector = Vector2.zero;
		}

		controller.Move( controlVector );
		controller.Dance( dancing );

		controller.isDashing = dashing;

		if( grabDown )
		{
			if( controller.heldObject )
			{
				controller.ChargeThrow();
			}
			else
			{
				controller.Grab();
			}
		}

		if( grabUp && controller.isCharging )
		{
			controller.Throw();
		}

		if( screenShotDown )
			GameUtils.CaptureScreenshot(2);

		// Slow-mo
		TimeFlow timeFlow = GameSystem.timeFlow;
		if( timeFlow )
		{
			bool slow = timeFlow.isSlowed;

			if( !controller.isKnockedDown )
			{
				if( slowMoDown )
					slow = !slow;
			}
			else // this entire "else" block doesn't belong here
			{
				if( !handledDeathSlowMo )
				{
					handledDeathSlowMo = true;
					slow = true;
					slowMoDeathRestoreTimer = SLOW_MO_DEATH_DURATION;
				}

				if( slowMoDeathRestoreTimer>0 )
				{
					slowMoDeathRestoreTimer -= Time.unscaledDeltaTime;

					if( slowMoDeathRestoreTimer <= 0 )
						slow = false;
				}
			}


			timeFlow.isSlowed = slow;

			if( pause )
				timeFlow.isStopped = !timeFlow.isStopped;
		}
	}



}
