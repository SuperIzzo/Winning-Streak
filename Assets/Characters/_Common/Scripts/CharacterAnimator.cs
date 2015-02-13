using UnityEngine;
using System.Collections;

//--------------------------------------------------------------
/// <summary> Character animator. </summary>
/// <description> 
/// Animates the character 3D model based on the character 
/// controller.
/// </description>
//--------------------------------------
[AddComponentMenu("Character/Character Animator")]
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
	#endregion
	
	//--------------------------------------------------------------
	/// <summary> Update is called once per frame </summary>
	//--------------------------------------
	void Update ()
	{
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

		// ragdoll activates when the character is knocked down
		ragdoll.activated = knockedDown;
	}
}
