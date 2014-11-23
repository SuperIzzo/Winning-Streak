using UnityEngine;
using System.Collections;

public class Damager : MonoBehaviour
{
	public Damageable ignore;

	void OnTriggerEnter( Collider collider )
	{
		Damageable damageable = collider.GetComponent<Damageable>();

		if( damageable && !damageable.Equals( ignore ) )
		{
			damageable.Damage( this );
		}
	}

	void OnCollisionEnter( Collision collider )
	{
		Damageable damageable = collider.collider.GetComponent<Damageable>();
		
		if( damageable && !damageable.Equals( ignore ) )
		{
			damageable.Damage( this );
		}
	}
}
