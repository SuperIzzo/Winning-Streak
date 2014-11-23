using UnityEngine;
using System.Collections;

public class Damageable : MonoBehaviour
{
	public GoRagdoll ragdoll;

	public void Damage( Damager damager )
	{
		// Turn into ragdoll here
		if( ragdoll )
		{
			ragdoll.KillPlayer();
		}
	}
}
