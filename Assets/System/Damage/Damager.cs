using UnityEngine;
using System.Collections;

public class Damager : MonoBehaviour
{
	//--------------------------------------------------------------
	/// <summary> Raises the trigger enter event. </summary>
	/// <param name="collider"> the collider </param>
	//--------------------------------------
	void OnTriggerEnter( Collider collider )
	{
		DamageTest( collider.gameObject );
	}

	//--------------------------------------------------------------
	/// <summary> Raises the collision enter event. </summary>
	/// <param name="collision"> the collision data </param>
	//--------------------------------------
	void OnCollisionEnter( Collision collision )
	{
		DamageTest( collision.collider.gameObject );
	}

	//--------------------------------------------------------------
	/// <summary> Does damage calculations and calls back on the damagees. </summary>
	/// <param name="gameObject"> the game object to be damaged </param>
	//--------------------------------------
	void DamageTest( GameObject gameObject )
	{
        //if there is little movement on the y axis, don't damage
        if (this.rigidbody.velocity.y < 1)
            return;

		Damageable damageabble = gameObject.GetComponent<Damageable>();

		if( damageabble )
			damageabble.OnDamage( this );

		// TODO: 	This is a barebones implementation
		//			in order to make this work properly for flying objects
		//			the speed and the mass have to be taken into consideration,
		//			non-moving objects should not be causing damage
		//			(unless we want the players to trip over them)

		//throw new System.NotImplementedException();
	}
}
