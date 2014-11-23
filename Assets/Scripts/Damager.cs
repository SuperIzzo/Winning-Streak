using UnityEngine;
using System.Collections;

public class Damager : MonoBehaviour
{
	public Damageable ignore;


	void Update()
	{
	}

	void OnTriggerEnter( Collider collider )
	{
		if( enabled )
		{
			Damageable damageable = collider.GetComponent<Damageable>();

			if( damageable && !damageable.Equals( ignore ) )
			{
				damageable.Damage( this );
			}
		}
	}

	void OnCollisionEnter( Collision collider )
	{
		if( enabled )
		{
			Damageable damageable = collider.collider.GetComponent<Damageable>();
			
			if( damageable && !damageable.Equals( ignore ) )
			{
				damageable.Damage( this );
			}
		}
	}
}
