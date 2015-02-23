using UnityEngine;
using System.Collections;

//--------------------------------------------------------------
/// <summary> Makes a GameObject with a <see cref="BaseCharacterController"/> 
/// component damageable. </summary>
//--------------------------------------
[AddComponentMenu("Damage/DamageableCharacter",1)]
[RequireComponent( typeof(BaseCharacterController) )]
public class DamageableCharacter : Damageable
{
	public Rigidbody rootBone;
	private BaseCharacterController controller;

	//--------------------------------------------------------------
	/// <summary> Unity Start callback. </summary>
	/// <description> Initializes components. </description>
	//--------------------------------------
	void Start()
	{
		controller = GetComponent<BaseCharacterController>();
	}

	//--------------------------------------------------------------
	/// <summary> Callback for a character taking damage. </summary>
	/// <description> Knocks down the character. </description>
	//--------------------------------------
	public override void OnDamage( DamageInfo info )
	{
        if(controller)
			controller.isKnockedDown = true;


		// HACK: Don't ask
		// This applies raw force to damagees based on the movement speed
		// of the tackling character
		if( rootBone )
		{
			var damagerController = info.damager.GetComponentInParent<BaseCharacterController>();
			if( damagerController )
			{
				Vector3 force = damagerController.lookDirection * 
					damagerController.relativeVelocity.magnitude;

				force.y = force.magnitude * 0.07f;
				force.Normalize();

				float forceMagnitude = 60.0f 
					+ Random.value*Random.value*80;

				force *= forceMagnitude;


				StartCoroutine( ApplyForce(force) );
			}
		}


	}


	private	IEnumerator ApplyForce( Vector3 force )
	{
		yield return null;

		rootBone.isKinematic = false;
		rootBone.AddForce( force, ForceMode.Impulse );
	}
}
