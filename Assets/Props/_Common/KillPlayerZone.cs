using UnityEngine;
using System.Collections;

//--------------------------------------------------------------
/// <summary> A zone that kills characters. </summary>
//--------------------------------------
public class KillPlayerZone : MonoBehaviour
{
	void OnCollisionEnter(Collision col)
	{
		KillPlayer( col.gameObject );
	}

	void OnTriggerEnter( Collider col )
	{
		KillPlayer( col.gameObject );
	}

	void KillPlayer( GameObject obj )
	{
		BaseCharacterController controller = obj.GetComponentInParent<BaseCharacterController>();
		
		if( controller )
			controller.KnockDown();
	}
}
