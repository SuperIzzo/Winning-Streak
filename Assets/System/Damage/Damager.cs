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
		CollisionDamage( collider.gameObject );
	}

	//--------------------------------------------------------------
	/// <summary> Raises the collision enter event. </summary>
	/// <param name="collision"> the collision data </param>
	//--------------------------------------
	void OnCollisionEnter( Collision collision )
	{
		CollisionDamage( collision.collider.gameObject );
	}
	
	//--------------------------------------------------------------
	/// <summary> Collisions callback to apply some damage </summary>
	/// <param name="otherObject">Other object.</param>
	//--------------------------------------
	public virtual void CollisionDamage( GameObject otherObject )
	{
		Damageable damageable = otherObject.GetComponent<Damageable>();

		if( damageable )
		{
			if( DamageTest(damageable) )
			{
				damageable.OnDamage( this );
			}
		}
	}

	//--------------------------------------------------------------
	/// <summary> Does damage calculations and calls back on the damagees. </summary>
	/// <param name="gameObject"> the game object to be damaged </param>
	//--------------------------------------
	public virtual bool DamageTest( Damageable gameObject )
	{
		return true;
	}
}
