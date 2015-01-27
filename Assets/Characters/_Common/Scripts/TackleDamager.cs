using UnityEngine;
using System.Collections;

public class TackleDamager : Damager
{
	public BaseCharacterController controller;

	//--------------------------------------------------------------
	/// <summary> Setup this instance. </summary>
	//--------------------------------------
	void Start()
	{
		if( controller==null )
		{
			controller = GetComponentInParent<BaseCharacterController>();
		}
	}

	//--------------------------------------------------------------
	/// <summary> Does damage calculations and calls back on the damagees. </summary>
	/// <param name="gameObject"> the game object to be damaged </param>
	//--------------------------------------
	public override bool DamageTest( Damageable damageable )
	{
		bool doDamage = false;

		if( controller.isTackling )
		{
			// Do damage when tackling
			doDamage = true;

			// ...except when we are hitting ourselves
			Damageable[] myDamageableParts= GetComponentsInParent<Damageable>();
			foreach( Damageable myDamageable in myDamageableParts )
			{
				if( damageable == myDamageable )
				{
					doDamage = false;
					break;
				}
			}
		}

		return doDamage;
	}
}
