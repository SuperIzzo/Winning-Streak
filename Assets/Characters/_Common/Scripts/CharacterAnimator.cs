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

	// Update is called once per frame
	void Update ()
	{
		float 	speed		= characterController.relativeVelocity.magnitude;
		bool  	dancing		= characterController.isDancing;
		bool	tackling	= characterController.isTackling;

		animator.SetFloat(	"speed",		speed		);
		animator.SetFloat(	"goofiness",	goofiness	);
		animator.SetBool(	"wiggle", 		dancing		);
		animator.SetBool(	"tackle",		tackling	);
	}
}
