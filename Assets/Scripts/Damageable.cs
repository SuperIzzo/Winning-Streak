using UnityEngine;
using System.Collections;

public class Damageable : MonoBehaviour
{
	public GoRagdoll ragdoll;
	public bool awardable = true;

	public void Damage( Damager damager )
	{
		// Turn into ragdoll here
		if( ragdoll )
		{
			ragdoll.KillPlayer();
		}

		if( awardable && damager.isAwarding )
		{
			ScoreManager.AddMultPoint( 3 );
			Commentator commentator = Commentator.GetInstance();

			if( commentator )
			{
				commentator.Comment( CommentatorEvent.HIT_PLAYER );
			}
		}
	}
}
