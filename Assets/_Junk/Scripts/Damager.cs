using UnityEngine;
using System.Collections;

public class Damager : MonoBehaviour
{
	public Damageable ignore;
	public bool isAwarding = true;


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
