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
	public override void OnDamage( Damager damager )
	{
        if(!controller)
            controller = GetComponent<BaseCharacterController>();

		controller.isKnockedDown = true;
	}
}
