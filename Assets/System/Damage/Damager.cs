using UnityEngine;
using System.Collections;

//--------------------------------------------------------------
/// <summary> A generic damager that causes damage upon
/// collision/trigger. </summary>
//--------------------------------------
[AddComponentMenu("Damage/Damager",100)]
public class Damager : MonoBehaviour
{
	//--------------------------------------------------------------
	/// <summary> Raises the trigger enter event. </summary>
	/// <param name="collider"> the collider </param>
	//--------------------------------------
	void OnTriggerEnter( Collider collider )
	{
		DamageInfo info = new DamageInfo();
		info.damager = this;
		info.collision = null;

		CollisionDamage( collider.gameObject, info );
	}

	//--------------------------------------------------------------
	/// <summary> Raises the collision enter event. </summary>
	/// <param name="collision"> the collision data </param>
	//--------------------------------------
	void OnCollisionEnter( Collision collision )
	{
		DamageInfo info = new DamageInfo();
		info.damager = this;
		info.collision = collision;

		CollisionDamage( collision.collider.gameObject, info );
	}
	
	//--------------------------------------------------------------
	/// <summary> Collisions callback to apply some damage to an 
	/// object we've just collided with. </summary>
	/// <param name="other">Other object.</param>
	/// <param name="info">Other object.</param>
	//--------------------------------------
	public virtual void CollisionDamage( GameObject other, DamageInfo info )
	{
		Damageable damageable = other.GetComponent<Damageable>();

		if( damageable )
		{
			if( DamageTest(damageable) )
			{
				damageable.OnDamage( info );
			}
		}
	}

	//--------------------------------------------------------------
	/// <summary> Does damage calculations and calls back on the damagees. </summary>
	/// <param name="damageable"> the game object to be damaged </param>
	/// <returns><c>true</c>, if the damageable should be damaged, 
	/// <c>false</c> otherwise.</returns>
	//--------------------------------------
	public virtual bool DamageTest( Damageable damageable )
	{
		return true;
	}
}
