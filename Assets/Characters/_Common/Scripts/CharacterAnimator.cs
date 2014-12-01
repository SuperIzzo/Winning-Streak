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
	[Range(0.0f,1.0f)]
	public float goofiness;
	public BaseCharacterController characterController;
	public Animator animator;
	public Ragdoll	ragdoll;

	// Update is called once per frame
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

		// racdoll activates when the character is knocked down
		ragdoll.activated = knockedDown;
	}
}
