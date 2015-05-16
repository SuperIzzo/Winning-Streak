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
using UnityEngine;
using System.Collections;

//--------------------------------------------------------------
/// <summary> Character animator. </summary>
/// <description> 
/// Animates the character 3D model based on the character 
/// controller.
/// </description>
//--------------------------------------
[AddComponentMenu("Winning Streak/Character/Character Animator",200)]
public class CharacterAnimator : MonoBehaviour
{
	//--------------------------------------------------------------
	#region Public settings
	//--------------------------------------
	/// <summary> The goofiness paramater for the animation. </summary>
	[Range(0.0f,1.0f)]
	public float goofiness;
	#endregion


	//--------------------------------------------------------------
	#region Public references
	//--------------------------------------
	/// <summary> The character controller component. </summary>
	public BaseCharacterController characterController;

	/// <summary> The animator component. </summary>
	public Animator animator;

	/// <summary> The ragdoll component. </summary>
	public Ragdoll	ragdoll;

	public Rigidbody rootBone;
	#endregion
	
	//--------------------------------------------------------------
	/// <summary> Update is called once per frame </summary>
	//--------------------------------------
	void Update ()
	{
		// Only update the animation if time is running
		if( Mathf.Abs( Time.deltaTime ) <= float.Epsilon )
			return;

		float 	speed		= characterController.relativeVelocity.magnitude;
		bool  	dancing		= characterController.isDancing;
		bool	tackling	= characterController.isTackling;
		bool	charging	= characterController.isCharging;
		bool	knockedDown	= characterController.isKnockedDown;

		animator.SetFloat(	"speed",		speed		);
		animator.SetFloat(	"goofiness",	goofiness	);
		animator.SetBool(	"wiggle", 		dancing		);
		animator.SetBool(	"tackle",		tackling	);
		animator.SetBool(	"charge_throw",	charging	);

		// Fix the character position based on the ragdoll simulations
		if( ragdoll.activated && !knockedDown && rootBone )
		{
			Vector3 position = rootBone.transform.position;
			position.y = characterController.transform.position.y;
			characterController.transform.position = position;
		}

		// ragdoll activates when the character is knocked down
		ragdoll.activated = knockedDown;
	}
}
