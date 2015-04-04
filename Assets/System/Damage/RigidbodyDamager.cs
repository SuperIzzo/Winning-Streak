using UnityEngine;
using System.Collections;

//--------------------------------------------------------------
/// <summary> Rigidbody damager causes damage based 
/// on physics parameters. </summary>
//--------------------------------------
[AddComponentMenu("Damage/Rigidbody Damager",101)]
public class RigidbodyDamager : Damager
{
	//--------------------------------------------------------------
	/// <summary> Does damage calculations 
	/// and calls back on the damagees. </summary>
	/// <param name="damageable">the game object to be damaged</param>
	/// <returns><c>true</c>, if the damageable should be damaged, 
	/// <c>false</c> otherwise.</returns>
	//--------------------------------------
	public override bool DamageTest( Damageable damageable )
	{
		//if there is little movement on the y axis, don't damage
		return GetComponent<Rigidbody>().velocity.y > 1;
		
		// TODO: 	This is a barebones implementation
		//			in order to make this work properly for flying objects
		//			the speed and the mass have to be taken into consideration,
		//			non-moving objects should not be causing damage
		//			(unless we want the players to trip over them)
	}
}
